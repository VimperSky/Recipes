using Recipes.Domain;

namespace Recipes.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly RecipesDbContext _dbContext;

        public UnitOfWork(RecipesDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}