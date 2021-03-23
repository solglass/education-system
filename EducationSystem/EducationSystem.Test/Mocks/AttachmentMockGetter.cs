using EducationSystem.Data.Models;
using EducationSystem.Core.Enums;
using System;

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
                    Path = "Test 1 mock",
                    AttachmentType = AttachmentType.File
                },
                2 => new AttachmentDto
                {
                    Path = "Test 2 mock"
                },
                3 => new AttachmentDto(),
                _ => null,
            };
        }
    }
}
