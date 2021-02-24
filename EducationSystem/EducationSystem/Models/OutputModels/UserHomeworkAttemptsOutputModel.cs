using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationSystem.Data.Models;

namespace EducationSystem.API.Models.OutputModels
{
  public class UserHomeworkAttemptsOutputModel
  {
    
    public int Id { get; set; }
    public string Comment { get; set; }
    public bool IsDeleted { get; set; }
    public int StatusId { get; set; }
    public int CountAttachments { get; set; }
    public int CountComments { get; set; }
    public int AttemptStatusId { get; set; }
    public string AttemptStatusName { get; set; }
    public HomeworkOutputModel Homework { get; set; }
    public AuthorOutputModel Author { get; set; }
  }
}
