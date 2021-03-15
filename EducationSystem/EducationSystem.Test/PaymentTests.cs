using EducationSystem.Data.Models;
using EducationSystem.Data.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests
{
   public class PaymentTests: BaseTest
    {
        private UserRepository _userRepository;
        private PaymentRepository _repository;

        private List<int> _userIdList;
        private List<int> _paymentIdList;

        [OneTimeSetUp]
        public void OneTimeSetUpTest()
        {
            _userRepository = new UserRepository(_options);
            _repository = new PaymentRepository(_options);

            _userIdList = new List<int>;
            _paymentIdList = new List<int>;

        }
            public int AddPaymentPositiveTest(PaymentDto payment)
        {
            throw new NotImplementedException();
        }

        public List<PaymentDto> GetPaymentByContractNumberPositiveTest(int contractNumber)
        {
            throw new NotImplementedException();
        }

        public PaymentDto GetPaymentByIdPositiveTest(int id)
        {
            throw new NotImplementedException();
        }

        public List<PaymentDto> GetPaymentsByPeriodPositiveTest(string periodFrom, string PeriodTo)
        {
            throw new NotImplementedException();
        }

        public List<PaymentDto> GetPaymentsByUserIdPositiveTest(int id)
        {
            throw new NotImplementedException();
        }


        public int UpdatePaymentPositiveTest(PaymentDto payment)
        {
            throw new NotImplementedException();
        }
    }
}
