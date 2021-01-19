using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class TutorGroupRepository
    {
        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23"; 
        public List<TutorGroupDto> GetTutorGroups()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<TutorGroupDto>("dbo.Tutor_Group_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                    .ToList();
            }
                       
        }
        public TutorGroupDto GetTutorGroupById(int id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection
                    .QuerySingleOrDefault<TutorGroupDto>("dbo.Tutor_Group_SelectAll", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            }
            
            
        }
        public void DeleteTutorGroupsById(int id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute("dbo.Tutor_Group_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            }              
        }
        public StudentGroupDto AddTutorGroups(TutorGroupDto tutorGroup)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<StudentGroupDto>("dbo.Tutor_Group_Add",
                    new
                    {
                        userID = tutorGroup.UserID,
                        groupID = tutorGroup.GroupID,
                    },
                    commandType: System.Data.CommandType.StoredProcedure);             
            }
            
        }
    }
}
