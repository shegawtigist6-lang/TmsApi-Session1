using System;

namespace TmsApi.Models;

public class Student
{
    // Corresponds to: readonly id: string
    public string Id { get; set; } = string.Empty;
    
    // Corresponds to: name: string
    public string Name { get; set; } = string.Empty;
    
    // Corresponds to: enrollmentDate: Temporal.Instant
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    
    // Corresponds to: gpa?: number (Nullable double in C# until they get a grade)
    public double? Gpa { get; set; }

    // Corresponds to: isStudent(value: unknown) logic in C#
    public static bool IsStudent(object? value)
    {
        if (value is not Student student) return false;
        
        return !string.IsNullOrEmpty(student.Id) && 
               !string.IsNullOrEmpty(student.Name);
    }

    // Corresponds to: parseStudent(raw: unknown) logic in C#
    public static Student ParseStudent(object? raw)
    {
        if (raw == null)
        {
            throw new ArgumentNullException(nameof(raw), "Expected an object, received null");
        }

        if (raw is not Student student)
        {
            throw new InvalidCastException($"Expected object to be of type Student, received {raw.GetType().Name}");
        }

        if (string.IsNullOrEmpty(student.Id))
        {
            throw new ArgumentException("Expected student id to be a valid string");
        }

        if (string.IsNullOrEmpty(student.Name))
        {
            throw new ArgumentException("Expected student name to be a valid string");
        }

        // Returns the clean student object with current validation rules applied
        return new Student
        {
            Id = student.Id,
            Name = student.Name,
            EnrollmentDate = DateTime.UtcNow // Sets current timestamp similar to Temporal.Now.instant()
        };
    }
}