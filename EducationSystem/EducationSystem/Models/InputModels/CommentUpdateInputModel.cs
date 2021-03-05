using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class CommentUpdateInputModel
    {
        public string Message { get; set; }
        public List<AttachmentInputModel> Attachments { get; set; }
    }
}
