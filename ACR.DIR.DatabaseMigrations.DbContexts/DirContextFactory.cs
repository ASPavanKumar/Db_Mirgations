using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;
using MySqlConnector;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ACR.DIR.DatabaseMigrations.DbContexts.DbConnection;
using ACR.DIR.DatabaseMigrations.DbContexts.Provider;
using ACR.DIR.DatabaseMigrations.DbContexts.MigrationsOperation.SqlGenerators;

namespace ACR.DIR.DatabaseMigrations.DbContexts
{
    /// <summary>
    /// Factory class for creating the DirContext during design time.
    /// </summary>
    public class DirContextFactory : IDesignTimeDbContextFactory<DirContext>
    {
        /// <summary>
        /// Creates a new instance of the DirContext.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        /// <returns>A new instance of the DirContext.</returns>
        public DirContext CreateDbContext(string[] args)
        {
            IDbSecretProvider dbSecretProvider;
            IOptions<ConnectionDetails> connectionOptions;
            ILoggerFactory loggerFactory;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            IServiceCollection services = new ServiceCollection();

            services.AddLogging(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Trace);
                logging.AddFilter("Microsoft", LogLevel.Information);
                logging.AddConsole();
            });

            services.AddDbSecretProvider(awsSecretsManager =>
            {
                awsSecretsManager.ServiceUrl = configuration.GetValue<string>("AWSSecretsManagerServiceUrl");
            });

            services.AddOptions<ConnectionDetails>().Configure(db =>
            {
                db.Server = configuration.GetValue<string>("Server")!;
                db.Port = configuration.GetValue<uint>("Port");
                db.Database = configuration.GetValue<string>("Database")!;
                db.Secret = configuration.GetValue<string>("Secret")!;
            });

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            dbSecretProvider = serviceProvider.GetRequiredService<IDbSecretProvider>();
            connectionOptions = serviceProvider.GetRequiredService<IOptions<ConnectionDetails>>();
            loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = connectionOptions.Value.Server,
                Port = connectionOptions.Value.Port,
                Database = connectionOptions.Value.Database,
                DefaultCommandTimeout = 0,
                ConnectionTimeout = 120
            };

            string connectionString = connectionStringBuilder.ToString();

            DbContextOptions<DirContext> options = new DbContextOptionsBuilder<DirContext>()
                .UseMySql(connectionString, ServerVersion.Parse("8.0.33"), options => options.EnableRetryOnFailure())
                .ReplaceService<IMigrationsSqlGenerator, CustomMySqlMigrationsSqlGenerator>()
                .AddInterceptors(new DbSecretConnectionInterceptor(dbSecretProvider, connectionOptions, loggerFactory))
                .UseLoggerFactory(loggerFactory)
                .EnableDetailedErrors()
                .Options;

            return new DirContext(options);
        }
    }
}
