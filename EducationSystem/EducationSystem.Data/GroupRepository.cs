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

        public void UpdateGroup(GroupDto groupDto)
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
        }

        public int DeleteGroup(int Id)
        {
            var result = _connection
                .Execute("dbo.Group_Delete", 
                new 
                { 
                    Id 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int AddGroup_Material(int GroupID, int MaterialID)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Group_Material_Add", 
                new 
                { 
                    GroupID, 
                    MaterialID 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public void DeleteGroup_Material(int Id)
        {
            var result = _connection
                .Execute("dbo.Group_Material_Delete", 
                new 
                { 
                    Id 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
        }

        //GroupStatus
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
        public int UpdateGroupStatus(int id, string Name)
        {
            var result = _connection
                .Execute("dbo.GroupStatus_Update",
                new { id, Name },
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


    }
}
