using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Models
{
    public class Comment_AttachmentDto
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int AttachmentId { get; set; }

    }
}
