using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IPaymentRepository
    {
        int AddPayment(PaymentDto payment);
        int DeletePayment(int id);
        PaymentDto GetPaymentByContractNumber(int contractNumber);
        PaymentDto GetPaymentById(int id);
        List<PaymentDto> GetPayments();
        List<UserDto> GetStudentsByIsPaidInPeriod(string period);
        int UpdatePayment(PaymentDto payment);
    }
}