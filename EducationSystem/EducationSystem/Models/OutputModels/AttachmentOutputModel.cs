using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class AttachmentOutputModel
    {

        public int Id { get; set; }
        public string Path { get; set; }
        public AttachmentTypeOutputModel AttachmentType { get; set; }
    }
}
