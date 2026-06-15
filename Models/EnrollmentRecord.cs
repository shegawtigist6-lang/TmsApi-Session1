namespace TmsApi.Models;

public class EnrollmentRequest
{
    public Student Student { get; set; } = new Student();
    public Course Course { get; set; } = new Course();
}