using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Application.DTOs.Tags;
using Recipes.Domain.Models;

namespace Recipes.Application.Services.Tags
{
    public interface ITagsService
    {
        /// <summary>
        /// Ensures that all tags in this list are added to database
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public Task<List<Tag>> GetOrCreateTags(string[] tags);

        public Task<SuggestedTagsDto> GetSuggestedSearchTags();

        public Task<FeaturedTagsDto> GetFeaturedTags();

    }
}