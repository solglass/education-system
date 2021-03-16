using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface ITagService
    {
        int AddTag(TagDto tagDto);
        int DeleteTag(int id);
        TagDto GetTagById(int id);
        List<TagDto> GetTags();
    }
}