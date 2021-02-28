using Dapper;
using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IOptions<AppSettingsConfig> options): base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<UserDto> GetUsers()
        {
            var UserDictionary = new Dictionary<int, UserDto>();


            var users = _connection.
                Query<UserDto, int, UserDto>(
                "dbo.User_SelectAll",
                (user, role) =>
                {


                    if (!UserDictionary.TryGetValue(user.Id, out UserDto userEntry))
                    {
                        userEntry = user;
                        userEntry.Roles = new List<Role>();
                        UserDictionary.Add(userEntry.Id, userEntry);
                    }

                    userEntry.Roles.Add((Role)role);
                    return userEntry;
                },
                splitOn: "Id", commandType: System.Data.CommandType.StoredProcedure)
            .ToList();
            return users;

        }
        public List<UserDto> GetPassedStudentsAttempt_SelectByGroupId(int groupId)
        {
            var UserDictionary = new Dictionary<int, UserDto>();


            var users = _connection.
                Query<UserDto, int, UserDto>(
                "dbo.PassedStudentsAttempt_SelectByGroupId",
                (user, role) =>
                {


                    if (!UserDictionary.TryGetValue(user.Id, out UserDto userEntry))
                    {
                        userEntry = user;
                        userEntry.Roles = new List<Role>();
                        UserDictionary.Add(userEntry.Id, userEntry);
                    }

                    userEntry.Roles.Add((Role)role);
                    return userEntry;
                },
                new { groupId },
                splitOn: "Id", commandType: System.Data.CommandType.StoredProcedure)
            .ToList();
            return users;

        }
        public UserDto GetUserById(int id)
        {
            var UserDictionary = new Dictionary<int, UserDto>();
            var users = _connection.
                Query<UserDto, int, UserDto>(
                "dbo.User_SelectById",
                (user, role) =>
                {
                    if (!UserDictionary.TryGetValue(user.Id, out UserDto userEntry))
                    {
                        userEntry = user;
                        userEntry.Roles = new List<Role>();
                        UserDictionary.Add(userEntry.Id, userEntry);
                    }

                    userEntry.Roles.Add((Role)role);
                    return userEntry;
                },
               new { id },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
            .FirstOrDefault();
            return users;
        }
        //public List<UserDto> PassedStudentsAttempt_SelectByGroupId(int groupId)
        //{
        //    var UserDictionary = new Dictionary<int, UserDto>();
        //}

        public int ChangeUserPassword(int id, string oldPassword, string newPassword)
        {

            return _connection
               .Execute("dbo.User_Change_Password", new
               {
                   id,
                   oldPassword,
                   newPassword

               }, commandType: System.Data.CommandType.StoredProcedure);

        }

        public int AddUser(UserDto user)
        {
            return _connection
                .QuerySingle<int>("dbo.User_Add", new
                {
                    user.FirstName,
                    user.LastName,
                    user.BirthDate,
                    user.Login,
                    user.Password,
                    user.Phone,
                    user.UserPic,
                    user.Email,
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }
        public int UpdateUser(UserDto user)
        {
            return _connection
                .Execute("dbo.User_Update", new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.BirthDate,
                    user.Phone,
                    user.UserPic,
                    user.Email
                }, commandType: System.Data.CommandType.StoredProcedure);
        }
        public int DeleteOrRecoverUser(int id, bool isDeleted)
        {
            return _connection
                .Execute("dbo.User_DeleteOrRecover",
                new 
                {
                  id,
                  isDeleted
                },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public int HardDeleteUser(int id)
        {
            return _connection
                .Execute("dbo.User_HardDelete", new { id },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public int AddRoleToUser(UserRoleDto userRole)
        {
            return _connection
               .QuerySingle<int>("dbo.RoleToUser_Add",
               new { userRole.UserId, userRole.RoleId },
               commandType: System.Data.CommandType.StoredProcedure);
        }

        public int DeleteRoleToUser(int id)
        {
            return _connection
                .Execute("dbo.RoleToUser_Delete", new { id },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public UserDto CheckUser(string login)
        {
            var userEntry = new UserDto();
            var result = _connection.
                Query<UserDto, int, UserDto>(
                "dbo.Check_User_Authentication",
                (user, role) =>
                {
                    if (userEntry.Id == 0)
                    {
                        userEntry = user;
                        userEntry.Roles = new List<Role>();                      
                    }
                    userEntry.Roles.Add((Role)role);
                    return userEntry;
                },
                new { login },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return result;

        }
    }
}
