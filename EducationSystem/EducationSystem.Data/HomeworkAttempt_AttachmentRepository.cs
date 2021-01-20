using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class HomeworkAttempt_AttachmentRepository
    {
        private SqlConnection _connection;
        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";

        public HomeworkAttempt_AttachmentRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }
        public List<HomeworkAttempt_AttachmentDto> GetHomeworkAttempt_Attachments()
        {
            var data = _connection.
                Query<HomeworkAttempt_AttachmentDto>(
                "dbo.HomeworkAttempt_Attachment_SelectAll",
                commandType: System.Data.CommandType.StoredProcedure).ToList();
            return data;
        }
        public HomeworkAttempt_AttachmentDto GetHomeworkAttempt_AttachmentById(int id)
        {
            var data = _connection.
                QueryFirstOrDefault<HomeworkAttempt_AttachmentDto>(
                "dbo.HomeworkAttempt_Attachment_SelectById", new { id } , commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }
        public int AddHomeworkAttempt_Attachment(HomeworkAttempt_AttachmentDto newObject)
        {
            var data = _connection
                .QueryFirst<int>("dbo.HomeworkAttempt_Attachment_Add",
                new
                {
                    HomeworkAttemptId = newObject.HomeworkAttemptId,
                    AttachmentId = newObject.AttachmentId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }
        public int DeleteHomeworkAttempt_AttachmentById(int id)
        {
            int rowsAffected = _connection.Execute(
                "dbo.HomeworkAttempt_Attachment_Delete",
                new { id },
                commandType: System.Data.CommandType.StoredProcedure);
            return rowsAffected;
        }
    }
}
