using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IPaymentRepository
    {
        int AddPayment(PaymentDto payment);
        int DeletePayment(int id);
        PaymentDto GetPaymentByContractNumber(int contractNumber);
        List<PaymentDto> GetPaymentsByUserId(int id);
        List<PaymentDto> GetPaymentsByPeriod(string periodFrom, string PeriodTo);
        PaymentDto GetPaymentById(int id);              
        List<UserDto> GetStudentsNotPaidInMonth(string period);
        int UpdatePayment(PaymentDto payment);
    }
}