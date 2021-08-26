using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Recipes.Application.DTOs.Recipe;
using Recipes.Application.Exceptions;
using Recipes.Application.Permissions.Models;
using Recipes.Application.Services.Recipes.Specifications;
using Recipes.Application.Services.Tags;
using Recipes.Domain;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;
using Recipes.Domain.Specifications;

namespace Recipes.Application.Services.Recipes
{
    public class RecipesService : IRecipesService
    {
        private readonly IImageFileSaver _imageFileSaver;
        private readonly IMapper _mapper;
        private readonly IRecipesRepository _recipesRepository;
        private readonly ITagsService _tagsService;
        private readonly IUnitOfWork _unitOfWork;

        public RecipesService(IRecipesRepository recipesRepository,
            ITagsService tagsService, IUnitOfWork unitOfWork, IMapper mapper, IImageFileSaver imageFileSaver)
        {
            _recipesRepository = recipesRepository;
            _tagsService = tagsService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageFileSaver = imageFileSaver;
        }

        public async Task<RecipesPageDto> GetRecipesPage(int pageSize, int page,
            RecipesType recipesType, UserClaims userClaims, string searchString = null)
        {
            FilterSpecification<Recipe> filterSpecification = recipesType switch
            {
                RecipesType.Own => new OwnRecipesFilterSpecification(userClaims.UserId),
                RecipesType.Starred => new StarredRecipesFilterSpecification(userClaims.UserId),
                _ => new AllRecipesFilterSpecification(searchString)
            };
            
            var count = await _recipesRepository.GetRecipesCount(filterSpecification);
            var pageCount = (int)Math.Ceiling(count * 1d / pageSize);

            if (page > 1 && page > pageCount)
                throw new ElementNotFoundException(ElementNotFoundException.RecipesPageNotFound);
            var pagingSpecification = new PagingSpecification<Recipe>((page - 1) * pageSize, pageSize);
            
            var recipes = await _recipesRepository.GetList(filterSpecification, pagingSpecification);

            var dtoRecipes = _mapper.Map<RecipePreviewDto[]>(recipes);
            if (userClaims.IsAuthorized)
            {
                for (var i = 0; i < dtoRecipes.Length; i++)
                {
                    dtoRecipes[i].IsLiked =
                        recipes[i].Activities.Any(x => x.IsLiked && x.UserId == userClaims.UserId);
                    dtoRecipes[i].IsStarred =
                        recipes[i].Activities.Any(x => x.IsStarred && x.UserId == userClaims.UserId);
                }
            }
            
            return new RecipesPageDto
            {
                Recipes = dtoRecipes,
                PageCount = pageCount
            };
        }
        
        public async Task<RecipeDetailDto> GetRecipeDetail(int id, UserClaims authorClaims)
        {
            var recipe = await _recipesRepository.GetById(id);
            if (recipe == null)
                return null;

            var dto = _mapper.Map<RecipeDetailDto>(recipe);
            if (authorClaims.IsAuthorized)
            {
                dto.IsLiked = recipe.Activities.Any(x => x.IsLiked && x.UserId == authorClaims.UserId);
                dto.IsStarred = recipe.Activities.Any(x => x.IsStarred && x.UserId == authorClaims.UserId);
            }

            return dto;
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

        public async Task<RecipePreviewDto> GetRecipeOfTheDay()
        {
            var recipe = await _recipesRepository.GetRecipeOfTheDay();
            return _mapper.Map<RecipePreviewDto>(recipe);
        }

        public async Task<int> GetAuthorRecipesCount(UserClaims userClaims)
        {
            var specification = new OwnRecipesFilterSpecification(userClaims.UserId);
            return await _recipesRepository.GetRecipesCount(specification);
        }
    }
}