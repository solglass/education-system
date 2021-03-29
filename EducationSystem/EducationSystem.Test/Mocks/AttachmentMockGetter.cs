using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
   public static class AttachmentMockGetter
    {
        public static AttachmentDto GetAttachmentDtoMock(int id)
        {
            return id switch
            {
                1 => new AttachmentDto
                {
                    Path=@"C:\Example\Path",
                    AttachmentType=(AttachmentType)1
                },
                2 => new AttachmentDto
                {
                    Path = @"D:\Example\Path",
                    AttachmentType = (AttachmentType)2
                },
                _ => null
            };
        }
    }
}
