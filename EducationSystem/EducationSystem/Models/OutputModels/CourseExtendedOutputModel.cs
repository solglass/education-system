﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class CourseExtendedOutputModel: CourseOutputModel
    {
        public bool IsDeleted { get; set; }
    }
}