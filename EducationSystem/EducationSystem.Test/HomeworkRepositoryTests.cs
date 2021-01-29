using EducationSystem.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class HomeworkRepositoryTests
    {
        private HomeworkRepository _homeworkRepo;
        private CourseRepository _courseRepo;
        private UserRepository _userRepo;
        private TagRepository _tagRepo;
        private HomeworkAttemptRepository _homeworkAttemptRepo;
        private int _homeworkId;
        private List<int> _groupIdList;
        private List<int> _themeIdList;
        private List<int> _tagIdList;
        private HomeworkDto _expectedHomework;
        private List<HomeworkDto> _homeworkFromDb;

        [OneTimeSetUp]
        public void SetUpTest()
        {
            _homeworkRepo = new HomeworkRepository();
            _userRepo = new UserRepository();
            _courseRepo = new CourseRepository();
            _tagRepo = new TagRepository();
            _homeworkAttemptRepo = new HomeworkAttemptRepository();
            _homeworkFromDb = new List<HomeworkDto>();
            _groupIdList = new List<int>();
            _themeIdList = new List<int>();
            _expectedHomework = GetHomeworkMock(1);
            _expectedHomework.HomeworkAttempts = GetHomeworkAttemptMock(1);
            _expectedHomework.Themes = GetThemeMock(3);
            _expectedHomework.Tags = GetTagMock(3);
            foreach (var theme in _expectedHomework.Themes)
            {
                _themeIdList.Add(_courseRepo.AddTheme(theme.Name));
            }
            foreach (var tag in _expectedHomework.Tags)
            {
                _tagIdList.Add(_tagRepo.TagAdd(tag));
            }
            _homeworkFromDb.AddRange(_homeworkRepo.GetHomeworks());
        }

        public List<ThemeDto> GetThemeMock(int n)
        {
            List<ThemeDto> result = new List<ThemeDto>();
            switch (n)
            {
                case 1:
                    return result;
                case 2:
                    result.Add(new ThemeDto { Name="Test theme 1"});
                    return result;
                case 3:
                    result.Add(new ThemeDto { Name = "Test theme 2" });
                    result.Add(new ThemeDto { Name = "Test theme 3" });
                    return result;
                default:
                    return result;
            }
        }
        public List<TagDto> GetTagMock(int n)
        {
            List<TagDto> result = new List<TagDto>();
            switch (n)
            {
                case 1:
                    return result;
                case 2:
                    result.Add(new TagDto { Name= "Test tag 1" });
                    return result;
                case 3:
                    result.Add(new TagDto { Name = "Test tag 2" });
                    result.Add(new TagDto { Name = "Test tag 3" });
                    return result;
                default:
                    return result;
            }
        }
        public List<HomeworkAttemptDto> GetHomeworkAttemptMock(int n)
        {
            List<HomeworkAttemptDto> result = new List<HomeworkAttemptDto>();
            switch (n)
            {
                case 1:
                    return result;
                case 2:
                    result.Add(new HomeworkAttemptDto
                    { 
                        Comment = "Test comment 1" ,
                        HomeworkId  = 1,
                        HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = 1, Name = "Test status 1" },
                        IsDeleted = false
                    });
                    return result;
                case 3:
                    result.Add(new HomeworkAttemptDto
                    {
                        Comment = "Test comment 1",
                        HomeworkId = 2,
                        HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = 1, Name = "Test status 1" },
                        IsDeleted = false
                    });
                    result.Add(new HomeworkAttemptDto
                    {
                        Comment = "Test comment 1",
                        HomeworkId = 1,
                        HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = 1, Name = "Test status 1" },
                        IsDeleted = false
                    });
                    return result;
                default:
                    return result;
            }
        }

        public List<UserDto> GetUserMock(int n)
        {
            List<UserDto> result = new List<UserDto>();
            switch (n)
            {
                case 1:
                    return result;
                case 2:
                    result.Add(new UserDto 
                    {
                        FirstName = "Петр", 
                        LastName = "Петров", 
                        BirthDate = new DateTime(1980,3,12),
                        Login = "Petr01", 
                        Password = "qqq123", 
                        Phone = "89825553535",
                        UserPic = "ddsa",
                        Email = "Petr.Petrov@mail.ru",
                        IsDeleted = false 
                    });
                    return result;
                case 3:
                    result.Add(new UserDto
                    {
                        FirstName = "Вася",
                        LastName = "Васильев",
                        BirthDate = new DateTime(1980, 7, 12),
                        Login = "Vasya01",
                        Password = "qqq123",
                        Phone = "8982543535",
                        UserPic = "ddsasda",
                        Email = "Vasya.Vaskin@mail.ru",
                        IsDeleted = false
                    });
                    result.Add(new UserDto
                    {
                        FirstName = "Максим",
                        LastName = "Максимов",
                        BirthDate = new DateTime(1982, 1, 11),
                        Login = "Max01",
                        Password = "qqq123",
                        Phone = "8982552535",
                        UserPic = "ddsa",
                        Email = "Max.Maximov@mail.ru",
                        IsDeleted = false
                    });
                    return result;
                default:
                    return result;
            }
        }
        public HomeworkDto GetHomeworkMock(int n)
        {
            HomeworkDto result = new HomeworkDto();
            switch (n)
            {
                case 1:
                    result = new HomeworkDto() { Description = "Test case 1", StartDate = new DateTime(2021, 1, 5), DeadlineDate = new DateTime(2021, 1, 11), IsOptional = true};
                    return result;
                case 2:
                    result = new HomeworkDto() { Description = "Test case 2", StartDate = new DateTime(2021, 1, 12), DeadlineDate = new DateTime(2021, 1, 19),  IsOptional = true };
                    return result;
                case 3:
                    result = new HomeworkDto() { Description = "Test case 3", StartDate = new DateTime(2021, 1, 20), DeadlineDate = new DateTime(2021, 1, 25), IsOptional = false };
                    return result;
                default:
                    return result;
            }
        }


        public List<GroupDto> GetGroupMock(int n)
        {
            List<GroupDto> groups = new List<GroupDto>();
            switch (n)
            {
                case 1:
                    return groups;
                case 2:
                    groups.Add(new GroupDto { GroupStatus = new GroupStatusDto() { Id = 1 }, Course = new CourseDto(), StartDate = new System.DateTime(2020, 10, 12) });
                    return groups;
                case 3:
                    groups.Add(new GroupDto { GroupStatus = new GroupStatusDto() { Id = 1 }, Course = new CourseDto(), StartDate = new System.DateTime(2020, 10, 12) });
                    groups.Add(new GroupDto { GroupStatus = new GroupStatusDto() { Id = 1 }, Course = new CourseDto(), StartDate = new System.DateTime(2021, 10, 12) });

                    return groups;
                default:
                    return groups;
            }
        }
    }
}
