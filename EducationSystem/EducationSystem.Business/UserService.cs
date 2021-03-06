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
        //public List<UserDto> PassedStudentsAttempt_SelectByGroupId(int groupId)
        //{
        //    return _repo.PassedStudentsAttempt_SelectByGroupId(groupId);
        //}
        public UserDto GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
        public List<UserDto> GetPassedStudentsAttempt_SelectByGroupId(int groupId)
        {
            return _userRepository.GetPassedStudentsAttempt_SelectByGroupId(groupId);
        }
        public int UpdateUser(UserDto userDto)
        {
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
    }
}
