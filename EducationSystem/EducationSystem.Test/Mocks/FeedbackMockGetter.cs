using EducationSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class FeedbackMockGetter
    {
        public static FeedbackDto GetFeedbackDtoMock(int id)
        {
            switch (id)
            {
                case 1:
                    return new FeedbackDto
                    {
                        Message = "Test 1 mock",
                        UnderstandingLevel = Core.Enums.UnderstandingLevel.Bad
                    };
                    break;
                case 2:
                    return new FeedbackDto
                    {
                        Message = "Test 2 mock",
                        UnderstandingLevel = Core.Enums.UnderstandingLevel.Good
                    };
                    break;
                case 3:
                    return new FeedbackDto
                    {
                        Message = "Test 3 mock",
                        UnderstandingLevel = Core.Enums.UnderstandingLevel.Medium
                    };
                    break;
                default:
                    return null;
            }
        }
    }
}
