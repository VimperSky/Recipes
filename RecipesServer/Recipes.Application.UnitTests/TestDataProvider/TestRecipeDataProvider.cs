using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using AutoMapper;
using Recipes.Application.MappingProfiles;
using Recipes.Application.Models.Recipe;
using Recipes.Domain.Models;

namespace Recipes.Application.UnitTests.TestDataProvider
{
    public static class TestRecipeDataProvider
    {
        public const string ImagePath = "test_image.jpg";
        private const string ImageContentType = "image/jpeg";

        public const string Name = "testName";
        public const int MainUserId = 7;
        public const int OtherUserId = 8;
        public const int MainRecipeId = 3;
        public const int OtherRecipeId = 5;
        
        public static readonly IMapper MapperInst;
        
        static TestRecipeDataProvider()
        {
            MapperInst = ApplicationMappingConfig.CreateApplicationMapper();
        }
        
        public static Recipe MainRecipe => new()
        {
            Name = "Какое-то название",
            Description = "Описание рецепта",
            Ingredients = new[]
            {
                new RecipeIngredientsBlock
                {
                    Header = "заголовок 1", Value = "Клубника\nМолоко"
                },
                new RecipeIngredientsBlock
                {
                    Header = "Заголовок 2", Value = "Какао\nЧто-тоеще"
                }
            },
            Steps = new[]
            {
                "Берем что-то там",
                "Делаем что-то с этим",
                "Еще что-то делаем",
                "Готово!"
            },
            CookingTimeMin = 60,
            Portions = 5,
            Id = MainRecipeId,
            AuthorId = MainUserId,
            Tags = new List<Tag>()
        };
        
        public static Recipe OtherRecipe => new()
        {
            Name = "Какое-то название2",
            Description = "Описание рецепта2",
            Ingredients = new[]
            {
                new RecipeIngredientsBlock
                {
                    Header = "что-то тут есть такое", Value = "Да"
                },
            },
            Steps = new[]
            {
                "Тут какой-то шаг но на самом деле не важно",
            },
            CookingTimeMin = 33,
            Portions = 7,
            Id = OtherRecipeId,
            AuthorId = OtherUserId,
            Tags = new List<Tag>()
        };

        public static RecipeCreateCommand RecipeCreateCommand => MapperInst.Map<RecipeCreateCommand>(MainRecipe);
        public static RecipeEditCommand MainRecipeEditCommand => MapperInst.Map<RecipeEditCommand>(MainRecipe);
        public static RecipeEditCommand OtherRecipeEditCommand => MapperInst.Map<RecipeEditCommand>(OtherRecipe);


        public static StreamContent ImageStream
        {
            get
            {
                var file = File.OpenRead(ImagePath);
                var fileContent = new StreamContent(file);
                fileContent.Headers.Add("Content-Type", ImageContentType);
                return fileContent;
            }
        }
    }
}