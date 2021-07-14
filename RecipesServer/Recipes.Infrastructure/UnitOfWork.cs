using Recipes.Domain;
using Recipes.Domain.Repositories;

namespace Recipes.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly RecipesContext _context;
        private readonly IRecipesRepository _recipesRepository;
        public UnitOfWork(RecipesContext context, IRecipesRepository recipesRepository)
        {
            _context = context;
            _recipesRepository = recipesRepository;
        }
        
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}