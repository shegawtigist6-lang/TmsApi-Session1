using Microsoft.AspNetCore.Mvc;

namespace TmsApi.Controllers;

[ApiController]
[Route("api/enrollments")]
public class EnrollmentsController(IEnrollmentService enrollmentService) : ControllerBase
{
    // ==========================================================
    // PART A: GET Endpoints
    // ==========================================================

    // GET /api/enrollments -> returns 200 OK with all enrollment records
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enrollments = await enrollmentService.GetAllAsync();
        return Ok(enrollments);
    }

    // GET /api/enrollments/{id} -> returns one record or 404
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var record = await enrollmentService.GetByIdAsync(id);
        return record is not null ? Ok(record) : NotFound();
    }

    // ==========================================================
    // PART B: POST with 201 + Location
    // ==========================================================

    // POST /api/enrollments -> creates and returns 201 with Location header
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnrollmentRequest request)
    {
        var record = await enrollmentService.EnrollAsync(request.StudentId, request.CourseCode);
        
        // This sets status to 201 and adds the proper Location header
        return CreatedAtAction(nameof(GetById), new { id = record.Id }, record);
    }

    // ==========================================================
    // PART C: DELETE with 204 / 404
    // ==========================================================

    // DELETE /api/enrollments/{id} -> returns 204 No Content or 404
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await enrollmentService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}

// Request model DTO
public record CreateEnrollmentRequest(string StudentId, string CourseCode);