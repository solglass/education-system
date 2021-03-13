using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;

namespace EducationSystem.Data.Models
{
    public class UserDto: ICloneable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string UserPic { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public List<Role> Roles { get; set; }

        public object Clone()
        {
            return new UserDto()
            {
                Email = Email,
                FirstName = FirstName,
                BirthDate = BirthDate,
                IsDeleted = IsDeleted,
                LastName = LastName,
                Password = Password,
                Phone = Phone,
                UserPic = UserPic,
                Login = Login,
            };
        }

        public override bool Equals(object obj)
        {
            var userDto = (UserDto)obj;
            if ((userDto.Id == Id) && String.Equals(userDto.FirstName, FirstName) && String.Equals(userDto.LastName, LastName) && userDto.BirthDate == BirthDate && String.Equals(userDto.Login, Login) && String.Equals(userDto.Phone, Phone)&&String.Equals(userDto.Email, Email))
                return true;
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string s = "";

            s += Id + " " + FirstName + " " + LastName + " " + Login + "; ";
            return s;
        }
    }
}
