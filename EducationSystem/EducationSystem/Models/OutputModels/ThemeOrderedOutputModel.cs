﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class ThemeOrderedOutputModel:ThemeOutputModel
    {
        public int Order { get; set; }
    }
}
