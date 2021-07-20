using Recipes.Domain;

namespace Recipes.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly RecipesContext _context;

        public UnitOfWork(RecipesContext context)
        {
            _context = context;
        }
        
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}