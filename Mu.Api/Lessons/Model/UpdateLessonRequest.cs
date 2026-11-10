using Mu.Api.Infrastructure.Validation;

namespace Mu.Api.Lessons.Model;

public record UpdateLessonRequest
{
    public Guid StudentId { get; set; }
    [MuFutureDate]
    public DateTime DateOfLesson { get; set; }
}