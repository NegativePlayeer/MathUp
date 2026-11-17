using Mu.Entities.Students;

namespace Mu.Entities;

public class Lesson
{

  public Guid Id { get; set; }
  public required Guid StudentId { get; set; }
  public Student Student { get; set; } = null!;
  public DateTime DateOfLesson { get; set; }
}