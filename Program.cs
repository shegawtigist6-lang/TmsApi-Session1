using TmsApi;

// 1. ALWAYS CREATE THE BUILDER FIRST
var builder = WebApplication.CreateBuilder(args);

// 2. REGISTER REQUIRED CORE SERVICES
builder.Services.AddAuthentication("Training");
builder.Services.AddAuthorization();
builder.Services.AddControllers(); 

// Exercise 3: Options Configuration & Startup Validation
builder.Services.AddOptions<PaymentOptions>()
    .BindConfiguration("Payments")
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Exercise 2: Lifetime Registrations (Fixed via IServiceScopeFactory)
builder.Services.AddSingleton<EnrollmentWorker>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

// Enabling Host Validation
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

// 3. BUILD THE APPLICATION
var app = builder.Build();

// 4. PIPELINE MIDDLEWARES & ROUTING
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

// --- EXERCISE 4: TEST ENDPOINT FOR STRUCTURED LOGS ---
// This is placed right here, after 'app' is built and before 'app.Run()'
app.MapPost("/api/enrollments/test-all-logs", async (IEnrollmentService enrollmentService) =>
{
    // A. First enrollment -> Expected: [Information] log
    var record1 = await enrollmentService.EnrollAsync("S-001", "CS-101");

    // B. Duplicate enrollment -> Expected: [Warning] log
    var record2 = await enrollmentService.EnrollAsync("S-001", "CS-101");

    // C. Get Non-existent ID -> Expected: [Warning] log
    await enrollmentService.GetByIdAsync("NON_EXISTENT_ID");

    // D. Delete Successful -> Expected: [Information] log
    await enrollmentService.DeleteAsync(record1.Id);

    // E. Delete Non-existent -> Expected: [Warning] log
    await enrollmentService.DeleteAsync("NON_EXISTENT_ID");

    return Results.Ok("All structured logs successfully triggered! Check your dotnet console.");
});

// Routing the controller actions safely
app.MapControllers();

// 5. RUN THE APPLICATION
app.Run();