using Microsoft.EntityFrameworkCore;
using Mu.Api.Features.Lessons.Model;
using Mu.Api.Infrastructure.Exceptions;
using Mu.Entities;

namespace Mu.Api.Lessons.Services;

public class LessonService : ILessonService
{
    private readonly StudentContext _studentContext;
    private readonly ILogger<LessonService> _logger;

    public LessonService(
        StudentContext studentContext,
        ILogger<LessonService> logger
    )
    {
        _studentContext = studentContext;
        _logger = logger;
    }

    public async Task<LessonDto> CreateLessonAsync(CreateLessonRequest request)
    {
        var studentExists = await _studentContext.Students.AnyAsync(s => s.Id == request.StudentId);

        if (studentExists is false)
        {
            throw new NotFoundException($"Student with Id {request.StudentId} not found");
        }

        var lesson = new Lesson
        {
            StudentId = request.StudentId!.Value,
            DateOfLesson = request.DateOfLesson!.Value
        };

        _studentContext.Lessons.Add(lesson);
        await _studentContext.SaveChangesAsync();

        return new LessonDto
        {
            Id = lesson.Id,
            StudentId = lesson.StudentId,
            DateOfLesson = lesson.DateOfLesson
        };
    }

    public async Task<LessonDto> GetLessonAsync(Guid lessonId)
    {
        var lesson = await _studentContext.Lessons.Where(l => l.Id == lessonId).Select(l => new LessonDto
        {
            Id = l.Id,
            StudentId = l.StudentId,
            DateOfLesson = l.DateOfLesson
        }).FirstOrDefaultAsync();

        if (lesson is null)
        {
            throw new NotFoundException($"Lesson with Id {lessonId} not found");
        }

        return lesson;
    }

    public async Task<IReadOnlyList<LessonDto>> GetLessonsListAsync()
    {
        var lessons = await _studentContext.Lessons.Select(l => new LessonDto
        {
            Id = l.Id,
            StudentId = l.StudentId,
            DateOfLesson = l.DateOfLesson
        }).ToListAsync();

        return lessons;
    }

    public async Task EditLessonAsync(Guid lessonId, UpdateLessonRequest request)
    {
        var entity = await _studentContext.Lessons.FindAsync(lessonId);

        if (entity is null)
        {
            throw new NotFoundException($"Lesson with Id {lessonId} not found");
        }

        var studentExists = await _studentContext.Students.AnyAsync(s => s.Id == request.StudentId);

        if (studentExists is false)
        {
            throw new NotFoundException($"Student with Id {request.StudentId} not found");
        }

        entity.StudentId = request.StudentId!.Value;
        entity.DateOfLesson = request.DateOfLesson!.Value;

        await _studentContext.SaveChangesAsync();

        _logger.LogInformation("Lesson with id {LessonId} was edited", lessonId);
    }

    public async Task DeleteLessonAsync(Guid lessonId)
    {
        var deletedCount = await _studentContext.Lessons.Where(l => l.Id == lessonId).ExecuteDeleteAsync();

        if (deletedCount is 0)
        {
            throw new NotFoundException($"Lesson with Id {lessonId} not found");
        }

        _logger.LogInformation("Lesson with id {LessonId} was deleted", lessonId);
    }
}