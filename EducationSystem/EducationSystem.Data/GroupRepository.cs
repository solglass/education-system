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
            var groups = _connection
                .Query<GroupDto>("dbo.Group_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return groups;
        }

        public GroupDto GetGroupById(int id)
        {
            var group = _connection
                .Query<GroupDto>("dbo.Group_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return group;
        }

        public void GetGroupAdd(int CourseID, int StatusId, string StartDate)
        {
            var materials = _connection
                .Execute("dbo.Group_Add", new { CourseID, StatusId, StartDate }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void GetGroupUpdate(int Id, int CourseID, int StatusId, string StartDate)
        {
            var materials = _connection
                .Execute("dbo.Group_Update", new { Id, CourseID, StatusId, StartDate }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void GetGroupDelete(int Id)
        {
            var materials = _connection
                .Execute("dbo.Group_Delete", new { Id }, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
