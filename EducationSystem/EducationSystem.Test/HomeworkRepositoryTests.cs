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
        private GroupRepository _groupRepo;
        private CourseRepository _courseRepo;
        private UserRepository _userRepo;
        private TagRepository _tagRepo;
        private HomeworkAttemptRepository _homeworkAttemptRepo;
        private int _homeworkId;
        private List<int> _groupIdList;
        private List<int> _themeIdList;
        private List<int> _tagIdList;
        private List<int> _userIdList;
        private List<int> _homeworkAttemptIdIdList;
        private HomeworkDto _expectedHomework;
        private List<HomeworkDto> _homeworkFromDb;

        [OneTimeSetUp]
        public void SetUpTest()
        {
            _groupRepo = new GroupRepository();
            _homeworkRepo = new HomeworkRepository();
            _userRepo = new UserRepository();
            _courseRepo = new CourseRepository();
            _tagRepo = new TagRepository();
            _homeworkAttemptRepo = new HomeworkAttemptRepository();
            _homeworkFromDb = new List<HomeworkDto>();
            _groupIdList = new List<int>();
            _themeIdList = new List<int>();
            _userIdList = new List<int>();
            _homeworkAttemptIdIdList = new List<int>();



            foreach (int groupId in _groupIdList)
            {
                _groupRepo.DeleteGroup(groupId);
            }
            foreach (int userId in _userIdList)
            {
                _userRepo.DeleteUser();
            }
            foreach (int tagId in _tagIdList)
            {
                _tagRepo.TagDelete(tagId);
            }
            foreach (int userId in _userIdList)
            {
                _userRepo.DeleteUser();
            }

        }

        [TestCase(1), Order(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void TestAddHomeworkAttempt(int homeworkAttemptMock)
        {
            List<HomeworkAttemptDto> homeworkAttempts = GetHomeworkAttemptMock(homeworkAttemptMock);


        }

        [OneTimeTearDown]
        public void TearDowTest()
        {
            deleteGroups();
            deleteUsers();
            deleteTags();
            deleteThemes();
        }

        public void deleteGroups()
        {
            foreach (int groupId in _groupIdList)
            {
                _groupRepo.DeleteGroup(groupId);
            }
        }
        public void deleteUsers()
        {
            foreach (int userId in _userIdList)
            {
                _userRepo.DeleteUser();
            }
        }
        public void deleteTags()
        {
            foreach (int tagId in _tagIdList)
            {
                _tagRepo.TagDelete(tagId);
            }
        }
        public void deleteThemes()
        {
            foreach (int themeId in _themeIdList)
            {
                _courseRepo.DeleteTheme(themeId);
            }
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
                        Comment = "Test comment 1",
                        HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = 1, Name = "Test status 1" },
                        IsDeleted = false
                    });

                    _userIdList.Add(_userRepo.AddUser().Id);
                    result[result.Count - 1].Author = (GetUserMock(n));
                    result[result.Count - 1].Author.Id = _userIdList[_userIdList.Count - 1];

                    return result;
                case 3:
                    result.Add(new HomeworkAttemptDto
                    {
                        Comment = "Test comment 1",
                        HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = 1, Name = "Test status 1" },
                        IsDeleted = false
                    });

                    _userIdList.Add(_userRepo.AddUser().Id);
                    result[result.Count - 1].Author = (GetUserMock(n));
                    result[result.Count - 1].Author.Id = _userIdList[_userIdList.Count - 1];

                    foreach(TagDto tag in GetTagMock(n))
                    _tagIdList.Add(_tagRepo.TagAdd().Id);
                    result[result.Count - 1].Author = (GetUserMock(n));
                    result[result.Count - 1].Author.Id = _userIdList[_userIdList.Count - 1];

                    result.Add(new HomeworkAttemptDto
                    {
                        Comment = "Test comment 1",
                        HomeworkAttemptStatus = new HomeworkAttemptStatusDto { Id = 1, Name = "Test status 1" },
                        IsDeleted = false
                    });
                    _userIdList.Add(_userRepo.AddUser().Id);
                    result[result.Count - 1].Author = (GetUserMock(n + 1));
                    result[result.Count - 1].Author.Id = _userIdList[_userIdList.Count - 1];

                    return result;
                default:
                    return result;
            }
        }

        public UserDto GetUserMock(int n)
        {
            UserDto result = new UserDto();
            switch (n)
            {
                case 1:
                    return result;
                case 2:
                    result = (new UserDto 
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
                    result=(new UserDto
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
                    return result;
                case 4:

                    result = (new UserDto
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
                    _groupIdList.Add(_groupRepo.AddGroup(GetGroupMock(n)));
                    result.GroupId = _groupIdList[_groupIdList.Count - 1];

                    foreach(TagDto tag in GetTagMock(n))
                    {
                        _tagIdList.Add(_tagRepo.TagAdd(tag));
                        result.Tags.Add(new TagDto { Id = _tagIdList[_tagIdList.Count - 1], Name = tag.Name });
                    }
                    
                    foreach (ThemeDto theme in GetThemeMock(n))
                    {
                        _themeIdList.Add(_courseRepo.AddTheme(theme.Name));
                        result.Themes.Add(new ThemeDto { Id = _themeIdList[_themeIdList.Count - 1], Name = theme.Name });
                    }
                    foreach (HomeworkAttemptDto homeworkAttempt in GetHomeworkAttemptMock(n))
                    {
                        _homeworkAttemptIdIdList.Add(_homeworkRepo.AddHomeworkAttempt(homeworkAttempt));
                        result.Themes.Add(new ThemeDto { Id = _themeIdList[_themeIdList.Count - 1], Name = theme.Name });
                    }

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


        public GroupDto GetGroupMock(int n)
        {
            GroupDto groups = new GroupDto();
            switch (n)
            {
                case 1:
                    return groups;
                case 2:
                    groups = (new GroupDto { GroupStatus = new GroupStatusDto() { Id = 1 }, Course = new CourseDto(), StartDate = new System.DateTime(2020, 10, 12) });
                    return groups;
                case 3:
                    groups = (new GroupDto { GroupStatus = new GroupStatusDto() { Id = 1 }, Course = new CourseDto(), StartDate = new System.DateTime(2020, 10, 12) });
                    return groups;
                case 4:
                    groups = (new GroupDto { GroupStatus = new GroupStatusDto() { Id = 1 }, Course = new CourseDto(), StartDate = new System.DateTime(2021, 10, 12) });

                    return groups;
                default:
                    return groups;
            }
        }
    }
}
