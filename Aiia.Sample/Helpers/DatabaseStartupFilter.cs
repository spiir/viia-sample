using System;
using DbUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Aiia.Sample.Helpers
{
    public class DatabaseStartupFilter : IStartupFilter
    {
        private readonly DbLogger<DatabaseStartupFilter> _logger;
        private readonly IOptionsMonitor<SampleOptions> _options;

        public DatabaseStartupFilter(IOptionsMonitor<SampleOptions> options, DbLogger<DatabaseStartupFilter> logger)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            var connectionString = _options.CurrentValue.ConnectionStrings.Main;

            EnsureDatabase.For.SqlDatabase(connectionString);
            var dbUpgradeEngineBuilder = DeployChanges.To
                                                      .SqlDatabase(connectionString)
                                                      .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                                                      .WithTransaction().LogTo(_logger);

            var dbUpgradeEngine = dbUpgradeEngineBuilder.Build();
            if (!dbUpgradeEngine.IsUpgradeRequired())
                return next;

            _logger.WriteInformation("Upgrades has been detected. Upgrading database now..");

            var operation = dbUpgradeEngine.PerformUpgrade();
            if (operation.Successful)
            {
                _logger.WriteInformation("Upgrade completed successfully");
                return next;
            }

            _logger.WriteError("Error happened in the upgrade. Please check the logs");

            return next;
        }
    }
}
