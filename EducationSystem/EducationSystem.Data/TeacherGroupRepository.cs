using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class TeacherGroupRepository : BaseRepository
    {     
        public List<TeacherGroupDto> GetTeacherGroups()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
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
        public void DeleteTeacherGroup(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute("dbo.Teacher_Group_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);        
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
