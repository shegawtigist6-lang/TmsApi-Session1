using Microsoft.EntityFrameworkCore;
using TmsApi.Entities;

namespace TmsApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Assessment> Assessments { get; set; }
    public DbSet<Certificate> Certificates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Student Entity Configuration
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("tms_students"); 
            entity.Property(s => s.Name).HasMaxLength(100).IsRequired(); 
            entity.Property(s => s.RegistrationNumber).HasMaxLength(20).IsRequired();
            entity.HasIndex(s => s.RegistrationNumber).IsUnique(); 
        });

        // 2. Course Entity Configuration
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("tms_courses");
            entity.Property(c => c.Title).HasMaxLength(150).IsRequired();
            entity.Property(c => c.Code).HasMaxLength(10).IsRequired();
        });

        // 3. Enrollment Entity Configuration
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.ToTable("tms_enrollments");

            // Configure DeleteBehavior.Restrict for Course relationship
            entity.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // 4. Assessment Entity Configuration
        modelBuilder.Entity<Assessment>(entity =>
        {
            entity.ToTable("tms_assessments");
            entity.Property(a => a.Title).HasMaxLength(100).IsRequired();
        });

        // 5. Certificate Entity Configuration
        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.ToTable("tms_certificates");
            entity.Property(c => c.SerialNumber).HasMaxLength(50).IsRequired();
        });
    }
}