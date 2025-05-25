using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

public class MongoMetricsService
{
    private readonly ILogger<MongoMetricsService> _logger;
    private static readonly ConcurrentDictionary<string, List<double>> _operationTimes = new();

    public MongoMetricsService(ILogger<MongoMetricsService> logger)
    {
        _logger = logger;
    }

    public async Task TrackOperation(string operationType, Func<Task> operation)
    {
        var sw = Stopwatch.StartNew();
        try
        {
            await operation();
        }
        finally
        {
            sw.Stop();
            _operationTimes.GetOrAdd(operationType, _ => new List<double>())
                .Add(sw.ElapsedMilliseconds);

            _logger.LogInformation(
                "MongoDB {OperationType} took {Duration}ms",
                operationType,
                sw.ElapsedMilliseconds
            );
        }
    }

    public Dictionary<string, object> GetStatistics()
    {
        return _operationTimes.ToDictionary(
            kvp => kvp.Key,
            kvp => new
            {
                AverageTime = kvp.Value.Average(),
                MaxTime = kvp.Value.Max(),
                OperationCount = kvp.Value.Count
            } as object
        );
    }
}