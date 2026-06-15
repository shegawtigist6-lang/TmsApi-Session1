using Microsoft.AspNetCore.Mvc;
using TmsApi.Models;

namespace TmsApi.Controllers;

[ApiController]
// Route explicitly configured in lowercase "api/enrollments" for consistent Scalar UI rendering
[Route("api/enrollments")]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentsController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost]
    public async Task<IActionResult> Enroll([FromBody] EnrollmentRequest request)
    {
        // 1. Extract the structured student model from the request payload
        var student = request.Student;

        // 2. Extract the structured course model from the request payload
        var course = request.Course;

        // 3. Forward parameters to the core enrollment service using standard identifiers
        var record = await _enrollmentService.EnrollAsync(student.Id, course.Code);
        return Ok(record);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var record = await _enrollmentService.GetByIdAsync(id);
        if (record == null) return NotFound();
        return Ok(record);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await _enrollmentService.DeleteAsync(id);
        if (!success) return NotFound();
        return Ok(new { message = "Enrollment successfully deleted" });
    }
}
