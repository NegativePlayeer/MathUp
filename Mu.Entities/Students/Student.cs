namespace Mu.Entities.Students;

public class Student
{
  public Guid Id { get; set; }
  public required string Fullname { get; set; }
  public SubjectLevel SubjectLevel { get; set; }
}

public enum SubjectLevel
{
  Primary,
  SecondaryStandard,
  SecondaryAdvanced
}