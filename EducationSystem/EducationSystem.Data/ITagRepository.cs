using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface ITagRepository
    {
        List<HomeworkTagDto> GetHomeworkTagById(int Id);
        MaterialTagDto GetMaterialTagById(int Id);
        TagDto GetTagById(int Id);
        List<TagDto> GetTags();
        List<ThemeTagDto> GetThemeTagById(int Id);
        int HomeworkTagAdd(HomeworkTagDto Tag);
        int HomeworkTagDelete(int homeworkId, int tagId);
        int MaterialTagAdd(MaterialTagDto Tag);
        int MaterialTagDelete(int materialId, int tagId);
        int TagAdd(TagDto tag);
        int TagDelete(int Id);
        int TagUpdate(int id, TagDto tag);
        int ThemeTagAdd(ThemeTagDto Tag);
        int ThemeTagDelete(int themeId, int tagId);
        int ThemeTagUpdate(ThemeTagDto Tag);
    }
}