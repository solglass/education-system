using EducationSystem.Data;
using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public class HomeworkService
    {
        private HomeworkRepository _homeworkRepository;

        public HomeworkService()
        {
            _homeworkRepository = new HomeworkRepository();
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
    }
}
