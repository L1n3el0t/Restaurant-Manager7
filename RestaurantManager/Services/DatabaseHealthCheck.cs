using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using RestaurantManager.Data;
using ServiceStack.Data;

namespace RestaurantManager.Services
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        public readonly RestaurantManagerContext _db;
        private readonly ILogger<DatabaseHealthCheck> _logger;
        public DatabaseHealthCheck(RestaurantManagerContext db, ILogger<DatabaseHealthCheck> logger)
        {
            _db = db;
            _logger = logger;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Simple query to check database connectivity
                var canConnect = _db.Database.CanConnect();
                if (canConnect)
                {
                    _logger.LogInformation("Database connection successful as of {DateTime.UtcNow}.", DateTime.UtcNow);
                    return Task.FromResult(HealthCheckResult.Healthy("Database is reachable."));
                    
                }
                else
                {
                    _logger.LogError("Database connection failed as of {DateTime.UtcNow}.", DateTime.UtcNow);
                    return Task.FromResult(HealthCheckResult.Unhealthy("Database is not reachable."));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database health check failed with an exception as of {DateTime.UtcNow}.", DateTime.UtcNow);
                return Task.FromResult(HealthCheckResult.Unhealthy("Database check failed.", ex));
            }
        }
    }
}


