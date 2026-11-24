using System.ComponentModel.DataAnnotations;
using Mu.Api.Infrastructure.Validation;

namespace Mu.Api.Features.Lessons.Model;

public record UpdateLessonRequest
{
    [Required]
    public Guid? StudentId { get; set; }
    [Required]
    [MuFutureDate]
    public DateTime? DateOfLesson { get; set; }
}