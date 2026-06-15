using System.Diagnostics;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 1. አጭር የ Correlation ID መፍጠር
        var correlationId = Guid.NewGuid().ToString("N")[..8];

        // 2. ምላሽ ከመጀመሩ በፊት ሄደሩን መstamp ማድረግ (በጣም ወሳኝ)
        context.Response.Headers["X-Correlation-Id"] = correlationId;

        // 3. ጊዜ መለካት መጀመር
        var stopwatch = Stopwatch.StartNew();

        // 4. ጥያቄው ሲገባ ሎግ ማድረግ
        _logger.LogInformation("➡️ Entry: {Method} {Path} [CorrelationID: {Id}]", 
            context.Request.Method, context.Request.Path, correlationId);

        // ወደ ቀጣዩ ሚድልዌር ማለፍ
        await _next(context);

        stopwatch.Stop();

        // 5. ጥያቄው ሲወጣ ሎግ ማድረግ
        _logger.LogInformation("⬅️ Exit: Status {StatusCode} in {ElapsedMs}ms [CorrelationID: {Id}]", 
            context.Response.StatusCode, stopwatch.ElapsedMilliseconds, correlationId);
    }
}
