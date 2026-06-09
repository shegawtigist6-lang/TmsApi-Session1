var builder = WebApplication.CreateBuilder(args);

// 1. REGISTER SERVICES (የአሰልጣኝ Authentication እዚህ ጋር ይመዘገባል)
builder.Services.AddAuthentication("Training")
                .AddScheme<TrainingAuthOptions, TrainingAuthHandler>("Training", null);
builder.Services.AddAuthorization();

var app = builder.Build();

// 2. PIPELINE ORDERING (ሞዱሉ የጠየቀው ፍጹም ቅደም ተከተል)
app.UseMiddleware<RequestLoggingMiddleware>(); // ሎገሩ ሁልጊዜ ከላይ (Outer) ይሆናል

app.UseExceptionHandler("/error"); // ለወደፊቱ የተዘጋጀ ስሎት
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 3. SECURED ENDPOINT
app.MapGet("/api/assessments/results", () => Results.Ok(new
{
    courseCode = "CS-101",
    studentId = "S-001",
    letterGrade = "A"
}))
.RequireAuthorization();

app.Run();