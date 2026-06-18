using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TmsApi.Data;

namespace TmsApi.Controllers;

[ApiController]
[Route("api/registrar")]
public class RegistrarController(AppDbContext context) : ControllerBase
{
    // 1. ንቁ የሆኑና GPA >= 3.0 የሆኑ ተማሪዎች ስንት ናቸው?
    [HttpGet("active-high-gpa-count")]
    public async Task<IActionResult> GetActiveHighGpaCount()
    {
        var count = await context.Students
            .Where(s => s.IsActive && s.GPA >= 3.0m)
            .CountAsync(); // በ SQL ደረጃ SELECT COUNT(*) ያደርጋል
        return Ok(new { ActiveHighGpaCount = count });
    }

    // 2. ከፍተኛ የተማሪዎች ምዝገባ ያላቸው ኮርሶች በቅደም ተከተል (Descending)
    [HttpGet("courses-by-enrollment")]
    public async Task<IActionResult> GetCoursesByEnrollment()
    {
        var list = await context.Courses
            .Select(c => new
            {
                c.Title,
                EnrollmentCount = c.Enrollments.Count
            })
            .OrderByDescending(x => x.EnrollmentCount)
            .ToListAsync();
        return Ok(list);
    }

    // 3. for one course (Average GPA per course)
    [HttpGet("course-average-gpa")]
    public async Task<IActionResult> GetCourseAverageGpa()
    {
        var list = await context.Enrollments
            .GroupBy(e => e.Course.Title)
            .Select(g => new
            {
                Course = g.Key,
                AverageGPA = g.Average(e => e.Student.GPA)
            })
            .ToListAsync();
        return Ok(list);
    }

    // 4. no course(Using Subquery - Approach A)
    [HttpGet("students-with-no-enrollments")]
    public async Task<IActionResult> GetStudentsWithNoEnrollments()
    {
        var list = await context.Students
            .Where(s => !s.Enrollments.Any())
            .Select(s => s.Name)
            .ToListAsync();
        return Ok(list);
    }
}