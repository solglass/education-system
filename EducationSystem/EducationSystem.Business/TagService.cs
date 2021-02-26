using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class TagService : ITagService
    {
        private TagRepository _tagRepository;
        public TagService()
        {
            _tagRepository = new TagRepository();
        }
        public List<TagDto> GetTags() { return _tagRepository.GetTags(); }
        public TagDto GetTagById(int id) { return _tagRepository.GetTagById(id); }
        public int UpdateTag(int id, TagDto tagDto) { return _tagRepository.TagUpdate(id, tagDto); }
        public int AddTag(TagDto tagDto) { return _tagRepository.TagAdd(tagDto); }
        public int DeleteTag(int id) { return _tagRepository.TagDelete(id); }
    }
}
