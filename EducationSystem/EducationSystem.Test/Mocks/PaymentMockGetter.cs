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
                    Date = DateTime.Today,
                    IsPaid = false,
                    Period = "2021.01"
                },
                2 => new PaymentDto()
                {
                    Amount = 2222,
                    ContractNumber = 99,
                    Date = DateTime.Today,
                    IsPaid = true,
                    Period = "2021.01",
                },
                3 => new PaymentDto()
                {
                    Amount = 4444,
                    ContractNumber = 99,
                    Date = DateTime.Today,
                    IsPaid = true,
                    Period = "2021.02",
                },
                4 => new PaymentDto()
                {
                    Amount = 4444,
                    ContractNumber = 11,
                    Date = DateTime.Today,
                    IsPaid = true,
                    Period = "2021.03",
                },
                5 => new PaymentDto()
                {
                    Amount = 1111,
                    ContractNumber = 11,
                    Date = DateTime.Today,
                    IsPaid = false,
                    Period = "2021.04",
                },
                6 => new PaymentDto()
                {
                    Amount = 1111,
                    ContractNumber = 11,
                    Date = DateTime.Today,
                    IsPaid = true,
                    Period = "2021.04",
                },
                7 => new PaymentDto()
                {
                    Amount = 1111,
                    ContractNumber = 11,
                    Date = DateTime.Today,
                    IsPaid = false,
                    Period = "2021.05",
                },
                99 => new PaymentDto(),

                _ => throw new NotImplementedException()
            };

            return PaymentDto;
        }

    }
}
