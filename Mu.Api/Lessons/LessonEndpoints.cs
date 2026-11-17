using Mu.Api.Lessons.Model;
using Mu.Api.Lessons.Services;

namespace Mu.Api.Lessons;

public static class LessonEndpoints
{

    const string BaseRoute = "/api/lessons";
    public static void MapLessonEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(BaseRoute);

        group.MapPost("/", async (
            CreateLessonRequest request,
            ILessonService service
        ) =>
        {
            var lesson = await service.CreateLessonAsync(request);

            return TypedResults.Created($"{BaseRoute}/{lesson.Id}", lesson);
        });

        group.MapGet("/{lessonId:guid}", async (
            Guid lessonId,
            ILessonService service
        ) =>
        {
            var lesson = await service.GetLessonAsync(lessonId);

            return TypedResults.Ok(lesson);
        });

        group.MapGet("/", async (
            ILessonService service
        ) =>
        {
            var lessons = await service.GetLessonsListAsync();

            return TypedResults.Ok(lessons);
        });

        group.MapPut("/{lessonId:guid}", async (
            Guid lessonId,
            UpdateLessonRequest request,
            ILessonService service
        ) =>
        {
            await service.EditLessonAsync(lessonId, request);

            return TypedResults.NoContent();
        });

        group.MapDelete("/{lessonId:guid}", async (
            Guid lessonId,
            ILessonService service
        ) =>
        {
            await service.DeleteLessonAsync(lessonId);

            return TypedResults.NoContent();
        });
    }
}