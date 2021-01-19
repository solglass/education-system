using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    class HomeworkAttempt_AttachmentRepository
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
        public HomeworkAttempt_AttachmentDto GetHomeworkAttempt_attachmentById(int id)
        {
            var data = _connection.
                QueryFirstOrDefault<HomeworkAttempt_AttachmentDto>(
                "dbo.HomeworkAttempt_Attachment_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }
        public HomeworkAttempt_AttachmentDto AddHomeworkAttempt_attachment(HomeworkAttempt_AttachmentDto NewObject)
        {
            var data = _connection
                .QuerySingleOrDefault<HomeworkAttempt_AttachmentDto>("dbo.HomeworkAttempt_Attachment_Add",
                new
                {
                    HomeworkAttemptId = NewObject.HomeworkAttemptId,
                    AttachmentId = NewObject.AttachmentId
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return data;
        }
        public void DeleteHomeworkAttempt_AttachmentById(int id)
        {
            try
            {
                _connection.Query("dbo.HomeworkAttempt_Attachment_Delete", commandType: System.Data.CommandType.StoredProcedure);
            }
            catch
            {
                return;
            }
        }
    }
}
