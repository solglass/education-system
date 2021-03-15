using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class PaymentMockGetter
    {
        public static PaymentDto GetPaymentDtoMock(int mockId)
        {
            PaymentDto PaymentDto = mockId switch
            {
                1 => new PaymentDto()
                {
                    Amount = 2222,
                    ContractNumber = 99,
                    Date = DateTime.Now,
                    IsPaid = true,
                    Period = "January2020",
                },
                _ => throw new NotImplementedException()
            };

            return PaymentDto;
        }

    }
}
