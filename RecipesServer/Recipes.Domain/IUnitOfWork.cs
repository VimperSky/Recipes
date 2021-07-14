namespace Recipes.Domain
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}