using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Recipes.Application.Exceptions;
using Recipes.Application.Models.Recipe;
using Recipes.Application.Permissions.Models;
using Recipes.Application.Services.Recipes;
using Recipes.Application.Services.Recipes.Specifications;
using Recipes.Application.Services.Tags;
using Recipes.Application.UnitTests.TestDataProvider;
using Recipes.Domain;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;
using Recipes.Domain.Specifications;
using Xunit;
using static Recipes.Application.UnitTests.TestDataProvider.TestRecipeDataProvider;

namespace Recipes.Application.UnitTests
{
    public class RecipesServiceTests
    {
        private readonly Mock<IRecipesRepository> _recipesRepoMock;
        private readonly Mock<IImageFileSaver> _imageFileSaverMock;
        private readonly Mock<ITagsService> _tagsServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        
        private readonly UserClaims _mainUserClaims;
        private readonly IRecipesService _recipesService;
        
        public RecipesServiceTests()
        {
            _mainUserClaims = new UserClaims(Name, MainUserId);

            _tagsServiceMock = new Mock<ITagsService>();

            _recipesRepoMock = new Mock<IRecipesRepository>();
            
            _imageFileSaverMock = new Mock<IImageFileSaver>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _recipesService = new RecipesService(_recipesRepoMock.Object,
                _tagsServiceMock.Object, 
                _unitOfWorkMock.Object,
                Mapper, 
                _imageFileSaverMock.Object);
        }


        [Theory]
        [InlineData(RecipesSelectionType.All, typeof(AllRecipesFilterSpecification))]
        [InlineData(RecipesSelectionType.Own, typeof(OwnRecipesFilterSpecification))]
        [InlineData(RecipesSelectionType.Starred, typeof(StarredRecipesFilterSpecification))]
        public async Task GetRecipesPage_RecipesSelectionType_ExpectedFilterSpecification(
            RecipesSelectionType selectionType, 
            Type expectedSpecification)
        {
            // Act
            await _recipesService.GetRecipesPage(1, 1, selectionType, _mainUserClaims);

            // Assert
            _recipesRepoMock.Verify(x => 
                x.GetRecipesCount(It.Is<FilterSpecification<Recipe>>(t => t.GetType() == expectedSpecification)));
        }

        [Theory]
        [InlineData(RecipesSelectionType.All)]
        [InlineData(RecipesSelectionType.Own)]
        [InlineData(RecipesSelectionType.Starred)]
        public async Task GetRecipesPage_NoRecipesFirstPage_ReturnsEmptyList(RecipesSelectionType selectionType)
        {
            // Arrange
            _recipesRepoMock.Setup(x => 
                x.GetRecipesCount(It.IsAny<AllRecipesFilterSpecification>()))
                .Returns((FilterSpecification<Recipe> _) => Task.FromResult(0));

            // Act
            var recipesPage = await _recipesService.GetRecipesPage(1, 1, selectionType, _mainUserClaims);

            // Assert
            Assert.Equal(1, recipesPage.PageCount);
            Assert.Empty(recipesPage.Recipes);
            _recipesRepoMock.Verify(x => 
                x.GetRecipesCount(It.IsAny<FilterSpecification<Recipe>>()), Times.Once);
            _recipesRepoMock.Verify(x => x.GetList(It.IsAny<FilterSpecification<Recipe>>(),
                It.IsAny<PagingSpecification<Recipe>>()), Times.Once);
        }
        
        [Theory]
        [InlineData(RecipesSelectionType.All)]
        [InlineData(RecipesSelectionType.Own)]
        [InlineData(RecipesSelectionType.Starred)]
        public async Task GetRecipesPage_NoRecipesSecondPage_ThrowsElementNotFoundException(RecipesSelectionType selectionType)
        {
            // Arrange
            _recipesRepoMock.Setup(x => 
                    x.GetRecipesCount(It.IsAny<FilterSpecification<Recipe>>()))
                .Returns((FilterSpecification<Recipe> _) => Task.FromResult(0));

            // Act
            Func<Task<RecipesPageResult>> act = () => _recipesService.GetRecipesPage(1, 2, selectionType, _mainUserClaims);

            // Assert
            await Assert.ThrowsAsync<ElementNotFoundException>(act);
            _recipesRepoMock.Verify(x => 
                x.GetRecipesCount(It.IsAny<FilterSpecification<Recipe>>()), Times.Once);
            _recipesRepoMock.Verify(x => x.GetList(It.IsAny<FilterSpecification<Recipe>>(),
                It.IsAny<PagingSpecification<Recipe>>()), Times.Never);
        }


        [Fact]
        public async Task GetRecipesPage_MockedRecipes_ReturnsMockedRecipes()
        {
            _recipesRepoMock.Setup(x => 
                    x.GetRecipesCount(It.IsAny<FilterSpecification<Recipe>>()))
                .Returns((FilterSpecification<Recipe> _) => Task.FromResult(2));
            
            _recipesRepoMock.Setup(x => 
                    x.GetList(It.IsAny<FilterSpecification<Recipe>>(), It.IsAny<PagingSpecification<Recipe>>()))
                .Returns(() => Task.FromResult(new List<Recipe> {MainRecipe, OtherRecipe}));
            
            var page = await _recipesService.GetRecipesPage(2, 1, RecipesSelectionType.All, _mainUserClaims);
            
            Assert.Equal(1, page.PageCount);
            page.Recipes[0].Should().BeEquivalentTo(Mapper.Map<RecipePreviewResult>(MainRecipe));
            page.Recipes[1].Should().BeEquivalentTo(Mapper.Map<RecipePreviewResult>(OtherRecipe));
        }
        
        
        [Fact]
        public async Task GetRecipeDetail_NonExistingRecipe_ReturnsNull()
        {
            // Arrange
            const int recipeId = 656;
            
            // Act
            var detail = await _recipesService.GetRecipeDetail(recipeId, _mainUserClaims);
            
            // Assert
            Assert.Null(detail);
            _recipesRepoMock.Verify(x => x.GetById(recipeId), Times.Once);
        }
        
                
        [Fact]
        public async Task GetRecipeDetail_ExistingRecipe_ReturnsNull()
        {
            // Arrange
            _recipesRepoMock.Setup(x => x.GetById(MainRecipeId))
                .Returns(() => Task.FromResult(MainRecipe));
            
            // Act
            var detail = await _recipesService.GetRecipeDetail(MainRecipeId, _mainUserClaims);
            
            // Assert
            detail.Should().BeEquivalentTo(Mapper.Map<RecipeDetailResult>(MainRecipe));
            _recipesRepoMock.Verify(x => x.GetById(MainRecipeId), Times.Once);
        }
        
        [Fact]
        public async Task CreateRecipe_ValidInputData_RecipeIsCreated()
        {
            // Arrange
            _recipesRepoMock.Setup(x => x.AddRecipe(It.IsAny<Recipe>()))
                .Returns((Recipe recipe) =>
                {
                    recipe.Id = MainRecipeId;
                    return Task.FromResult(recipe);
                });
            
            var expectedRecipeModel = MainRecipe;
            expectedRecipeModel.AuthorId = _mainUserClaims.UserId;

            // Act
            var createdRecipeId = await _recipesService.CreateRecipe(TestRecipeDataProvider.RecipeCreateCommand,
                _mainUserClaims);
            
            // Assert
            _tagsServiceMock.Verify(x => x.GetOrCreateTags(TestRecipeDataProvider.RecipeCreateCommand.Tags), Times.Once);
            _recipesRepoMock.Verify(x => x.
                AddRecipe(It.Is<Recipe>(recipe => recipe.AuthorId == expectedRecipeModel.AuthorId)), Times.Once);
            _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
            
            Assert.Equal(MainRecipeId, createdRecipeId);
        }
        
        [Fact]
        public async Task EditRecipe_NonExistingRecipe_ThrowsElementNotFoundException()
        {
            // Arrange
            var dto = MainRecipeEditCommand;
            dto.Id = 999;

            // Act && Assert
            await Assert.ThrowsAsync<ElementNotFoundException>(() => _recipesService.EditRecipe(dto, _mainUserClaims));
            _recipesRepoMock.Verify(x => x.GetById(dto.Id), Times.Once);
        }
        
        [Fact]
        public async Task EditRecipe_NotOwnedRecipe_ThrowsPermissionsException()
        {
            // Arrange
            _recipesRepoMock.Setup(x => x.GetById(OtherRecipeId))
                .Returns(() => Task.FromResult(OtherRecipe));
            
            // Act
            Func<Task> act = () => _recipesService.EditRecipe(OtherRecipeEditCommand, _mainUserClaims);
            
            // Assert
            await Assert.ThrowsAsync<PermissionException>(act);
            _recipesRepoMock.Verify(x => x.GetById(OtherRecipeId), Times.Once);
        }
        
        [Fact]
        public async Task EditRecipe_OwnedRecipe_ChangesCorrespondingProperties()
        {
            // Arrange
            var returnedObject = OtherRecipe;
            returnedObject.AuthorId = MainUserId;
            returnedObject.Id = MainRecipeId;
            
            _recipesRepoMock.Setup(x => x.GetById(MainRecipeId))
                .Returns(() => Task.FromResult(returnedObject));
            
            // Act
            await _recipesService.EditRecipe(MainRecipeEditCommand, _mainUserClaims);
            
            // Assert
            returnedObject.Should().BeEquivalentTo(MainRecipe);
            
            _recipesRepoMock.Verify(x => x.GetById(MainRecipeId), Times.Once);
            _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }
        
        [Fact]
        public async Task DeleteRecipe_NonExistingRecipe_ThrowsElementNotFoundException()
        {
            // Arrange
            const int recipeId = 756;
            
            // Act && Assert
            await Assert.ThrowsAsync<ElementNotFoundException>(() => _recipesService.DeleteRecipe(recipeId, _mainUserClaims));
            _recipesRepoMock.Verify(x => x.GetById(recipeId), Times.Once);
        }
        
        [Fact]
        public async Task DeleteRecipe_NotOwnedRecipe_ThrowsPermissionsException()
        {
            // Arrange
            const int recipeId = 756;
                        
            _recipesRepoMock.Setup(x => x.GetById(recipeId))
                .Returns(() => Task.FromResult(OtherRecipe));
            
            // Act && Assert
            await Assert.ThrowsAsync<PermissionException>(() => _recipesService.DeleteRecipe(recipeId, _mainUserClaims));
            _recipesRepoMock.Verify(x => x.GetById(recipeId), Times.Once);
        }
        
        [Fact]
        public async Task DeleteRecipe_OwnedRecipe_RecipeIsDeleted()
        {
            // Arrange
            _recipesRepoMock.Setup(x => x.GetById(MainRecipeId))
                .Returns(() => Task.FromResult(MainRecipe));
            
            // Act && Assert
            await _recipesService.DeleteRecipe(MainRecipeId, _mainUserClaims);
            _recipesRepoMock.Verify(x => x.GetById(MainRecipeId), Times.Once);
            _recipesRepoMock.Verify(x => 
                x.DeleteRecipe(It.Is<Recipe>(recipe => recipe.Id == MainRecipeId)), Times.Once);
            
            _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }

        [Fact]
        public async Task UploadImage_NullFormFile_ThrowsArgumentNullException()
        {
            // Act && Assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => _recipesService.UploadImage(1, null, _mainUserClaims));
        }
        
        [Fact]
        public async Task UploadImage_NonExistingRecipeId_ThrowsElementNotFoundException()
        {
            // Arrange
            const int recipeId = 756;
            
            // Act && Assert
            await Assert.ThrowsAsync<ElementNotFoundException>(
                () => _recipesService.UploadImage(recipeId, ImageFormFile, _mainUserClaims));
            _recipesRepoMock.Verify(x => x.GetById(recipeId), Times.Once);
        }
        
        [Fact]
        public async Task UploadImage_ForeignRecipe_ThrowsPermissionsException()
        {
            // Arrange
            _recipesRepoMock.Setup(x => x.GetById(OtherRecipeId))
                .Returns(() => Task.FromResult(OtherRecipe));
            
            // Act && Assert
            await Assert.ThrowsAsync<PermissionException>(
                () => _recipesService.UploadImage(OtherRecipeId, ImageFormFile, _mainUserClaims));
            _recipesRepoMock.Verify(x => x.GetById(OtherRecipeId), Times.Once);
        }
        
        [Fact]
        public async Task UploadImage_OwnedRecipe_ImageIsUpdated()
        {
            // Arrange
            _recipesRepoMock.Setup(x => x.GetById(MainRecipeId))
                .Returns(() => Task.FromResult(MainRecipe));
            
            // Act
            await _recipesService.UploadImage(MainRecipeId, ImageFormFile, _mainUserClaims);

            // Assert
            _recipesRepoMock.Verify(x => x.GetById(MainRecipeId), Times.Once);
            _imageFileSaverMock.Verify(x => x.CreateFile(It.IsAny<IFormFile>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
        }
        
        [Fact]
        public async Task GetRecipeOfTheDay_MockedFunctions_ExpectedRecipeIsReturned()
        {
            // Arrange
            _recipesRepoMock.Setup(x => x.GetRecipeOfTheDay())
                .Returns(() => Task.FromResult(OtherRecipe));
            
            // Act
            var recipeOfTheDay = await _recipesService.GetRecipeOfTheDay();

            // Assert
            _recipesRepoMock.Verify(x => x.GetRecipeOfTheDay(), Times.Once);
            recipeOfTheDay.Should().BeEquivalentTo(Mapper.Map<RecipePreviewResult>(OtherRecipe));
        }
        
        
        [Fact]
        public async Task GetAuthorRecipesCount_MockedFunctions_ExpectedRecipeCountIsReturned()
        {
            // Arrange
            const int returnedCount = 5;
            _recipesRepoMock.Setup(x => x.GetRecipesCount(It.IsAny<OwnRecipesFilterSpecification>()))
                .Returns(() => Task.FromResult(returnedCount));
            
            // Act
            var recipeOfTheDay = await _recipesService.GetAuthorRecipesCount(_mainUserClaims);

            // Assert
            Assert.Equal(returnedCount, recipeOfTheDay);
            _recipesRepoMock.Verify(x => x.GetRecipesCount(It.IsAny<OwnRecipesFilterSpecification>()), Times.Once);
        }
    }
    
}