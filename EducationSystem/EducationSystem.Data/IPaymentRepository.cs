using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IPaymentRepository
    {
        List<PaymentDto> GetPaymentsByPeriod(string periodFrom, string PeriodTo);
        List<PaymentDto> GetPaymentsByUserId(int id);
        PaymentDto GetPaymentById(int id);
        List<PaymentDto> GetPaymentByContractNumber(int contractNumber);
        List<UserDto> GetStudentsByIsPaidInPeriod(string period);
        int AddPayment(PaymentDto payment);
        int UpdatePayment(PaymentDto payment);
        int DeletePayment(int id);
    }
}