using System;
using Microsoft.Extensions.DependencyInjection;

namespace TmsApi;

public class EnrollmentWorker
{
    // Injecting the scope factory instead of the scoped service directly to avoid capturing it
    private readonly IServiceScopeFactory _scopeFactory;

    public EnrollmentWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void ProcessBatch()
    {
        Console.WriteLine("Worker is processing batch...");

        // TODO2: Create a short-lived scope using the injected factory
        using var scope = _scopeFactory.CreateScope();

        // TODO3: Resolve the scoped service from the new scope's provider
        var enrollmentService = scope.ServiceProvider.GetRequiredService<IEnrollmentService>();

        // TODO4: Use the service safely inside this block
        // The 'using' block will automatically dispose of the scope and its services when done
        Console.WriteLine("Successfully resolved enrollment service inside a safe scope!");
    }
}