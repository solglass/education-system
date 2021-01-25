﻿using Dapper;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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

                var UserDictionary = new Dictionary<int,UserDto>();


                var users = _connection.
                    Query<UserDto, RoleDto, UserDto>(
                    "dbo.User_SelectAll",
                    (user, role) =>
                    {
                      

                        if (!UserDictionary.TryGetValue(user.Id, out UserDto userEntry))
                        {
                           userEntry = user;
                           userEntry.roles = new List<RoleDto>();
                            UserDictionary.Add(userEntry.Id, userEntry);
                        }

                        userEntry.roles.Add(role);
                        return userEntry;
                    },
                    splitOn: "Id", commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
                return users;
           
           
        }
        public UserDto SelectUserById(int id)
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
                        userEntry.roles = new List<RoleDto>();
                        UserDictionary.Add(userEntry.Id, userEntry);
                    }

                    userEntry.roles.Add(role);
                    return userEntry;
                },
               new { id},
                splitOn: "Id",
                commandType: System.Data.CommandType.StoredProcedure)
            .FirstOrDefault();
            return users;
        }
        public UserDto AddUser()
        {
            var user = _connection
                .Query<UserDto>("dbo.UserRole_Add", commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return user;
        }
        public UserDto UpdateUser()
        {
            var user = _connection
               .Query<UserDto>("dbo.User_Update", commandType: System.Data.CommandType.StoredProcedure)
               .FirstOrDefault();
            return user;
        }
        public UserDto DeleteUser()
        {
            var user = _connection
               .Query<UserDto>("dbo.dbo.User_Delete", commandType: System.Data.CommandType.StoredProcedure)
               .FirstOrDefault();
            return user;
        }
        public List<UserRoleDto> GetUserRole()
        {
            var userRole = _connection
                .Query<UserRoleDto>("dbo.UserRole_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
               .ToList();
            return userRole;
        }
        public UserRoleDto SelectUserRoleById()
        {
            var userRole = _connection
                .Query<UserRoleDto>("dbo.UserRole_SelectById", commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return userRole;
        }
        public UserRoleDto AddUserRole()
        {
            var userRole = _connection
                .Query<UserRoleDto>("dbo.UserRole_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return userRole;
        }
        public UserRoleDto UpdateUserRole()
        {
            var userRole = _connection
                .Query<UserRoleDto>("dbo.UserRole_Update", commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return userRole;
        }
        public UserRoleDto DeleteUserRole()
        {
            var userRole = _connection
                .Query<UserRoleDto>("dbo.UserRole_Delete", commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return userRole;
        }
        public List<RoleDto> GetRole()
        {
            var role = _connection
                .Query<RoleDto>("dbo.Role_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
               .ToList();
            return role;
        }
        public RoleDto SelectRoleById()
        {
            var role = _connection
                .Query<RoleDto>("dbo.Role_SelectById", commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return role;
        }
        public RoleDto AddRole()
        {
            var role = _connection
                .Query<RoleDto>("dbo.Role_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return role;
        }
        public RoleDto UpdateRole()
        {
            var role = _connection
                 .Query<RoleDto>("dbo.Role_Update", commandType: System.Data.CommandType.StoredProcedure)
                 .FirstOrDefault();
            return role;
        }
        public RoleDto DeleteRole()
        {
            var role = _connection
                 .Query<RoleDto>("dbo.Role_Delete", commandType: System.Data.CommandType.StoredProcedure)
                 .FirstOrDefault();
            return role;
        }
    }

}
