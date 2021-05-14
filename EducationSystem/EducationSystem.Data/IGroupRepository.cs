using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IGroupRepository
    {
        int AddGroup(GroupDto groupDto);
        int AddGroup_Material(int groupId, int materialId);
        int AddStudentGroup(StudentGroupDto studentGroup);
        int AddTeacherGroup(int groupId,int teacherId);
        int AddTutorToGroup(int userId,int groupId);
        int DeleteGroup(int id);
        int DeleteGroup_Material(int groupId, int materialId);
        int DeleteStudentGroup(int userId, int groupId);
        int DeleteTeacherGroup(int userId, int groupId);
        List<GroupReportDto> GenerateReport();
        GroupDto GetGroupById(int id);
        List<GroupDto> GetGroups();
        List<GroupDto> GetGroupsByCourseId(int id);
        public List<int> GetGroupsByStudentId(int id);
        public List<int> GetGroupsByTutorId(int id);
        public List<int> GetGroupsByTeacherId(int id);
        List<GroupDto> GetGroupsByHomeworkId(int id);
        List<GroupDto> GetGroupsWithoutTutors();
        StudentGroupDto GetStudentGroupById(int id);
        int UpdateGroup(GroupDto groupDto);
        List<NumberOfLessonsForGroupToCompleteTheThemeDto> GetGroupByThemeId(int id);
        int DeleteTutorGroup(int userId, int groupId);
    }
}