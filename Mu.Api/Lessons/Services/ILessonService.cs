using Mu.Api.Lessons.Model;

namespace Mu.Api.Lessons.Services;

public interface ILessonService
{
  Task<LessonDto> CreateLessonAsync(CreateLessonRequest request);
  Task<LessonDto> GetLessonAsync(Guid lessonId);
  Task<IReadOnlyList<LessonDto>> GetLessonsListAsync();
  Task EditLessonAsync(Guid lessonId, UpdateLessonRequest request);
  Task DeleteLessonAsync(Guid lessonId);
}