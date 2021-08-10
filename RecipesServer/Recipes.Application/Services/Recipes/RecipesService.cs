using System;
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
    public class RecipesService
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecipesService(IRecipesRepository recipesRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _recipesRepository = recipesRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RecipesPageDto> GetRecipesPage(string searchString, int pageSize, int page)
        {
            var count = await _recipesRepository.GetRecipesCount(searchString);
            var pageCount = (int)Math.Ceiling(count * 1d / pageSize);

            if (page > 1 && page > pageCount)
                throw new ArgumentOutOfRangeException(nameof(page));

            var recipes = await _recipesRepository.Get(searchString, (page - 1) * pageSize, pageSize);
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
            var recipeModel = _mapper.Map<Recipe>(recipeEditDto);
            if (recipeModel.AuthorId != userClaims.UserId)
                throw new PermissionException(PermissionException.NotEnoughPermissionsToModifyRecipe);
            
            await _recipesRepository.EditRecipe(recipeModel);

            _unitOfWork.Commit();
        }

        public async Task UploadImage(int recipeId, IFormFile formFile, UserClaims userClaims)
        {
            if (formFile == null)
                throw new ArgumentNullException(nameof(formFile));
            
            var recipe = await _recipesRepository.GetById(recipeId);
            if (recipe == null)
                throw new ArgumentException("recipeId with this id doesn't exist", nameof(recipeId));

            if (recipe.AuthorId != userClaims.UserId)
                throw new PermissionException(PermissionException.NotEnoughPermissionsToModifyRecipe);
            
            recipe.ImagePath = await FileTools.CreateFile(formFile);
            await _recipesRepository.EditRecipe(recipe);
            _unitOfWork.Commit();
        }

        public async Task DeleteRecipe(int recipeId, UserClaims userClaims)
        {
            var recipe = await _recipesRepository.GetById(recipeId);
            if (recipe == null)
                throw new ArgumentException("recipeId with this id doesn't exist", nameof(recipeId));
            
            if (recipe.AuthorId != userClaims.UserId)
                throw new PermissionException(PermissionException.NotEnoughPermissionsToModifyRecipe);
            
            await _recipesRepository.DeleteRecipe(recipeId);
            _unitOfWork.Commit();
        }
    }
}