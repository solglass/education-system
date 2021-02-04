using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class GroupRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public GroupRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<GroupDto> GetGroups()
        {
            var result = _connection
                .Query<GroupDto, CourseDto, GroupStatusDto, GroupDto>("dbo.Group_SelectAll",
                    (group, course, groupStatus) =>
                    {
                        group.Course = course;
                        group.GroupStatus = groupStatus;
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
                .Query<GroupDto, CourseDto, GroupStatusDto, GroupDto>("dbo.Group_SelectById",
                    (group, course, groupStatus) =>
                    {
                        group.Course = course;
                        group.GroupStatus = groupStatus;
                        return group;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .FirstOrDefault();
            return result;
        }

        /*public GroupDto GetGroupByProgram(int id)
        {
            var result = _connection
                .Query<GroupDto, CourseDto, ThemeDto, GroupDto>("Group_SelectByProgram",
                    (group, course, theme) =>
                    {
                        group.Course = course;
                        course.Themes = theme;
                        return group;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .Distinct()
                .FirstOrDefault();
            return result;
        }*/

        public List<GroupDto> GetGroupsWithoutTutors()
        {
            var result = _connection
                .Query<GroupDto>("Group_SelectWithoutTutors", commandType: System.Data.CommandType.StoredProcedure)
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
                    StatusId = groupDto.GroupStatus.Id, 
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
                    StatusId = groupDto.GroupStatus.Id, 
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

        public List<GroupStatusDto> GetGroupStatus()
        {
            var groupStatus = _connection
                                .Query<GroupStatusDto>("dbo.GroupStatus_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                                .ToList();
            return groupStatus;
        }

        public GroupStatusDto GetGroupStatusById(int id)
        {
            var groupStatus = _connection
            .QuerySingleOrDefault<GroupStatusDto>("dbo.GroupStatus_SelectAll", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            return groupStatus;
        }

        public int AddGroupStatus(string Name)
        {
            var result = _connection
              .Execute("dbo.GroupStatuses_Add",
              new { Name },
              commandType: System.Data.CommandType.StoredProcedure);
            return result;

        }
        public int UpdateGroupStatus(GroupStatusDto groupStatus)
        {
            var result = _connection
                .Execute("dbo.GroupStatus_Update",
                new { groupStatus.Id, groupStatus.Name },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteGroupStatus(int id)
        {
            var result = _connection
                .Execute("dbo.GroupStatus_Delete",
                new { id },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
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
        public void DeleteTutorGroupsByIds(int userId, int groupId)
        {
            _connection.Execute("dbo.Tutor_Group_Delete", new { userId, groupId }, commandType: System.Data.CommandType.StoredProcedure);
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
        public List<StudentGroupDto> GetStudentGroups()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<StudentGroupDto>("dbo.Student_Group_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                    .ToList();
            }
        }
        public StudentGroupDto GetStudentGroupById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection
                    .QuerySingleOrDefault<StudentGroupDto>("dbo.Student_Group_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            }

        }
        public int DeleteStudentGroupById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Execute("dbo.Student_Group_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
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

        public List<TeacherGroupDto> GetTeacherGroups()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<TeacherGroupDto>("dbo.Teacher_Group_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            }
        }
        public TeacherGroupDto GetTeacherGroupById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<TeacherGroupDto>("dbo.Teacher_Group_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int DeleteTeacherGroup(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Execute("dbo.Teacher_Group_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
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
    }
}
