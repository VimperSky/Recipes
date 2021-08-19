using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetSelectedTags();
        Task<IEnumerable<Tag>> VerifyTags(string[] tags);
    }
}