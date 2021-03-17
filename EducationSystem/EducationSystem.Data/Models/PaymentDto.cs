using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class PaymentDto : ICloneable
    {
        public int Id { get; set; }
        public int ContractNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Period { get; set; }
        public bool IsPaid { get; set; }
        public UserDto Student { get; set; }

        public object Clone()
        {           
            return new PaymentDto()
            {
                Amount = Amount,
                ContractNumber = ContractNumber,
                Student = Student,
                Date = Date,
                IsPaid = IsPaid,
                Period = Period
            };
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is PaymentDto))
                return false;

            var paymentDto = (PaymentDto)obj;

            return ((paymentDto.Id == Id) &&
            (paymentDto.ContractNumber == ContractNumber) &&
            (paymentDto.Amount == Amount) &&
            (paymentDto.Date == Date) &&
            (paymentDto.Student.Id == Student.Id) &&
            (paymentDto.IsPaid == IsPaid) &&
            string.Equals(paymentDto.Period, Period));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string s = "";

            s += Id + " " + ContractNumber + " " + Amount + " " + Date + "; ";
            return s;
        }

    }

}
