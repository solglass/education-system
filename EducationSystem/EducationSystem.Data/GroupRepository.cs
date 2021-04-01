﻿using Dapper;
using EducationSystem.Core.Config;
using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        public GroupRepository(IOptions<AppSettingsConfig> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<GroupDto> GetGroups()
        {
            var result = _connection
                .Query<GroupDto, CourseDto, int, GroupDto>("dbo.Group_SelectAll",
                    (group, course, groupStatus) =>
                    {
                        group.Course = course;
                        group.GroupStatus = (GroupStatus)groupStatus;
                        return group;
                    },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .ToList();
            return result;
        }
       
        public List<GroupDto> GetGroupByThemeId(int themeid)
        {
            List<ThemeDto> Themes = new List<ThemeDto>();
            ThemeDto themeEntry = new ThemeDto();
            CourseDto courseEntry = new CourseDto();
            var result = _connection
               .Query<GroupDto,CourseDto, int, GroupDto>("dbo.Group_SelectByTheme",
                   (group, course, groupStatus) =>
                   {
                       if (course == null)
                       {
                           course = courseEntry;
                       }
                       group.Course = course;
                       if (group.Course.Themes == null)
                       {
                           group.Course.Themes = Themes;
                       }
                       group.GroupStatus = (GroupStatus)groupStatus;
                       return group;
                   }, new { themeid },
                   splitOn: "Id",
                   commandType: System.Data.CommandType.StoredProcedure)
               .Distinct()
               .ToList();
            return result;
        }
        public GroupDto GetGroupProgramsByGroupId(int id)
        {
            var groupEntry = new GroupDto();
            var courseEntry = new CourseDto();
            var themeDictionary = new Dictionary<int, ThemeDto>();
            var result = _connection
                .Query<GroupDto, CourseDto, ThemeDto, int, GroupDto>(
                    "dbo.Group_SelectByProgram",
                    (group, course, theme, groupStatus) =>
                    {
                        if (groupEntry.Id == 0)
                        {
                            groupEntry = group;
                            groupEntry.Course = new CourseDto();
                        }
                        if (courseEntry.Id == 0)
                        {
                            courseEntry = course;
                            courseEntry.Themes = new List<ThemeDto>();
                            groupEntry.Course = courseEntry;
                        }
                        if (theme != null && !themeDictionary.TryGetValue(theme.Id, out ThemeDto themeEntry))
                        {
                            themeEntry = theme;
                            courseEntry.Themes.Add(theme);
                            themeDictionary.Add(themeEntry.Id, themeEntry);
                        }
                        groupEntry.GroupStatus = (GroupStatus)groupStatus;
                        return groupEntry;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return result;
        }
        public GroupDto GetGroupById(int id)
        {
            var result = _connection
                .Query<GroupDto, CourseDto, int, GroupDto>("dbo.Group_SelectById",
                    (group, course, groupStatus) =>
                    {
                        group.Course = course;
                        group.GroupStatus = (GroupStatus)groupStatus;
                        return group;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .FirstOrDefault();
            return result;
        }
        public List<GroupDto> GetGroupsWithoutTutors()
        {
            var result = _connection
                .Query<GroupDto, int, CourseDto, GroupDto>("dbo.Group_SelectWithoutTutors",
                 (group, groupStatus, course) =>
                 {
                     group.Course = course;
                     group.GroupStatus = (GroupStatus)groupStatus;
                     return group;
                 },
                    splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int AddGroup(GroupDto groupDto)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Group_Add",
                new
                {
                    CourseID = groupDto.Course.Id,
                    StatusId = (int)groupDto.GroupStatus,
                    StartDate = groupDto.StartDate
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int UpdateGroup(GroupDto groupDto)
        {
            var result = _connection
                .Execute("dbo.Group_Update",
                new
                {
                    Id = groupDto.Id,
                    CourseID = groupDto.Course.Id,
                    StatusId = (int)groupDto.GroupStatus,
                    StartDate = groupDto.StartDate
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteGroup(int id)
        {
            var result = _connection
                .Execute("dbo.Group_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int HardDeleteGroup(int id)
        {
            var result = _connection
                .Execute("dbo.Group_HardDelete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int AddGroup_Material(int groupId, int materialId)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Group_Material_Add",
                new
                {
                    groupId,
                    materialId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public int DeleteGroup_Material(int groupId, int materialId)
        {
            var result = _connection
                .Execute("dbo.Group_Material_Delete",
                new
                {
                    groupId,
                    materialId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
        public List<GroupDto> GetGroupsByCourseId(int id)
        {

            var groups = _connection.
                Query<GroupDto, int, CourseDto, GroupDto>("dbo.Group_SelectAllByCourseId", (group, groupStatus, course ) =>
                {
                  group.Course = course;
                  group.GroupStatus = (GroupStatus)groupStatus;
                  return group;
                
                },
                new { courseId = id },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                
                .ToList();
            return groups;
        }

        public List<int> GetGroupsByStudentId(int id)
        {
            var result = _connection.
                Query<int>("dbo.Group_SelectAllByStudentId",
                new { studentId = id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public List<int> GetGroupsByTutorId(int id)
        {
            var result = _connection.
                Query<int>("dbo.Group_SelectAllByTutorId",
                new { tutorId = id }, commandType: CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public List<int> GetGroupsByTeacherId(int id)
        {
            var result = _connection.
                Query<int>("dbo.Group_SelectAllByTeacherId",
                new { teacherId = id }, commandType: CommandType.StoredProcedure)
                .ToList();
            return result;
        }
        public int DeleteTutorGroup(int userId, int groupId)
        {
            return _connection.Execute("dbo.Tutor_Group_Delete",
                new
                { 
                    userId,
                    groupId 
                }, 
                commandType: CommandType.StoredProcedure);
        }
        public int AddTutorToGroup(int userId, int groupId)
        {

            return _connection.Execute("dbo.Tutor_Group_Add",
                new
                {
                    userId,
                   groupId
                },
                commandType: CommandType.StoredProcedure);
        }
        public StudentGroupDto GetStudentGroupByGroupAndUserIds(int groupId, int userId)
        {
            var studentGroupEntry = new StudentGroupDto();
            return _connection.Query<StudentGroupDto, UserDto, GroupDto, int, StudentGroupDto>
                ("dbo.Student_Group_SelectByGroupAndUserId",
                (studentGroup, student, group, groupStatus)=>
                {
                    studentGroupEntry = studentGroup;
                    studentGroupEntry.User = student;
                    studentGroupEntry.Group = group;
                    studentGroupEntry.Group.GroupStatus = (GroupStatus)groupStatus;

                    return studentGroupEntry;
                },
                new
                {
                    groupId,
                    userId
                },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

        }
        public int DeleteStudentGroup(int userId, int groupId)
        {
            return _connection.Execute("dbo.Student_Group_Delete",
                new 
                { 
                    userId,
                    groupId 
                },
                commandType: CommandType.StoredProcedure);
        }
        public int AddStudentGroup(StudentGroupDto studentGroup)
        {
            return _connection.QuerySingleOrDefault<int>("dbo.Student_Group_Add",
                   new
                   {
                       UserId = studentGroup.User.Id,
                       GroupId = studentGroup.Group.Id,
                       contractNumber = studentGroup.ContractNumber
                   },
                   commandType: CommandType.StoredProcedure);
        }

        public int DeleteTeacherGroup(int userId, int groupId)
        {

            return _connection.Execute("dbo.Teacher_Group_Delete",
                new
                {
                    userId,
                    groupId
                },
                commandType: CommandType.StoredProcedure);
        }
        public int AddTeacherGroup(int userId, int groupId)
        {

            return _connection.QuerySingleOrDefault<int>("dbo.Teacher_Group_Add",
                new
                {
                    userId,
                    groupId
                },
                commandType: CommandType.StoredProcedure);
        }
        public List<GroupReportDto> GenerateReport()
        {
            return _connection
            .Query<GroupReportDto>("dbo.Create_Report",
            commandType: CommandType.StoredProcedure)
            .ToList();
        }

    }
}
