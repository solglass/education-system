﻿using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public class HomeworkService : IHomeworkService
    {
        private IHomeworkRepository _homeworkRepository;
        private ITagRepository _tagRepository;
        private IGroupRepository _groupRepository;
        private ICourseRepository _courseRepository;
        private IAttachmentRepository _attachmentRepository;
        public HomeworkService(IHomeworkRepository homeworkRepository,
            ITagRepository tagRepository,
            ICourseRepository courseRepository,
            IGroupRepository groupRepository,
            IAttachmentRepository attachmentRepository)
        {
            _courseRepository = courseRepository;
            _tagRepository = tagRepository;
            _homeworkRepository = homeworkRepository;
            _groupRepository = groupRepository;
            _attachmentRepository = attachmentRepository;
        }

        public List<HomeworkDto> GetHomeworksByCourseId(int courseId)
        {
            var result = _homeworkRepository.SearchHomeworks(courseId: courseId);
            result.ForEach(h =>
            new Action(delegate ()
            {
                h.Groups = _groupRepository.GetGroupsByHomeworkId(h.Id);
                h.Course = _courseRepository.GetCourseById(h.Course.Id);
                h.HomeworkAttempts = _homeworkRepository.GetHomeworkAttemptsByHomeworkId(h.Id);
            }).Invoke());

            return result;
        }
        public List<HomeworkDto> GetHomeworksByGroupId(int groupId)
        {
            var result = _homeworkRepository.GetHomeworksByGroupId(groupId);

            result.ForEach(h =>
            new Action(delegate () 
            {
                h.Themes = _courseRepository.GetThemesByHomeworkId(h.Id);
                h.Tags = _tagRepository.GetTagsByHomeworkId(h.Id);
                h.Course = _courseRepository.GetCourseById(h.Course.Id);
                h.HomeworkAttempts = _homeworkRepository.GetHomeworkAttemptsByHomeworkId(h.Id);
            }).Invoke());


            return result;
        }

        public List<HomeworkDto> GetHomeworksByTagId(int tagId)
        {
            var result = _homeworkRepository.SearchHomeworks(tagId: tagId);

            result.ForEach(h =>
            new Action(delegate ()
            {
                h.Groups = _groupRepository.GetGroupsByHomeworkId(h.Id);
                h.Course = _courseRepository.GetCourseById(h.Course.Id);
                h.HomeworkAttempts = _homeworkRepository.GetHomeworkAttemptsByHomeworkId(h.Id);
            }).Invoke());


            return result;
        }

        public List<HomeworkDto> GetHomeworksByThemeId(int themeId)
        {
            var result = _homeworkRepository.SearchHomeworks(themeId: themeId);

            result.ForEach(h =>
            new Action(delegate ()
            {
                h.Groups = _groupRepository.GetGroupsByHomeworkId(h.Id);
                h.Course = _courseRepository.GetCourseById(h.Course.Id);
                h.HomeworkAttempts = _homeworkRepository.GetHomeworkAttemptsByHomeworkId(h.Id);
            }).Invoke());

            return result;
        }
        public List<HomeworkDto> GetHomeworks()
        {
            var result = _homeworkRepository.GetHomeworks();

            result.ForEach(h =>
            new Action(delegate ()
            {
                h.Groups = _groupRepository.GetGroupsByHomeworkId(h.Id);
                h.Course = _courseRepository.GetCourseById(h.Course.Id);
                h.HomeworkAttempts = _homeworkRepository.GetHomeworkAttemptsByHomeworkId(h.Id);
            }).Invoke());

            return result;
        }

        public HomeworkDto GetHomeworkById(int id)
        {
            var result = _homeworkRepository.GetHomeworkById(id);
            result.Course = _courseRepository.GetCourseById(result.Course.Id);
            result.Groups = _groupRepository.GetGroupsByHomeworkId(id);
            result.HomeworkAttempts = _homeworkRepository.GetHomeworkAttemptsByHomeworkId(id);
            return result;
        }

        public int UpdateHomework(int homeworkId, HomeworkDto homeworkDto)
        {
            homeworkDto.Id = homeworkId;
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
                _homeworkRepository.HomeworkTagAdd(result,  tag.Id );
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

        public List<CommentDto> GetCommentsByHomeworkAttemptId(int homeworkAttamptId)
        {
            return _homeworkRepository.SearchComments(homeworkAttamptId, null);
        }
        public List<CommentDto> GetCommentsByHomeworkId(int homeworkId)
        {
            return _homeworkRepository.SearchComments(null, homeworkId);
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
                item.Comments = _homeworkRepository.SearchComments(item.Id, null);

                // check comments and then
                foreach (var comment in item.Comments)
                {
                    //comment.Attachments = _homeworkRepository.GetAttachmentsByCommentId
                }

                item.Attachments = _attachmentRepository.GetAttachmentsByHomeworkAttemptId(item.Id);
            }

            return dtos;
        }


        public int AddHomeworkAttempt(int homeworkId, HomeworkAttemptDto homeworkAttempt)
        {
            homeworkAttempt.Homework = new HomeworkDto { Id = homeworkId };
            return _homeworkRepository.AddHomeworkAttempt(homeworkAttempt);
        }

        public int UpdateHomeworkAttempt(int attemptId, HomeworkAttemptDto homeworkAttempt)
        {
            homeworkAttempt.Id = attemptId;
            return _homeworkRepository.UpdateHomeworkAttempt(homeworkAttempt);
        }

        public int DeleteHomeworkAttempt(int id)
        {
            bool isDeleted = true;
            return _homeworkRepository.DeleteOrRecoverHomeworkAttempt(id, isDeleted);
        }
        public int RecoverHomeworkAttempt(int id)
        {
            bool isDeleted = false;
            return _homeworkRepository.DeleteOrRecoverHomeworkAttempt(id, isDeleted);
        }
        public List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByUserId(int id)
        {
            var dtos = _homeworkRepository.GetHomeworkAttemptsByUserId(id);

            return dtos;
        }
        public List<HomeworkAttemptWithCountDto> GetHomeworkAttemptsByStatusIdAndGroupId(int statusId, int groupId)
        {
            var dtos = _homeworkRepository.GetHomeworkAttemptsByStatusIdAndGroupId(statusId, groupId);

            return dtos;
        }

        public HomeworkAttemptDto GetHomeworkAttemptById(int id)
        {
            return _homeworkRepository.GetHomeworkAttemptById(id);
        }

        public int AddComment(int attemptId, CommentDto comment)
        {

            comment.HomeworkAttempt = new HomeworkAttemptDto() { Id = attemptId };
            return _homeworkRepository.AddComment(comment);
        }

        public int UpdateComment(int attemptId, int commentId, CommentDto comment)
        {

            comment.Id = commentId;
            comment.HomeworkAttempt = new HomeworkAttemptDto { Id = attemptId };
            var result = _homeworkRepository.UpdateComment(comment);
            return result;
        }

        public int AddHomeworkTag(int homeworkId, int tagId)
        {
            return _homeworkRepository.HomeworkTagAdd(homeworkId, tagId);
        }
        public int DeleteHomeworkTag(int homeworkId, int tagId)
        { 
            return _homeworkRepository.HomeworkTagDelete(homeworkId, tagId);
        }
        public int AddHomeworkGroup(int homeworkId, int groupId)
        {
            return _homeworkRepository.AddHomework_Group(homeworkId, groupId);
        }
        public int DeleteHomeworkGroup(int homeworkId, int groupId)
        {
            return _homeworkRepository.DeleteHomework_Group(homeworkId, groupId);
        }
    }
}
