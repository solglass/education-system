﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.OutputModels
{
    public class LessonThemeOutputModel
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public int LessonId { get; set; }
    }
}
