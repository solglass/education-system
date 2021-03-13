using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models.OutputModels
{
    public class StudentGroupOutputModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int ContractNumber { get; set; }

    }
}