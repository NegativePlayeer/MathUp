using Microsoft.EntityFrameworkCore;
using Mu.Api;
using Mu.Api.Infrastructure;
using Mu.Api.Lessons;
using Mu.Api.Lessons.Model;
using Mu.Api.Lessons.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StudentContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddExceptionHandler<MuExceptionHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddValidation();

builder.Services.AddScoped<ILessonService, LessonService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.MapLessonEndpoints();
app.Run();
