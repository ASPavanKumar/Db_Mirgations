using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using ACR.DIR.DatabaseMigrations.DbContexts.Provider;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using MySqlConnector;

namespace ACR.DIR.DatabaseMigrations.DbContexts.DbConnection
{
    internal class DbSecretConnectionInterceptor : DbConnectionInterceptor
    {
        private readonly IDbSecretProvider _dbSecretProvider;
        private readonly IOptions<ConnectionDetails> _options;
        private readonly ILogger<DbSecretConnectionInterceptor> _logger;

        public DbSecretConnectionInterceptor(IDbSecretProvider dbSecretProvider, IOptions<ConnectionDetails> options, ILoggerFactory loggerFactory)
        {
            _dbSecretProvider = dbSecretProvider;
            _options = options;
            _logger = loggerFactory.CreateLogger<DbSecretConnectionInterceptor>();
        }

        public override System.Data.Common.DbConnection ConnectionCreated(ConnectionCreatedEventData eventData, System.Data.Common.DbConnection dbConnection)
        {
            Validator.ValidateObject(_options.Value, new ValidationContext(_options.Value), true);

            DbSecretValue dbSecretValue = _dbSecretProvider.GetValueAsync(_options.Value.Secret).GetAwaiter().GetResult();

            dbConnection.ConnectionString = RebuildMySqlConnectionString(dbConnection.ConnectionString, dbSecretValue);

            _logger.LogInformation("DbSecretValue has been provided to DbConnection.");

            return base.ConnectionCreated(eventData, dbConnection);
        }

        private static string RebuildMySqlConnectionString(string connectionString, DbSecretValue dbSecretValue)
        {
            var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString)
            {
                UserID = dbSecretValue.Username,
                Password = dbSecretValue.Password
            };

            string newConnectionString = connectionStringBuilder.ToString();

            return newConnectionString;
        }
    }
}
