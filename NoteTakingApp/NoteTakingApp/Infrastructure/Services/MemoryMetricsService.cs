using System.Diagnostics;
using Microsoft.Extensions.Logging;

public class MemoryMetricsService : IHostedService, IDisposable
{
    private readonly ILogger<MemoryMetricsService> _logger;
    private Timer? _timer;

    public MemoryMetricsService(ILogger<MemoryMetricsService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(LogMemoryUsage, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        return Task.CompletedTask;
    }

    private void LogMemoryUsage(object? state)
    {
        var process = Process.GetCurrentProcess();
        _logger.LogInformation(
            "Memory Usage: {WorkingSet}MB, Private Memory: {PrivateMemory}MB",
            process.WorkingSet64 / 1024.0 / 1024.0,
            process.PrivateMemorySize64 / 1024.0 / 1024.0
        );
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}