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

        public AttachmentTypeDto GetAttachmentTypeById(int id)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentTypeDto>("dbo.AttachmentType_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
                return data;
        }

        public AttachmentTypeDto ModifyAttachmentType(int id, string name)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentTypeDto>("dbo.AttachmentType_Update", new { id, name }, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }

        public void  DeleteAttachmentTypeById(int id)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentTypeDto>("dbo.AttachmentType_Delete", new { id }, commandType: System.Data.CommandType.StoredProcedure);
        }


        public AttachmentTypeDto AddAttachmentType(AttachmentTypeDto NewObject)
        {
            var data = _connection
                .QuerySingleOrDefault<AttachmentTypeDto>("dbo.AttachmentType_Add",
                new { name = NewObject.Name}, commandType: System.Data.CommandType.StoredProcedure);
            return data;

        }
    }
}
