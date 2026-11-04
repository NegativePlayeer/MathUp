using Microsoft.EntityFrameworkCore;
using Mu.Entities;
using Mu.Entities.Students;

namespace Mu.Api;

public class StudentContext : DbContext
{
  public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }

  public DbSet<Student> Students { get; set; }
  public DbSet<Lesson> Lessons { get; set; }
}