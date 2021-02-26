using Dapper;
using EducationSystem.Core.Config;
using EducationSystem.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EducationSystem.Data
{
    public class PaymentRepository : BaseRepository,IPaymentRepository
    {
        public PaymentRepository(IOptions<AppSettingsConfig> options) : base(options)
        {
            _connection = new SqlConnection(_connectionString);
        }

        public List<PaymentDto> GetPayments()
        {
            var payments = _connection.Query<PaymentDto, UserDto, PaymentDto>(
                    "dbo.Payment_SelectAll",
                    (payment, user) =>
                    {
                        payment.Student = user;
                        return payment;
                    },
                            splitOn: "Id",
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
        public List<UserDto> GetStudentsByIsPaidInPeriod(string period)
        {
            var result = _connection
               .Query<UserDto>("dbo.Student_SelectByPeriodAndIsPaid", new { period },
               commandType: System.Data.CommandType.StoredProcedure).ToList();
            return result;
        }
        // should return id of inserted entity, use 'QuerySingle' method
        public int AddPayment(PaymentDto payment)
        {
            var result = _connection
                .QuerySingle<int>("dbo.Payment_Add",
                new
                {
                    payment.ContractNumber,
                    payment.Amount,
                    payment.Date,
                    payment.Period,
                    payment.IsPaid
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        // should return affected rows' count, use 'Execute' method
        public int UpdatePayment(int id, PaymentDto payment)
        {
            payment.Id = id;
            var result = _connection
                .Execute("dbo.Course_Update",
                new
                {
                    payment.ContractNumber,
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
