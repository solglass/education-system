using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Data
{
    public interface IPaymentRepository
    {
        List<PaymentDto> GetPaymentsByPeriod(string periodFrom, string PeriodTo);
        PaymentDto GetPaymentById(int id);              
        List<UserDto> GetListOfStudentsByPeriodWhoHaveNotPaid(string period);
        List<PaymentDto> GetPaymentsByUserId(int id);
        List<PaymentDto> GetPaymentByContractNumber(int contractNumber);
        int AddPayment(PaymentDto payment);
        int UpdatePayment(PaymentDto payment);
        int DeletePayment(int id);
    }
}