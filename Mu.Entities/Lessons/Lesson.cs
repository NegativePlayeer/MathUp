namespace Mu.Entities;

public class Lesson
{
  public Guid Id { get; set; }
  public required Guid StudentId { get; set; }
  public DateTime DateOfLesson { get; set; }
}