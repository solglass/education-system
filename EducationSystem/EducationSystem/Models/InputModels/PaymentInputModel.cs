using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models

{
    public class PaymentInputModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string Period { get; set; }
        public int ContractNumber { get; set; }
        //2DO FIX
        //public List <UserInputModel> Ids { get; set; }

    }
}
