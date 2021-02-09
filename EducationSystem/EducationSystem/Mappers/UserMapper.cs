using EducationSystem.API.Models.InputModels;
using EducationSystem.API.Models.OutputModels;
using EducationSystem.API.Utils;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Mappers
{
    public class UserMapper
    {
        public UserDto ToDto(UserInputModel inputModel) 
        {
            if (string.IsNullOrEmpty(inputModel.BirthDate))
            {
                throw new Exception("Ошибка! Не было передано значение BirthDate");
            }

            var (isBirthDateParsed, birthDate) = Converters.StrToDateTime(inputModel.BirthDate);

            if (!isBirthDateParsed)
            {
                throw new Exception("Ошибка! Неверный формат BirthDate");
            }
            return new UserDto {
                Id = inputModel.Id,
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                BirthDate = birthDate,
                Login = inputModel.Login,
                Password = inputModel.Password,
                Phone = inputModel.Phone,
                UserPic = inputModel.UserPic,
                Email = inputModel.Email
            };
        }
        public List<UserDto> ToDtos(List<UserInputModel> inputModels)
        {
            List<UserDto> result = new List<UserDto>();
            foreach (UserInputModel inputModel in inputModels)
            {
                result.Add(ToDto(inputModel));
            }

            return result;
        }
        public UserOutputModel FromDto(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new Exception("Пользователь не найден!");
            }
            return new UserOutputModel()
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                BirthDate = Converters.DateTimeToStr(userDto.BirthDate),
                Login = userDto.Login,
                Phone = userDto.Phone,
                UserPic = userDto.UserPic,
                Email = userDto.Email
            };
        }
        public List<UserOutputModel> FromDtos(List<UserDto> userDtos)
        {
            List<UserOutputModel> models = new List<UserOutputModel>();
            if (userDtos == null || userDtos.Count == 0)
            {
                throw new Exception("Пользователи не найдены!");
            }
            foreach (var userDto in userDtos)
            {
                models.Add(FromDto(userDto));
            }
            return models;
        }
    }
}