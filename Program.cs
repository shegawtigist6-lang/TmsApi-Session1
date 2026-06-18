using Microsoft.EntityFrameworkCore;
using TmsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Register DbContext with SQL Logging and Sensitive Data enabled (Step 1)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information) 
           .EnableSensitiveDataLogging()); 

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 2. Auto-Seeder Block: Adds test data if database is empty (Step 2)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); 

    if (!context.Students.Any())
    {
        var students = new List<TmsApi.Entities.Student>
        {
            new() { RegistrationNumber = "TMS-2026-0001", Name = "Alice Smith", GPA = 3.8m, IsActive = true },
            new() { RegistrationNumber = "TMS-2026-0002", Name = "Bob Jones", GPA = 2.9m, IsActive = true },
            new() { RegistrationNumber = "TMS-2026-0003", Name = "Charlie Brown", GPA = 3.4m, IsActive = false },
            new() { RegistrationNumber = "TMS-2026-0004", Name = "Diana Prince", GPA = 3.9m, IsActive = true },
            new() { RegistrationNumber = "TMS-2026-0005", Name = "Evan Wright", GPA = 2.5m, IsActive = true }
        };
        context.Students.AddRange(students);

        var courses = new List<TmsApi.Entities.Course>
        {
            new() { Code = "CS-101", Title = "Introduction to Computer Science", Capacity = 30 },
            new() { Code = "CS-201", Title = "Data Structures and Algorithms", Capacity = 25 },
            new() { Code = "MAT-101", Title = "Calculus I", Capacity = 40 }
        };
        context.Courses.AddRange(courses);
        context.SaveChanges();

        var enrollments = new List<TmsApi.Entities.Enrollment>
        {
            new() { StudentId = students[0].Id, CourseId = courses[0].Id, EnrolledAt = DateTime.UtcNow, Grade = 4.0m },
            new() { StudentId = students[0].Id, CourseId = courses[1].Id, EnrolledAt = DateTime.UtcNow, Grade = 3.6m },
            new() { StudentId = students[1].Id, CourseId = courses[0].Id, EnrolledAt = DateTime.UtcNow, Grade = 2.8m },
            new() { StudentId = students[3].Id, CourseId = courses[1].Id, EnrolledAt = DateTime.UtcNow, Grade = 3.9m }
        };
        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();
    }
}

app.Run();