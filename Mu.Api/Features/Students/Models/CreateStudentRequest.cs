using System.ComponentModel.DataAnnotations;
using Mu.Entities.Students;

namespace Mu.Api.Features.Students.Models;

public record CreateStudentRequest
{
  [Required]
  [MaxLength(100)]
  public string? Fullname { get; init; }
  [Required]
  [EnumDataType(typeof(SubjectLevel))]
  public SubjectLevel? SubjectLevel { get; init; }
}