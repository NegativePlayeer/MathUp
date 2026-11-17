using Microsoft.EntityFrameworkCore;
using Mu.Api.Infrastructure;
using Mu.Entities;
using Mu.Entities.Students;

namespace Mu.Api;

public class StudentContext : DbContext
{
  public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }


  public DbSet<Student> Students { get; set; }
  public DbSet<Lesson> Lessons { get; set; }
  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<DateTime>()
      .HaveConversion<UtcDateTimeConverter>();
  }
}