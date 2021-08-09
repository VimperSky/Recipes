using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        private static async Task<string> CreateFile(IFormFile formFile)
        {
            var guid = Guid.NewGuid();
            var imagePath = Path.Combine("Storage", "images", $"recipe_img_{guid}.{Path.GetExtension(formFile.FileName)}");
            await using Stream fileStream = new FileStream(imagePath, FileMode.Create);
            await formFile.CopyToAsync(fileStream);

            return Path.Combine($"images/recipe_img_{guid}.{Path.GetExtension(formFile.FileName)}");
        }
        
        public async Task<int> CreateRecipe(RecipeCreateDto recipeCreateDto)
        {
            var recipeModel = _mapper.Map<Recipe>(recipeCreateDto);
            
            var addedRecipe = await _recipesRepository.AddRecipe(recipeModel);

            _unitOfWork.Commit();
            return addedRecipe.Id;
        }

        public async Task EditRecipe(RecipeEditDto recipeEditDto)
        {
            var recipeModel = _mapper.Map<Recipe>(recipeEditDto);

            var editRecipe = _recipesRepository.EditRecipe(recipeModel);
            await editRecipe;

            _unitOfWork.Commit();
        }

        public async Task UploadImage(int recipeId, IFormFile formFile)
        {
            if (formFile == null)
                throw new ArgumentNullException(nameof(formFile));
            
            var recipe = await _recipesRepository.GetById(recipeId);
            if (recipe == null)
                throw new ArgumentException("recipeId with this id doesn't exist", nameof(recipeId));
            
            recipe.ImagePath = await CreateFile(formFile);
            await _recipesRepository.EditRecipe(recipe);
            _unitOfWork.Commit();
        }

        public async Task DeleteRecipe(int id)
        {
            await _recipesRepository.DeleteRecipe(id);
            _unitOfWork.Commit();
        }
    }
}