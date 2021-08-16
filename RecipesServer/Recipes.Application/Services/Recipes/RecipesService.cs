﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Recipes.Application.DTOs.Recipe;
using Recipes.Application.Exceptions;
using Recipes.Application.Permissions.Models;
using Recipes.Domain;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Application.Services.Recipes
{
    public class RecipesService : IRecipesService
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageFileSaver _imageFileSaver;

        public RecipesService(IRecipesRepository recipesRepository, IUnitOfWork unitOfWork, IMapper mapper, IImageFileSaver imageFileSaver)
        {
            _recipesRepository = recipesRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageFileSaver = imageFileSaver;
        }

        public async Task<RecipesPageDto> GetRecipesPage(string searchString, int pageSize, int page)
        {
            var count = await _recipesRepository.GetRecipesCount(searchString);
            var pageCount = (int)Math.Ceiling(count * 1d / pageSize);

            if (page > 1 && page > pageCount)
                throw new ResourceNotFoundException(ResourceNotFoundException.RecipesPageNotFound);

            var recipes = await _recipesRepository.GetList(searchString, (page - 1) * pageSize, pageSize);
            var recipesPage = new RecipesPageDto
            {
                Recipes = _mapper.Map<RecipePreviewDto[]>(recipes),
                PageCount = pageCount
            };
            return recipesPage;
        }

        public async Task<RecipeDetailDto> GetRecipeDetail(int id)
        {
            var recipe = await _recipesRepository.GetById(id);
            if (recipe == null)
                return null;

            return _mapper.Map<RecipeDetailDto>(recipe);
        }

        public async Task<int> CreateRecipe(RecipeCreateDto recipeCreateDto, UserClaims userClaims)
        {
            var recipeModel = _mapper.Map<Recipe>(recipeCreateDto);
            recipeModel.AuthorId = userClaims.UserId;
            
            var addedRecipe = await _recipesRepository.AddRecipe(recipeModel);

            _unitOfWork.Commit();
            return addedRecipe.Id;
        }
        
        public async Task EditRecipe(RecipeEditDto recipeEditDto, UserClaims userClaims)
        {
            var recipeDb = await _recipesRepository.GetById(recipeEditDto.Id);
            if (recipeDb == null)
                throw new ResourceNotFoundException(ResourceNotFoundException.RecipeNotFound);
            
            if (recipeDb.AuthorId != userClaims.UserId)
                throw new PermissionException(PermissionException.NotEnoughPermissionsToModifyResource);
            
            var recipeModel = _mapper.Map<Recipe>(recipeEditDto);
            foreach(var toProp in typeof(Recipe).GetProperties())
            {
                var value = toProp.GetValue(recipeModel, null);
                if (value != null)
                {
                    toProp.SetValue(recipeDb, value, null);
                }
            }

            _unitOfWork.Commit();
        }
        
        public async Task UploadImage(int recipeId, IFormFile formFile, UserClaims userClaims)
        {
            if (formFile == null)
                throw new ArgumentNullException(nameof(formFile));
            
            var recipe = await _recipesRepository.GetById(recipeId);
            if (recipe == null)
                throw new ResourceNotFoundException(ResourceNotFoundException.RecipeNotFound);

            if (recipe.AuthorId != userClaims.UserId)
                throw new PermissionException(PermissionException.NotEnoughPermissionsToModifyResource);
            
            recipe.ImagePath = await _imageFileSaver.CreateFile(formFile);
            _unitOfWork.Commit();
        }

        public async Task DeleteRecipe(int recipeId, UserClaims userClaims)
        {
            var recipe = await _recipesRepository.GetById(recipeId);
            if (recipe == null)
                throw new ResourceNotFoundException(ResourceNotFoundException.RecipeNotFound);
            
            if (recipe.AuthorId != userClaims.UserId)
                throw new PermissionException(PermissionException.NotEnoughPermissionsToModifyResource);
            
            await _recipesRepository.DeleteRecipe(recipe);
            _unitOfWork.Commit();
        }
    }
}