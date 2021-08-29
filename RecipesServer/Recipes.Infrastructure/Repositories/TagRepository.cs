using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly RecipesDbContext _recipesDbContext;

        public TagRepository(RecipesDbContext recipesDbContext)
        {
            _recipesDbContext = recipesDbContext;
        }

        public async Task<List<Tag>> GetTagsByLevel(TagLevel tagLevel)
        {
            return await _recipesDbContext.Tags.Where(x => x.TagLevel == tagLevel).ToListAsync();
        }

        public async Task<List<Tag>> GetTags(string[] tags)
        {
            return await _recipesDbContext.Tags.Where(x => tags.Contains(x.Value)).ToListAsync();
        }

        public async Task<Tag> CreateTag(string tag)
        {
            var createdTag = Tag.Create(tag);
            await _recipesDbContext.AddAsync(createdTag);
            return createdTag;
        }
    }
}