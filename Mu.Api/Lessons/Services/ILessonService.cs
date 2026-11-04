using Mu.Api.Lessons.Model;

namespace Mu.Api.Lessons.Services;

public interface ILessonService
{
  Task<LessonDto> CreateLessonAsync(string studentId, DateTime dateOfLesson);
}