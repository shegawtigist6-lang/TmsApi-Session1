using TmsApi;
// 1. ALWAYS ADD REQUIRED USINGS AT THE TOP
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TmsApi.Data;

// 2. CREATE THE BUILDER
var builder = WebApplication.CreateBuilder(args);

// 3. REGISTER REQUIRED CORE SERVICES
builder.Services.AddAuthentication("Training");
builder.Services.AddAuthorization();
builder.Services.AddControllers(); 

// --- MODULE 5: REGISTER POSTGRESQL DB CONTEXT ---
// Registers AppDbContext to use PostgreSQL with the connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- EXERCISE 7: REGISTER OPENAPI SERVICE ---
builder.Services.AddOpenApi(); 

// --- EXERCISE 6: PROBLEM DETAILS SERVICE ---
builder.Services.AddProblemDetails();

// Exercise 3: Options Configuration & Startup Validation
builder.Services.AddOptions<PaymentOptions>()
    .BindConfiguration("Payments")
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Exercise 2: Lifetime Registrations
builder.Services.AddSingleton<EnrollmentWorker>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

// 4. BUILD THE APPLICATION
var app = builder.Build();

// 5. ENVIRONMENT-AWARE MIDDLEWARE PIPELINE
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

// --- EXERCISE 7: AUTOMATIC ENVIRONMENT TOGGLE (PRODUCTION VS DEVELOPMENT) ---
// The application automatically detects the environment mode and updates policies
if (app.Environment.IsDevelopment()) 
{
    // In Development only: Expose OpenAPI and Scalar UI
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    // In Production only: Activates Exception Handler and Hides Stack Traces
    app.UseExceptionHandler();
    app.UseStatusCodePages();
}

// --- EXERCISE 6: TEST ROUTE ---
app.MapGet("/api/error", () =>
{
    throw new Exception("Simulated database failure for ProblemDetails testing");
});

// --- EXERCISE 4: TEST ENDPOINT FOR STRUCTURED LOGS ---
app.MapPost("/api/enrollments/test-all-logs", async (IEnrollmentService enrollmentService) =>
{
    var record1 = await enrollmentService.EnrollAsync("S-001", "CS-101");
    var record2 = await enrollmentService.EnrollAsync("S-001", "CS-101");
    await enrollmentService.GetByIdAsync("NON_EXISTENT_ID");
    await enrollmentService.DeleteAsync(record1.Id);
    await enrollmentService.DeleteAsync("NON_EXISTENT_ID");
    return Results.Ok("All structured logs successfully triggered!");
});

app.MapControllers();

// 6. RUN THE APPLICATION
app.Run();