using Microsoft.EntityFrameworkCore;
using TmsApi.Models;

namespace TmsApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // This creates and manages the "Students" table inside the PostgreSQL database
    public DbSet<Student> Students { get; set; }

    // This creates and manages the "Courses" table inside the PostgreSQL database
    public DbSet<Course> CustomCourses { get; set; }
}