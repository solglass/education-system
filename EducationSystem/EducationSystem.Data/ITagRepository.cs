using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface ITagRepository
    {
        
        TagDto GetTagById(int id);
        List<TagDto> GetTags();         
        int MaterialTagAdd(int materialId, int tagId);
        int MaterialTagDelete(int materialId, int tagId);
        int TagAdd(TagDto tag);
        int TagDelete(int id);
        int ThemeTagAdd(int themeId, int tagId);
        int ThemeTagDelete(int themeId, int tagId);
    }
}