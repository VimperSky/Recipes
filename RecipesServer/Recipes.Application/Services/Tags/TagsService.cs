using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.Application.DTOs.Tags;
using Recipes.Domain;
using Recipes.Domain.Models;
using Recipes.Domain.Repositories;

namespace Recipes.Application.Services.Tags
{
    public class TagsService: ITagsService
    {
        private readonly ITagRepository _tagsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagsService(ITagRepository tagsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tagsRepository = tagsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<List<Tag>> VerifyTags(string[] tags)
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

        public async Task<SuggestedTagsDto> GetSuggestedSearchTags()
        {
            var dto = new SuggestedTagsDto 
            {
                TagValues = (await _tagsRepository.GetTagsByLevel(TagLevel.SearchSuggested))
                    .Select(x => x.Value).ToArray()
            };
            return dto;
        }
    }
}