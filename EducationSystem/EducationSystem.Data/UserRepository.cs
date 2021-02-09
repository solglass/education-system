using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EducationSystem.Data
{
    public class UserRepository : BaseRepository
    {
        public UserRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<UserDto> GetUsers()
        {
            var UserDictionary = new Dictionary<int, UserDto>();


            var users = _connection.
                Query<UserDto, RoleDto, UserDto>(
                "dbo.User_SelectAll",
                (user, role) =>
                {


                    if (!UserDictionary.TryGetValue(user.Id, out UserDto userEntry))
                    {
                        userEntry = user;
                        userEntry.Roles = new List<RoleDto>();
                        UserDictionary.Add(userEntry.Id, userEntry);
                    }

                    userEntry.Roles.Add(role);
                    return userEntry;
                },
                splitOn: "Id", commandType: System.Data.CommandType.StoredProcedure)
            .ToList();
            return users;

        }
        public UserDto GetUserById(int id)
        {
            var UserDictionary = new Dictionary<int, UserDto>();
            var users = _connection.
                Query<UserDto, RoleDto, UserDto>(
                "dbo.User_SelectById",
                (user, role) =>
                {
                    if (!UserDictionary.TryGetValue(user.Id, out UserDto userEntry))
                    {
                        userEntry = user;
                        userEntry.Roles = new List<RoleDto>();
                        UserDictionary.Add(userEntry.Id, userEntry);
                    }

                    userEntry.Roles.Add(role);
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

        public int ChangeUserPassword (int id, string oldPassword, string newPassword)
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
                    user.Login,
                    user.Password,
                    user.Phone,
                    user.UserPic,
                    user.Email
                }, commandType: System.Data.CommandType.StoredProcedure);
        }
        public int DeleteUser(int id)
        {
            return _connection
                .Execute("dbo.User_Delete", new { id },
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
        public List<RoleDto> GetRoles()
        {
            return _connection
                 .Query<RoleDto>("dbo.Role_SelectAll", commandType: CommandType.StoredProcedure)
                 .ToList();
        }
        public RoleDto GetRoleById(int id)
        {
            return _connection
                .Query<RoleDto>("dbo.Role_SelectById", new { id },
                commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
        }
        public int AddRole(RoleDto role)
        {
            return _connection
                .QuerySingle<int>("dbo.Role_Add",
                new { role.Name },
                commandType: System.Data.CommandType.StoredProcedure);
        }
        public int UpdateRole(RoleDto role)
        {
            return _connection
                .Execute("dbo.Role_Update",
                new { role.Id, role.Name },
                commandType: System.Data.CommandType.StoredProcedure);
        }
        public int DeleteRole(int id)
        {
            return _connection
                .Execute("dbo.Role_Delete",
                new { id },
                commandType: System.Data.CommandType.StoredProcedure);
        }
        public UserDto CheckUser(string login, string password)
        {         
            var userEntry = new UserDto();
            var result = _connection.
                Query<UserDto, RoleDto, UserDto>(
                "dbo.Check_User_Authentication",
                (user, role) =>
                {
                if (userEntry.Id == 0)
                    {
                        userEntry = user;
                        userEntry.Roles = new List<RoleDto>();                      
                    }
                    userEntry.Roles.Add(role);
                    return userEntry;
                },
                new { login, password },
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return result;

        } 
    }
}
