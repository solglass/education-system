using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class GroupStatusRepository
    {
        private SqlConnection _connection;
        
        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        
        public GroupStatusRepository()
        {
            _connection = new SqlConnection(_connectionString);
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
            .QuerySingleOrDefault<GroupStatusDto>("dbo.GroupStatus_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            return groupStatus;
        }
                            
        public int AddGroupStatus(string Name)
        {
            var firstRow = _connection
            .QuerySingleOrDefault("dbo.GroupStatus_Add",
                new { Name },
                commandType: System.Data.CommandType.StoredProcedure);
            var data = (IDictionary<string, object>)firstRow;
            int value = Convert.ToInt32(data["LastId"]);
            return value;

            /*var result = _connection
              .QuerySingle<int>("dbo.GroupStatus_Add",
              new { Name },
              commandType: System.Data.CommandType.StoredProcedure);
            return result;*/

        }
        public int UpdateGroupStatus(int id, string Name)
        {
            var result = _connection
                .Execute("dbo.GroupStatus_Update",
                new { id, Name},
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
