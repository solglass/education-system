using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int ContractNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Period { get; set; }
        public bool IsPaid { get; set; }
        public bool IsDeleted { get; set; }

    }
}
