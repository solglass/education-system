using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
  public class HomeworkAttemptWithCountDto: HomeworkAttemptDto
  {
    public int CountAttachments { get; set; }
    public int CountComments { get; set; }
  }
}
