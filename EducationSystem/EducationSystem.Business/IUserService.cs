using EducationSystem.Data.Models;
using System.Collections.Generic;

namespace EducationSystem.Business
{
    public interface IUserService
    {
        int AddUser(UserDto userDto);
        int ChangePassword(int id, string oldPassword, string password);
        int DeleteUser(int id);
        int RecoverUser(int id);
        int DeletePayment(int id);
        int UpdateUser(int id, UserDto userDto);
        int AddPayment(int id, PaymentDto paymentDto);
        int UpdatePayment(int id, PaymentDto paymentDto);
        UserDto GetUserById(int id);
        PaymentDto GetPaymentById(int id);
        List<UserDto> GetPassedStudentsAttempt_SelectByGroupId(int groupId);
        List<UserDto> GetUsers();
        List<UserDto> GetListOfStudentsByPeriodWhoHaveNotPaid(string period);
        List<PaymentDto> GetPaymentsByPeriod(string periodFrom, string PeriodTo);
        List<PaymentDto> GetPaymentsByUserId(int id);      
    }
}