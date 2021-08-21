using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Recipes.Application.DTOs.Recipe;
using Recipes.Application.Exceptions;
using Recipes.Application.Permissions.Models;
using Recipes.Application.Services.Tags;
using Recipes.Domain;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Application.Services.Recipes
{
    public class RecipesService : IRecipesService
    {
        private readonly IImageFileSaver _imageFileSaver;
        private readonly IMapper _mapper;
        private readonly IRecipesRepository _recipesRepository;
        private readonly ITagsService _tagsService;
        private readonly IUnitOfWork _unitOfWork;

        public RecipesService(IRecipesRepository recipesRepository, ITagsService tagsService, IUnitOfWork unitOfWork, IMapper mapper,
            IImageFileSaver imageFileSaver)
        {
            _recipesRepository = recipesRepository;
            _tagsService = tagsService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageFileSaver = imageFileSaver;
        }

        public async Task<RecipesPageDto> GetRecipesPage(int pageSize, int page,
            string searchString = null, UserClaims authorClaims = null)
        {
            var authorId = authorClaims?.UserId ?? 0;
            
            var count = await _recipesRepository.GetRecipesCount(searchString, authorId);
            var pageCount = (int)Math.Ceiling(count * 1d / pageSize);

            if (page > 1 && page > pageCount)
                throw new ElementNotFoundException(ElementNotFoundException.RecipesPageNotFound);

            var recipes = await _recipesRepository.GetList((page - 1) * pageSize, pageSize, 
                searchString, authorId);
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
            var tags = await _tagsService.GetOrCreateTags(recipeCreateDto.Tags);

            var recipeModel = _mapper.Map<Recipe>(recipeCreateDto);
            recipeModel.AuthorId = userClaims.UserId;
            recipeModel.Tags = tags;
            
            var addedRecipe = await _recipesRepository.AddRecipe(recipeModel);

            _unitOfWork.Commit();
            return addedRecipe.Id;
        }

        public async Task EditRecipe(RecipeEditDto recipeEditDto, UserClaims userClaims)
        {
            var recipeDb = await _recipesRepository.GetById(recipeEditDto.Id);
            if (recipeDb == null)
                throw new ElementNotFoundException(ElementNotFoundException.RecipeNotFound);

            if (recipeDb.AuthorId != userClaims.UserId)
                throw new PermissionException(PermissionException.NotEnoughPermissionsToModifyResource);
            
            var tags = await _tagsService.GetOrCreateTags(recipeEditDto.Tags);
            
            var recipeModel = _mapper.Map<Recipe>(recipeEditDto);
            recipeModel.Tags = tags;
            
            foreach (var toProp in typeof(Recipe).GetProperties())
            {
                var value = toProp.GetValue(recipeModel, null);
                if (value != null) toProp.SetValue(recipeDb, value, null);
            }

            _unitOfWork.Commit();
        }

        public async Task UploadImage(int recipeId, IFormFile formFile, UserClaims userClaims)
        {
            if (formFile == null)
                throw new ArgumentNullException(nameof(formFile));

            var recipe = await _recipesRepository.GetById(recipeId);
            if (recipe == null)
                throw new ElementNotFoundException(ElementNotFoundException.RecipeNotFound);

            if (recipe.AuthorId != userClaims.UserId)
                throw new PermissionException(PermissionException.NotEnoughPermissionsToModifyResource);

            recipe.ImagePath = await _imageFileSaver.CreateFile(formFile);
            _unitOfWork.Commit();
        }

        public async Task DeleteRecipe(int recipeId, UserClaims userClaims)
        {
            var recipe = await _recipesRepository.GetById(recipeId);
            if (recipe == null)
                throw new ElementNotFoundException(ElementNotFoundException.RecipeNotFound);

            if (recipe.AuthorId != userClaims.UserId)
                throw new PermissionException(PermissionException.NotEnoughPermissionsToModifyResource);

            await _recipesRepository.DeleteRecipe(recipe);
            _unitOfWork.Commit();
        }
    }
}