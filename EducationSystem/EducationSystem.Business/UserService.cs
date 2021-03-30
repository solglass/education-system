using EducationSystem.Data;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Business
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IPaymentRepository _paymentRepository;

        public UserService(IUserRepository userRepository, IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
        }
        public List<UserDto> GetUsers()
        {
            return _userRepository.GetUsers();
        }
        public UserDto GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
        public List<UserDto> GetPassedStudentsAttempt_SelectByGroupId(int groupId)
        {
            return _userRepository.GetPassedStudentsAttempt_SelectByGroupId(groupId);
        }
        public int UpdateUser(int id, UserDto userDto)
        {
            userDto.Id = id;
            return _userRepository.UpdateUser(userDto);
        }
        public int AddUser(UserDto userDto)
        {
            userDto.Password = new SecurityService().GetHash(userDto.Password);
            return _userRepository.AddUser(userDto);
        }

        public int DeleteUser(int id)
        {
            bool isDeleted = true;
            return _userRepository.DeleteOrRecoverUser(id, isDeleted);
        }

        public int RecoverUser(int id)
        {
            bool isDeleted = false;
            return _userRepository.DeleteOrRecoverUser(id, isDeleted);
        }

        public int ChangePassword(int id, string oldPassword, string password)
        {
            oldPassword = new SecurityService().GetHash(oldPassword);
            password = new SecurityService().GetHash(password);
            return _userRepository.ChangeUserPassword(id, oldPassword, password);
        }
        public List<PaymentDto> GetPaymentsByPeriod(string periodFrom, string PeriodTo)
        {
            return _paymentRepository.GetPaymentsByPeriod(periodFrom, PeriodTo);
        }
        public List<PaymentDto> GetPaymentsByUserId(int id)
        {
            return _paymentRepository.GetPaymentsByUserId(id);
        }
        public int AddPayment(int id, PaymentDto paymentDto)
        {
            paymentDto.Student.Id = id;
            return _paymentRepository.AddPayment(paymentDto);
        }
        public PaymentDto GetPaymentById(int id)
        {
            return _paymentRepository.GetPaymentById(id);
        }
        public int UpdatePayment(int id, PaymentDto paymentDto)
        {
            paymentDto.Id = id;
            return _paymentRepository.UpdatePayment(paymentDto);
        }
        public int DeletePayment(int id)
        {
            return _paymentRepository.DeletePayment(id);
        }
        public List<UserDto> GetListOfStudentsByPeriodWhoHaveNotPaid(string period)
        {
            return _paymentRepository.GetListOfStudentsByPeriodWhoHaveNotPaid(period);
        }
    }
    
}
