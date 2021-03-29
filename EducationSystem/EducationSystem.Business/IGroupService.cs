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
        int DeleteTeacherGroup(int groupId, int userId);
        public int AddTeacherGroup(int groupId, int userId);
        public int DeleteStudentGroup(int userId, int groupId);
        public int AddStudentGroup(int groupId, int userId, StudentGroupDto studentGroupDto);
        int DeleteTutorGroup(int groupId, int userId);
        int AddTutorToGroup(int groupId, int userId);
        public StudentGroupDto GetStudentGroupById(int userGroupId);
        GroupDto GetGroupById(int id);
        GroupDto GetGroupProgramsByGroupId(int id);
        List<GroupDto> GetGroups();
        List<GroupDto> GetGroupsWithoutTutors();
        int UpdateGroup(GroupDto groupDto);
        List<GroupDto> GetGroupByThemeId(int id);
        List<GroupReportDto> GenerateReport();
        List<int> GetGroupsByStudentId(int id);
        List<int> GetGroupsByTeacherId(int id);
        List<int> GetGroupsByTutorId(int id);
    }
}