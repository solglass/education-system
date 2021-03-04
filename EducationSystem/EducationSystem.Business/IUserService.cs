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
        List<UserDto> GetPassedStudentsAttempt_SelectByGroupId(int groupId);
        List<PaymentDto> GetPaymentsByPeriod(string periodFrom, string PeriodTo);
        List<PaymentDto> GetPaymentsByUserId(int id);
        UserDto GetUserById(int id);
        List<UserDto> GetUsers();
        int UpdateUser(UserDto userDto);

    }
}