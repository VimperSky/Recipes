using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Domain.Models;

namespace Recipes.Domain.Repositories
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetTagsByLevel(TagLevel tagLevel);

        Task<List<Tag>> GetTags(string[] tags);

        Task<Tag> CreateTag(string tag);
    }
}