using System.ComponentModel.DataAnnotations;

namespace EducationSystem.API.Models.InputModels
{
    public class StudentGroupInputModel
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int ContractNumber { get; set; }

    }
}