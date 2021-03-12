using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class UserOutputExtendedModel:UserOutputModel
    {
        public bool IsDeleted { get; set; }
    }
}
