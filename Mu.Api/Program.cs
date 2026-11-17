using Microsoft.EntityFrameworkCore;
using Mu.Api;
using Mu.Api.Infrastructure;
using Mu.Api.Lessons;
using Mu.Api.Lessons.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StudentContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddExceptionHandler<MuExceptionHandler>();

builder.Services.AddProblemDetails();
builder.Services.AddValidation();
builder.Services.AddOpenApi();

builder.Services.AddScoped<ILessonService, LessonService>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.UseSwaggerUI(o =>
      o.SwaggerEndpoint("/openapi/v1.json", "MathUp API"));
}

app.MapLessonEndpoints();
app.Run();