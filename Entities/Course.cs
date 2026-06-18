namespace TmsApi.Entities;

public class Course
{
    // Surrogate primary key — internal, used by foreign keys
    public int Id { get; set; }

    // Natural key — human-readable (uniqueness configured in Session 2)
    public required string Code { get; set; } 

    public required string Title { get; set; }
    
    public int Capacity { get; set; }
public ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    // Navigation property for many-to-many relationship
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}