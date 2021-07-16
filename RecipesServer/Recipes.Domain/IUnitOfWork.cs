using Recipes.Domain.Repositories;

namespace Recipes.Domain
{
    public interface IUnitOfWork
    {
        void Commit();
        
        IRecipesRepository RecipesRepository { get; }
    }
}