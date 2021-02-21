﻿using EducationSystem.Data;
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

        public List<HomeworkDto> GetHomeworks()
        {
            return _homeworkRepository.GetHomeworks();
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

        public int RecoverHomework(int id)
        {
            return _homeworkRepository.RecoverHomework(id);
        }

        public int HardDeleteHomework(int id)
        {
            return _homeworkRepository.HardDeleteHomework(id);
        }

        //public int AddHomework_Theme(HomeworkThemeDto homeworkThemeDto)
        //{
        //    return _homeworkRepository.AddHomework_Theme(homeworkThemeDto);
        //}

        public int DeleteHomework_Theme(int id)
        {
            return _homeworkRepository.DeleteHomework_Theme(id);
        }
        public List<HomeworkAttemptStatusDto> GetHomeworkAttemptStatuses()
        {
            return _homeworkRepository.GetHomeworkAttemptStatuses();
        }

        public int DeleteHomeworkAttemptStatus(int id)
        {
            return _homeworkRepository.DeleteHomeworkAttemptStatus(id);
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

        public int RecoverHomeworkAttempt(int id)
        {
            return _homeworkRepository.RecoverHomeworkAttempt(id);
        }

        public int HardDeleteHomeworkAttempt(int id)
        {
            return _homeworkRepository.HardDeleteHomeworkAttempt(id);
        }
    }
}
