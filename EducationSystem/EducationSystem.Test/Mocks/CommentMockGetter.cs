using EducationSystem.Data.Models;
using System;

namespace EducationSystem.Data.Tests.Mocks
{
    public static class CommentMockGetter
    {
        public static CommentDto GetCommentDtoMock(int id)
        {
            return id switch
            {
                1 => new CommentDto
                {
                    Message = "Test 1 mock",
                    IsDeleted = false
                },
                2 => new CommentDto
                {
                    Message = "Test 2 mock",
                    IsDeleted = false
                },
                3 => new CommentDto
                {
                    Message = "Test 3 mock",
                    IsDeleted = true
                },
                4 => new CommentDto
                {
                    IsDeleted = true
                },
                _ => null,
            };
        }
    }
}
