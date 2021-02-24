using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
  public class HomeworkAttemptByUserDto
  {
    public int Id { get; set; }
    public string Comment { get; set; }
    public bool IsDeleted { get; set; }
    public int StatusId { get; set; }
    public int CountAttachments { get; set; }
    public int CountComments { get; set; }
    public int AttemptStatusId { get; set; }
    public string AttemptStatusName { get; set; }
    public HomeworkDto Homework { get; set; }
    public AuthorDto Author { get; set; }
    

    public override bool Equals(object obj)
    {
      HomeworkAttemptDto attemptObj = (HomeworkAttemptDto)obj;
      if (!attemptObj.Comment.Equals(Comment) || attemptObj.IsDeleted != IsDeleted)
      {
        return false;
      }
      if (attemptObj.Author == null || Author == null || !attemptObj.Author.Equals(Author))
      {
        return false;
      }
      if (attemptObj.Homework == null || Homework == null || !attemptObj.Homework.Equals(Homework))
      {
        return false;
      }

      return true;
    }
  }
}
