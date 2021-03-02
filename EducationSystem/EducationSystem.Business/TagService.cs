using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class TagService : ITagService
    {
        private ITagRepository _tagRepository;
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public List<TagDto> GetTags() { return _tagRepository.GetTags(); }
        public TagDto GetTagById(int id) { return _tagRepository.GetTagById(id); }
        public int UpdateTag(TagDto tagDto) { return _tagRepository.TagUpdate(tagDto); }
        public int AddTag(TagDto tagDto) { return _tagRepository.TagAdd(tagDto); }
        public int DeleteTag(int id) { return _tagRepository.TagDelete(id); } 
    }
}
