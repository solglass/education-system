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

        public void GetGroupAdd(GroupDto groupDto)
        {
            var result = _connection
                .Query<GroupDto>("dbo.Group_Add", 
                new 
                { 
                    CourseID = groupDto.Course.Id, 
                    StatusId = groupDto.GroupStatus.Id, 
                    StartDate = groupDto.StartDate 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public void GetGroupUpdate(GroupDto groupDto)
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

        public void GetGroupDelete(int Id)
        {
            var result = _connection
                .Execute("dbo.Group_Delete", 
                new 
                { 
                    Id 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public void GetGroup_MaterialAdd(int GroupID, int MaterialID)
        {
            var result = _connection
                .Query<Group_MaterialDto>("dbo.Group_Material_Add", 
                new 
                { 
                    GroupID, 
                    MaterialID 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public void GetGroup_MaterialDelete(int Id)
        {
            var result = _connection
                .Execute("dbo.Group_Material_Delete", 
                new 
                { 
                    Id 
                }, 
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
