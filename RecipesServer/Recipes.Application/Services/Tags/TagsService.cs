using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.Application.Models.Tags;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Application.Services.Tags
{
    public class TagsService: ITagsService
    {
        private readonly ITagRepository _tagsRepository;
        private readonly IMapper _mapper;

        public TagsService(ITagRepository tagsRepository, IMapper mapper)
        {
            _tagsRepository = tagsRepository;
            _mapper = mapper;
        }
        
        public async Task<List<Tag>> GetOrCreateTags(string[] tags)
        {
            if (tags.Length == 0)
                return new List<Tag>();
            
            var dbTags = await _tagsRepository.GetTags(tags);

            if (dbTags.Count < tags.Length)
            {
                var notAddedTags = tags.Except(dbTags.Select(x => x.Value));
                var newlyAddedTags = new List<Tag>();
                foreach (var tagToAdd in notAddedTags)
                {
                    newlyAddedTags.Add(await _tagsRepository.CreateTag(tagToAdd));
                }
                
                dbTags.AddRange(newlyAddedTags);
            }
            
            return dbTags;
        }

        public async Task<SuggestedTagsResult> GetSuggestedSearchTags()
        {
            return new SuggestedTagsResult 
            {
                TagValues = (await _tagsRepository.GetTagsByLevel(TagLevel.Suggested))
                    .Select(x => x.Value).ToArray()
            };;
        }

        public async Task<FeaturedTagsResult> GetFeaturedTags()
        {
            return new FeaturedTagsResult
            {
                Tags = _mapper.Map<TagInfo[]>(await _tagsRepository.GetTagsByLevel(TagLevel.Featured))
            };;
        }
    }
}