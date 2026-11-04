namespace Mu.Entities;

public class Lesson
{
  public required string Id { get; set; }
  public required string StudentId { get; set; }
  public DateTime DateOfLesson { get; set; }
}