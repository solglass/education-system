﻿using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class StudentGroupRepository : BaseRepository
    {             
        public List<StudentGroupDto> GetStudentGroups()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
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
        public void DeleteStudentGroupById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute("dbo.Student_Group_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
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
    }
}
