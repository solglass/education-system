using EducationSystem.Data;
using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public class HomeworkService
    {
        private HomeworkRepository _homeworkRepository;
        private HomeworkAttemptRepository _homeworkAttemptRepository;
        public HomeworkService()
        {
            _homeworkRepository = new HomeworkRepository();
            _homeworkAttemptRepository = new HomeworkAttemptRepository();
        }

        public List<HomeworkDto> GetHomeworksByGroupId(int groupId)
        { 
            return _homeworkRepository.GetHomeworksByGroupId(groupId); 
        }

        public List<HomeworkDto> GetHomeworksByTagId(int tagId)
        {
            return _homeworkRepository.GetHomeworksByTagId(tagId);
        }

        public List<HomeworkDto> GetHomeworksByThemeId(int themeId)
        {
            return _homeworkRepository.GetHomeworksByThemeId(themeId);
        }

        public HomeworkDto GetHomeworkById(int id)
        {
            return _homeworkRepository.GetHomeworkById(id);
        }

        public int UpdateHomework(HomeworkDto homeworkDto)
        {
            return _homeworkRepository.UpdateHomework(homeworkDto);
        }

        public int AddHomework(HomeworkDto homeworkDto)
        {
            return _homeworkRepository.AddHomework(homeworkDto);
        }
        public int DeleteHomework(int id)
        {
            return _homeworkRepository.DeleteHomework(id);
        }

        public int AddHomework_Theme(int homeworkId, int themeId)
        {
            return _homeworkRepository.AddHomework_Theme(homeworkId, themeId);
        }

        public int DeleteHomework_Theme(int homeworkId, int themeId)
        {
            return _homeworkRepository.DeleteHomework_Theme(homeworkId, themeId);
        }

        public List<CommentDto> GetComments()
        {
            return _homeworkRepository.GetComments();
        }

        public CommentDto GetCommentById(int id)
        {
            return _homeworkRepository.GetCommentById(id);
        }



        public List<HomeworkAttemptDto> GetHomeworkAttemptsByHomeworkId(int id)
        {
            var dtos = _homeworkRepository.GetHomeworkAttemptsByHomeworkId(id);

            foreach (var item in dtos)
            {
                item.Comments = _homeworkRepository.GetCommentsByHomeworkAttemptId(item.Id);

                // check comments and then
                foreach (var comment in item.Comments)
                {
                    //comment.Attachments = _homeworkRepository.GetAttachmentsByCommentId
                }

                item.Attachments = _homeworkRepository.GetAttachmentsByHomeworkAttemptId(item.Id);
            }

            return dtos;
        }


        public List<HomeworkAttemptDto> GetHomeworkAttemptsAll()
        {
            var dtos = _homeworkRepository.GetHomeworkAttempts();

            foreach (var item in dtos)
            {
                item.Comments = _homeworkRepository.GetCommentsByHomeworkAttemptId(item.Id);

                // check comments and then
                foreach (var comment in item.Comments)
                {
                    //comment.Attachments = _homeworkRepository.GetAttachmentsByCommentId
                }

                item.Attachments = _homeworkRepository.GetAttachmentsByHomeworkAttemptId(item.Id);
            }

            return dtos;
        }

        public int AddHomeworkAttempt(HomeworkAttemptDto homeworkAttempt)
        {
            return _homeworkRepository.AddHomeworkAttempt(homeworkAttempt);
        }

        public int UpdateHomeworkAttempt(HomeworkAttemptDto homeworkAttempt)
        {
            return _homeworkRepository.UpdateHomeworkAttempt(homeworkAttempt);
        }

        public int DeleteHomeworkAttempt(int id)
        {
            return _homeworkRepository.DeleteHomeworkAttempt(id);
        }
        public int DeleteHomeworkAttemptAttachment(int homeworkAttemptId, int attachmentId)
        {
            return _homeworkAttemptRepository.DeleteHomeworkAttempt_Attachment(homeworkAttemptId, attachmentId);
        }
    }
}
