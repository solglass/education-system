using EducationSystem.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class TagMockGetter
    {
        public static TagDto GetTagDtoMock(int id)
        {
            return id switch
            {
                1 => new TagDto { Name = "Test Tag" },
                2 => new TagDto { Name = "Test Tag2" },
                3 => new TagDto { Name = "Test Tag3" },
                4 => new TagDto(),
                _ => null,
            };
        }
    }
}
