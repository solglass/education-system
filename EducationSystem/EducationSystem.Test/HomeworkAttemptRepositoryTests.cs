using EducationSystem.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class HomeworkAttemptRepositoryTests
    {
        private UserRepository _userRepo;
        private UserDto _userDto;
        private HomeworkRepository _homeworkRepo;
        private HomeworkDto _homeworkDto;
        private AttachmentRepository _attachmentRepo;
        private List<AttachmentDto> _attachmentDtos; 
        private List<CommentDto> _commentDtos;

        [SetUp]
        public void AttemptSetUp()
        {
            _userRepo = new UserRepository();
            _homeworkRepo = new HomeworkRepository();
            _attachmentRepo = new AttachmentRepository();
        }
    }
}
