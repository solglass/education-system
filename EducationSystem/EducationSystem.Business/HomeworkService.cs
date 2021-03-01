using EducationSystem.Data;
using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public class HomeworkService
    {
        private HomeworkRepository _homeworkRepository;
        private HomeworkAttemptRepository _homeworkAttemptRepository;
        private TagRepository _tagRepository;
        public HomeworkService()
        {
            _homeworkRepository = new HomeworkRepository();
            _homeworkAttemptRepository = new HomeworkAttemptRepository();
            _tagRepository = new TagRepository();
        }

        public List<HomeworkDto> GetHomeworksByGroupId(int groupId)
        {
            return _homeworkRepository.SearchHomeworks(groupId, null, null);
        }

        public List<HomeworkDto> GetHomeworksByTagId(int tagId)
        {
            return _homeworkRepository.SearchHomeworks(null, null, tagId);
        }

        public List<HomeworkDto> GetHomeworksByThemeId(int themeId)
        {
            return _homeworkRepository.SearchHomeworks(null, themeId, null);
        }

        public HomeworkDto GetHomeworkById(int id)
        {
            var result = _homeworkRepository.GetHomeworkById(id);
            return result;
        }

        public int UpdateHomework(HomeworkDto homeworkDto)
        {
            return _homeworkRepository.UpdateHomework(homeworkDto);
        }

        public int AddHomework(HomeworkDto homeworkDto)
        {
            var result = _homeworkRepository.AddHomework(homeworkDto);
            homeworkDto.Themes.ForEach(theme =>
            {
                _homeworkRepository.AddHomework_Theme(result, theme.Id);
            });
            homeworkDto.Tags.ForEach(tag =>
            {
                _tagRepository.HomeworkTagAdd(new HomeworkTagDto() { HomeworkId = result, TagId = tag.Id });
            });
            return result;
        }

        public int DeleteHomework(int id)
        {
            bool isDeleted = true;
            return _homeworkRepository.DeleteOrRecoverHomework(id, isDeleted);
        }

        public int AddHomework_Theme(int homeworkId, int themeId)
        {
            return _homeworkRepository.AddHomework_Theme(homeworkId, themeId);
        }
        public int RecoverHomework(int id)
        {
            bool isDeleted = false;
            return _homeworkRepository.DeleteOrRecoverHomework(id, isDeleted);
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

        public int DeleteComment(int id)
        {
            bool isDeleted = true;
            return _homeworkRepository.DeleteOrRecoverComment(id, isDeleted);
        }

        public int RecoverComment(int id)
        {
            bool isDeleted = false;
            return _homeworkRepository.DeleteOrRecoverComment(id, isDeleted);
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
            bool isDeleted = true;
            return _homeworkRepository.DeleteOrRecoverHomeworkAttempt(id, isDeleted);
        }
        public int DeleteHomeworkAttemptAttachment(int homeworkAttemptId, int attachmentId)
        {
            return _homeworkAttemptRepository.DeleteHomeworkAttempt_Attachment(homeworkAttemptId, attachmentId);
        }

        public int RecoverHomeworkAttempt(int id)
        {
            bool isDeleted = false;
            return _homeworkRepository.DeleteOrRecoverHomeworkAttempt(id, isDeleted);
        }
        public List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByUserId(int id)
        {
          var dtos = _homeworkAttemptRepository.GetHomeworkAttemptsByUserId(id);
        
          return dtos;
        }
        public List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByStatusIdAndGroupId(int statusId, int groupId)
        {
          var dtos = _homeworkAttemptRepository.GetHomeworkAttemptsByStatusIdAndGroupId(statusId, groupId);
        
          return dtos;
        }

        public HomeworkAttemptDto GetHomeworkAttemptById(int id)
        {
            return _homeworkRepository.GetHomeworkAttemptById(id);
        }
    }
}
