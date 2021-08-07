using System;
using System.IO;
using AutoMapper;
using Recipes.Application.DTOs.Recipe;
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

        public RecipesPageDto GetRecipesPage(string searchString, int pageSize, int page)
        {
            var count = _recipesRepository.GetRecipesCount(searchString);
            var pageCount = (int)Math.Ceiling(count * 1d / pageSize);

            if (page > 1 && page > pageCount)
                throw new ArgumentOutOfRangeException(nameof(page));

            var recipes = _recipesRepository.Get(searchString, (page - 1) * pageSize, pageSize);
            var recipesPage = new RecipesPageDto
            {
                Recipes = _mapper.Map<RecipePreviewDto[]>(recipes),
                PageCount = pageCount
            };
            return recipesPage;
        }

        public RecipeDetailDto GetRecipeDetail(int id)
        {
            var recipe = _recipesRepository.GetById(id);
            if (recipe == null)
                return null;

            return _mapper.Map<RecipeDetailDto>(recipe);
        }

        public int CreateRecipe(RecipeCreateDto recipeCreateDto)
        {
            var recipeModel = _mapper.Map<Recipe>(recipeCreateDto);
            var addedRecipeId = _recipesRepository.AddRecipe(recipeModel);
            _unitOfWork.Commit();
            return addedRecipeId;
        }

        public void EditRecipe(RecipeEditDto recipeEditDto)
        {
            var recipeModel = _mapper.Map<Recipe>(recipeEditDto);
            _recipesRepository.EditRecipe(recipeModel);
            _unitOfWork.Commit();
        }

        public void DeleteRecipe(int id)
        {
            _recipesRepository.DeleteRecipe(id);
            _unitOfWork.Commit();
        }
    }
}