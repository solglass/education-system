using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface IGroupService
    {
        int AddGroup(GroupDto groupDto);
        int AddGroup_Material(int groupId, int materialId);
        int DeleteGroup(int id);
        int DeleteGroup_Material(int groupId, int materialId);
        GroupDto GetGroupById(int id);
        GroupDto GetGroupProgramsByGroupId(int id);
        List<GroupDto> GetGroups();
        List<GroupDto> GetGroupsWithoutTutors();
        int UpdateGroup(GroupDto groupDto);
        List<GroupDto> GetGroupByThemeId(int id);
    }
}