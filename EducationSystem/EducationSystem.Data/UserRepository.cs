using Dapper;
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
            var user = _connection
                .Query<UserDto>("dbo.User_SelectAll", commandType: System.Data.CommandType.StoredProcedure)
               .ToList();
            return user;
        }
        public UserDto GetUserById(int id)
        {
            var user = _connection
                .Query<UserDto>("dbo.User_SelectById", new { id }, commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return user;
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


        // paymentrepository
        public List<PaymentDto> GetPayments()
        {
            var payments = _connection.Query<PaymentDto, UserDto, PaymentDto>(
                    "dbo.Payment_SelectAll",
                    (payment, user) =>
                    {
                        payment.Student = user;
                        return payment;
                    },
                            splitOn: "Id,UserId,ContractNumber",
                    commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return payments;
        }

        public PaymentDto GetPaymentById(int id)
        {
            var payment = _connection.Query<PaymentDto, UserDto, PaymentDto>(
                    "dbo.Payment_SelectById",
                    (payment, user) =>
                    {
                        payment.Student = user;
                        return payment;
                    },
                    new { id },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return payment;
        }

        public PaymentDto GetPaymentByContractNumber(int contractNumber)
        {
            var payment = _connection.Query<PaymentDto, UserDto, PaymentDto>(
                    "dbo.Payment_SelectByContractNumber",
                    (payment, user) =>
                    {
                        payment.Student = user;
                        return payment;
                    },
                    new { contractNumber },
                    splitOn: "Id",
                    commandType: System.Data.CommandType.StoredProcedure)
                .FirstOrDefault();
            return payment;
        }

        // should return id of inserted entity, use 'QuerySingle' method
        public int AddPayment(int contractNumber, decimal amount, DateTime date, string period, bool IsPaid)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Payment_Add",
                new
                {
                    contractNumber,
                    amount,
                    date,
                    period,
                    IsPaid
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        // should return affected rows' count, use 'Execute' method
        public int UpdatePayment(PaymentDto payment)
        {
            var result = _connection
                .Execute("dbo.Course_Update",
                new
                {
                    
                    payment.Amount,
                    payment.Date,
                    payment.Period,
                    payment.IsPaid
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        // should return affected rows' count, use 'Execute' method
        public int DeletePayment(int id)
        {
            var result = _connection
                .Execute("dbo.Payment_Delete",
                new
                {
                    id
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }
    }

}


