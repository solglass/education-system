using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace EducationSystem.Data
{
    public class AttachmentTypeRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public AttachmentTypeRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<AttachmentTypeDto> GetAttachmentTypes()
        {
            var data = _connection
                .Query<AttachmentTypeDto>("dbo.AttachmentType_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return data;


        }
    }
}
