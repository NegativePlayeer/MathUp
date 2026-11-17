namespace Mu.Api.Lessons.Model;

public record LessonDto
{
  public Guid Id { get; set; }
  public Guid StudentId { get; set; }
  public DateTime DateOfLesson { get; set; }
}
