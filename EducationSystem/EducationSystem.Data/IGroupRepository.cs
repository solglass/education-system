﻿using EducationSystem.Data.Models;
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
        int DeleteStudentGroupById(int userId, int groupId);
        int DeleteTeacherGroup(int userId, int groupId);
        int DeleteTutorGroupsByIds(int userId, int groupId);
        List<GroupReportDto> GenerateReport();
        GroupDto GetGroupById(int id);
        GroupDto GetGroupProgramsByGroupId(int id);
        List<GroupDto> GetGroups();
        List<GroupDto> GetGroupsByCourseId(int id);
        List<GroupDto> GetGroupsWithoutTutors();
        StudentGroupDto GetStudentGroupById(int id);
        int HardDeleteGroup(int id);
        int UpdateGroup(GroupDto groupDto);
        List<GroupDto> GetGroupByThemeId(int id);
    }
}