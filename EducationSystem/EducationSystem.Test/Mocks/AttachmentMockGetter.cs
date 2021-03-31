using EducationSystem.Core.Enums;
using EducationSystem.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class AttachmentMockGetter
    {
        public static AttachmentDto GetAttachmentDtoMock(int id)
        {
            return id switch
            {
                1 => new AttachmentDto
                {
                    Description = "Test attach 1",
                    Path=@"C:\Example\Path",
                    AttachmentType=(AttachmentType)1
                },
                2 => new AttachmentDto
                {
                    Description = "Test attach 2",
                    Path = @"D:\Example\Path",
                    AttachmentType = (AttachmentType)2
                },
                3 => new AttachmentDto(),
                4 => new AttachmentDto
                {
                    Description = "Test attach 4",
                    Path = @"D:\Example\Path",
                },
                _ => null
            };
        }
    }
}
