using Dapper;
using EducationSystem.Core.Config;
using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        public GroupDto GetGroupProgramsByGroupId(int id)
        {
            var groupEntry = new GroupDto();
            var courseEntry = new CourseDto();
            var themeDictionary = new Dictionary<int, ThemeDto>();
            var result = _connection
                .Query<GroupDto, CourseDto, ThemeDto, GroupDto>(
                    "dbo.Group_SelectByProgram",
                    (group, course, theme) =>
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
                        return groupEntry;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
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
                Query<GroupDto>("dbo.Group_SelectAllByCourseId",
                new { courseId = id }, commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return groups;
        }
        public List<TutorGroupDto> GetTutorGroups()
        {
            return _connection
                .Query<TutorGroupDto>("dbo.Tutor_Group_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();

        }
        public TutorGroupDto GetTutorGroupById(int id)
        {
            return _connection
                   .QuerySingleOrDefault<TutorGroupDto>("dbo.Tutor_Group_SelectAll", new { id }, commandType: System.Data.CommandType.StoredProcedure);
        }
        public int DeleteTutorGroupsByIds(int userId, int groupId)
        {
            return _connection.Execute("dbo.Tutor_Group_Delete", new { userId, groupId }, commandType: System.Data.CommandType.StoredProcedure);
        }
        public int AddTutorToGroup(TutorGroupDto tutorGroup)
        {

            return _connection.Execute("dbo.Tutor_Group_Add",
                new
                {
                    userID = tutorGroup.UserID,
                    groupID = tutorGroup.GroupID,
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
        public StudentGroupDto GetStudentGroupById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection
                    .QuerySingleOrDefault<StudentGroupDto>("dbo.Student_Group_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            }

        }
        public int DeleteStudentGroupById(int userId, int groupId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Execute("dbo.Student_Group_Delete", new { userId, groupId }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int AddStudentGroup(StudentGroupDto studentGroup)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<int>("dbo.Student_Group_Add",
                    new
                    {
                        usertID = studentGroup.UserID,
                        groupID = studentGroup.GroupID,
                        contractNumber = studentGroup.ContractNumber
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public TeacherGroupDto GetTeacherGroupById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<TeacherGroupDto>("dbo.Teacher_Group_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int DeleteTeacherGroup(int userId, int groupId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Execute("dbo.Teacher_Group_Delete", new { userId, groupId }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int AddTeacherGroup(TeacherGroupDto teacherGroup)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<int>("dbo.Teacher_Group_Add",
                    new
                    {
                        userID = teacherGroup.UserID,
                        groupID = teacherGroup.GroupID
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public List<GroupReportDto> GenerateReport()
        {
            return _connection
            .Query<GroupReportDto>("dbo.Create_Report", commandType: System.Data.CommandType.StoredProcedure)
            .ToList();
        }

    }
}
