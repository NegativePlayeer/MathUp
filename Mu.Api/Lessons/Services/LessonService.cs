using Microsoft.EntityFrameworkCore;
using Mu.Api.Infrastructure.Exceptions;
using Mu.Api.Lessons.Model;
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
        var student = await _studentContext.Students.FirstOrDefaultAsync(s => s.Id == request.StudentId);

        if (student is null)
        {
            throw new NotFoundException($"Student with Id {request.StudentId} not found");
        }

        var lesson = new Lesson
        {
            StudentId = request.StudentId,
            DateOfLesson = request.DateOfLesson
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

        var student = await _studentContext.Students.FindAsync(request.StudentId);

        if (student is null)
        {
            throw new NotFoundException($"Student with Id {request.StudentId} not found");
        }

        entity.StudentId = request.StudentId;
        entity.DateOfLesson = request.DateOfLesson;

        await _studentContext.SaveChangesAsync();

        _logger.LogInformation("Lesson with id {LessonId} was edited", lessonId);
    }

    public async Task DeleteLessonAsync(Guid lessonId)
    {
        var entity = await _studentContext.Lessons.FirstOrDefaultAsync(l => l.Id == lessonId);

        if (entity is null)
        {
            throw new NotFoundException($"Lesson with Id {lessonId} not found");
        }

        _studentContext.Lessons.Remove(entity);
        await _studentContext.SaveChangesAsync();

        _logger.LogInformation("Lesson with id {LessonId} was deleted", lessonId);
    }
}