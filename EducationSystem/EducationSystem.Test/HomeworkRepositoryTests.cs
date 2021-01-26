using EducationSystem.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
    public class HomeworkRepositoryTests
    {
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
        public List<HomeworkAttemptStatusDto> GetHomeworkAttemptStatusMock(int n)
        {
            List<HomeworkAttemptStatusDto> result = new List<HomeworkAttemptStatusDto>();
            switch (n)
            {
                case 1:
                    return result;
                case 2:
                    result.Add(new HomeworkAttemptStatusDto { Name= "Test tag 1" });
                    return result;
                case 3:
                    result.Add(new HomeworkAttemptStatusDto { Name = "Test tag 2" });
                    result.Add(new HomeworkAttemptStatusDto { Name = "Test tag 3" });
                    return result;
                default:
                    return result;
            }
        }
        public List<UserDto> UserMock(int n)
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
        public HomeworkDto GetCourseMock(int n)
        {
            HomeworkDto result = new HomeworkDto();
            switch (n)
            {
                case 1:
                    result = new HomeworkDto() { Description = "Test case 1", StartDate = new DateTime(2021, 1, 5), DeadlineDate = new DateTime(2021, 1, 11), GroupId = 1 , IsOptional = true};
                    return result;
                case 2:
                    result = new HomeworkDto() { Description = "Test case 2", StartDate = new DateTime(2021, 1, 12), DeadlineDate = new DateTime(2021, 1, 19), GroupId = 1, IsOptional = true };
                    return result;
                case 3:
                    result = new HomeworkDto() { Description = "Test case 3", StartDate = new DateTime(2021, 1, 20), DeadlineDate = new DateTime(2021, 1, 25), GroupId = 2, IsOptional = false };
                    return result;
                default:
                    return result;
            }
        }
    }
}
