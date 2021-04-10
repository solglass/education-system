using System.Collections.Generic;

namespace EducationSystem.API.Models.OutputModels
{
    public class UserOutputModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string UserPic { get; set; }
        public string Email { get; set; }
        public int ContractNumber { get; set; }
        public List<string> Roles { get; set; }
    }
}
