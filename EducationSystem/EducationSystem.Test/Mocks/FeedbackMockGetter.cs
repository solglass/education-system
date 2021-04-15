using EducationSystem.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace EducationSystem.Data.Tests.Mocks
{
    [ExcludeFromCodeCoverage]
    public static class FeedbackMockGetter
    {
        public static FeedbackDto GetFeedbackDtoMock(int id)
        {
            return id switch
            {
                1 => new FeedbackDto
                {
                    Message = "Test 1 mock",
                    UnderstandingLevel = Core.Enums.UnderstandingLevel.Bad
                },
                2 => new FeedbackDto
                {
                    Message = "Test 2 mock",
                    UnderstandingLevel = Core.Enums.UnderstandingLevel.Good
                },
                3 => new FeedbackDto
                {
                    Message = "Test 3 mock",
                    UnderstandingLevel = Core.Enums.UnderstandingLevel.Good
                },
                4 => new FeedbackDto(),
                _ => null,
            };
        }
    }
}
