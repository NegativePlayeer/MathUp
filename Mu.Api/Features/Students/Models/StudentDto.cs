using Mu.Entities.Students;

namespace Mu.Api.Features.Students.Models;

public record StudentDto
{
  public Guid Id { get; init; }
  public required string Fullname { get; init; }
  public SubjectLevel SubjectLevel { get; init; }
}