using System.Collections.Generic;

namespace EducationSystem.API.Models

{
    public class PaymentInputModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string Period { get; set; }
        //2DO FIX
        //public List <UserInputModel> Ids { get; set; }
    
    }
}
