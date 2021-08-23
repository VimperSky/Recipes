using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Recipes.Application.Exceptions;
using Recipes.Application.Permissions.Models;
using Recipes.Application.Services.Recipes;
using Recipes.Application.Services.Tags;
using Recipes.Domain;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;
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
            _recipesRepoMock.Setup(x => x.AddRecipe(It.IsAny<Recipe>()))
                .Returns((Recipe recipe) =>
                {
                    recipe.Id = MainRecipeId;
                    return Task.FromResult(recipe);
                });
            
            _imageFileSaverMock = new Mock<IImageFileSaver>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _recipesService = new RecipesService(_recipesRepoMock.Object,
                _tagsServiceMock.Object, _unitOfWorkMock.Object, MapperInst, _imageFileSaverMock.Object);
        }
        
        [Fact]
        public async Task CreateRecipe_ShouldCallAddRecipeAndCommit()
        {
            // Arrange
            var expectedRecipeModel = MainRecipe;
            expectedRecipeModel.AuthorId = _mainUserClaims.UserId;

            // Act
            var createdRecipeId = await _recipesService.CreateRecipe(RecipeCreateDto, _mainUserClaims);
            
            // Assert
            _recipesRepoMock.Verify(x => x.
                AddRecipe(It.Is<Recipe>(recipe => recipe.AuthorId == expectedRecipeModel.AuthorId)), Times.Once);
            _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
            Assert.Equal(MainRecipeId, createdRecipeId);
        }
        
        [Fact]
        public async Task EditRecipe_NonExistingRecipe_ThrowsElementNotFoundException()
        {
            // Arrange
            var dto = MainRecipeEditDto;
            dto.Id = 999;

            // Act && Assert
            await Assert.ThrowsAsync<ElementNotFoundException>(() => _recipesService.EditRecipe(dto, _mainUserClaims));
        }
        
        [Fact]
        public async Task EditRecipe_NotOwnedRecipe_ThrowsPermissionsException()
        {
            // Arrange
            _recipesRepoMock.Setup(x => x.GetById(OtherRecipeId))
                .Returns(() => Task.FromResult(OtherRecipe));
            
            // Act && Assert
            await Assert.ThrowsAsync<PermissionException>(() => _recipesService.EditRecipe(OtherRecipeEditDto, _mainUserClaims));
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
            await _recipesService.EditRecipe(MainRecipeEditDto, _mainUserClaims);
            
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
    }
}