using System;

namespace TmsApi.Models;

public class Course
{
    // Corresponds to: readonly id: string
    public string Id { get; set; } = string.Empty;
    
    public string Code { get; set; } = string.Empty;

    // Corresponds to: title: string
    public string Title { get; set; } = string.Empty;
    
    // Corresponds to: capacity: number
    public int Capacity { get; set; }
    
    // Corresponds to: startDate?: Temporal.PlainDate (Nullable DateTime in C#)
    public DateTime? StartDate { get; set; }

    // Corresponds to the CourseStatus type
    public string Status { get; set; } = "DRAFT"; 
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public string Syllabus { get; set; } = string.Empty;
    public int EnrolledCount { get; set; }
    public string CancellationReason { get; set; } = string.Empty;

    // Corresponds to the describeCourse(status) function logic converted to C#
    public string DescribeCourse()
    {
        return Status.ToUpper() switch
        {
            "DRAFT" => $"Draft created by {CreatedBy}",
            "PUBLISHED" => $"Published with syllabus: {Syllabus}",
            "ACTIVE" => $"Active with {EnrolledCount} students since {StartDate?.ToString("yyyy-MM-dd")}",
            "ARCHIVED" => $"Archived with a final count of {EnrolledCount} students",
            "CANCELLED" => $"Cancelled due to: {CancellationReason}",
            _ => "Unknown Status"
        };
    }
}