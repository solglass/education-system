using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data.Models
{
    public class HomeworkAttemptRepository
    {
        private SqlConnection _connection;

        private string _connectionString = "Data Source=80.78.240.16;Initial Catalog=DevEdu;Persist Security Info=True;User ID=student;Password=qwe!23";
        public HomeworkAttemptRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<HomeworkAttemptDto> GetHomeworkAttempts()
        {
            var homeworkAttempt = _connection
                .Query<HomeworkAttemptDto>("dbo.HomeworkAttempt_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return homeworkAttempt;
        }

        public HomeworkAttemptDto GetHomeworkAttemptById(int id)
        {
            var homeworkAttempt = _connection
                .Query<HomeworkAttemptDto>("dbo.HomeworkAttempt_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return homeworkAttempt;
        }

        public int AddHomeworkAttempt(string comment, int userId, int homeworkAttemptId, int statusId)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttempt_Add",
                new { comment, userId, homeworkAttemptId, statusId },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int UpdateHomeworkAttempt(int id, string comment, int userId, int homeworkId, int statusId)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttempt_Update",
                new { id, comment, userId, homeworkId, statusId },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public int DeleteHomeworkAttempt(int id)
        {
            var result = _connection
                .Execute("dbo.HomeworkAttempt_Delete", 
                new { id }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
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
                "dbo.HomeworkAttempt_Attachment_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure);
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
