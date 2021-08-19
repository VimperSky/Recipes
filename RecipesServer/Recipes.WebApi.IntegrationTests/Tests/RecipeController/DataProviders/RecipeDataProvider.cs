using System.IO;
using System.Net.Http;
using Recipes.Application.DTOs.Recipe;

namespace Recipes.WebApi.IntegrationTests.Tests.RecipeController.DataProviders
{
    public static class RecipeDataProvider
    {
        public const string TestImagePath = "test_image.jpg";
        private const string TestImageContentType = "image/jpeg";

        public static RecipeCreateDto TestRecipeCreateDto => new()
        {
            Name = "Какое-то название",
            Description = "Описание рецепта",
            Ingredients = new[]
            {
                new IngredientDto
                {
                    Header = "заголовок 1", Value = "Клубника\nМолоко"
                },
                new IngredientDto
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
            Tags = new[] {"Ягоды", "Клубника", "Лето"},
            CookingTimeMin = 60,
            Portions = 5
        };

        public static RecipeEditDto TestRecipeEditDto => new()
        {
            Name = "Какое-то название",
            Description = "Описание рецепта",
            Ingredients = new[]
            {
                new IngredientDto
                {
                    Header = "заголовок 1", Value = "Клубника\nМолоко"
                },
                new IngredientDto
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
            Tags = new[] {"Фрукты", "Овощи", "Огурец"},
            CookingTimeMin = 60,
            Portions = 5
        };

        public static StreamContent TestImageStream
        {
            get
            {
                var file = File.OpenRead(TestImagePath);
                var fileContent = new StreamContent(file);
                fileContent.Headers.Add("Content-Type", TestImageContentType);
                return fileContent;
            }
        }
    }
}