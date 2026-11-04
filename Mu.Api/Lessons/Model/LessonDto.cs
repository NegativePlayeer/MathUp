namespace Mu.Api.Lessons.Model;

public record LessonDto
{
  public required string LessonId { get; set; }
  public required string StudentId { get; set; }
  public DateTime DateOfLesson { get; set; }
}