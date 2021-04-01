﻿using EducationSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Models.InputModels
{
    public class HomeworkAttemptInputModel
    {
        [Required]
        public string Comment { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int AuthorId { get; set; }
        [Required]
        [Range((int)HomeworkAttemptStatus.ToDo,(int)HomeworkAttemptStatus.PassedLate)]
        public int HomeworkAttemptStatusId { get; set; }
    }
}
