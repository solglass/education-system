using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace EducationSystem.Data
{
    public class AttachmentRepository
    {
        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public AttachmentRepository()
        {

        }

        public List<AttachmentDto> GetAttachments()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand 
            {
                CommandText = "dbo.Attachment_SelectAll", 
                Connection = sqlConnection,
                CommandType = System.Data.CommandType.StoredProcedure
            };

            var reader = sqlCommand.ExecuteReader();

            List<AttachmentDto> result = new List<AttachmentDto>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result.Add(new AttachmentDto 
                    {
                        Id = reader.GetInt32(0),
                        Path = reader.GetString(1),              
                        AttachmentTypeID = reader.GetInt32(2),
                        Name = reader.GetString(3)
                    });
                }
            }
            reader.Close();

            sqlConnection.Close();
            
            return result;
        }
    }
}
