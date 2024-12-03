using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Amazon.SecretsManager.Extensions.Caching;
using Amazon.SecretsManager.Model;

using Microsoft.Extensions.Logging;

namespace ACR.DIR.DatabaseMigrations.DbContexts.Provider.AmazonSecretsManager;

internal class AmazonSecretProvider : IDbSecretProvider
{
    private readonly ISecretsManagerCache _amazonSecretsManager;
    private readonly ILogger<AmazonSecretProvider> _logger;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public AmazonSecretProvider(ISecretsManagerCache amazonSecretsManager, ILogger<AmazonSecretProvider> logger)
    {
        _amazonSecretsManager = amazonSecretsManager;
        _logger = logger;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
        };
    }

    public async Task<DbSecretValue> GetValueAsync(string secret, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting DbSecretValue...");
        
        SecretCacheItem secretCacheItem = _amazonSecretsManager.GetCachedSecret(secret);

        GetSecretValueResponse response = await secretCacheItem.GetSecretValue(cancellationToken);

        string secretValueString = GetSecretValueString(response);

        DbSecretValue dbSecretValue = JsonSerializer.Deserialize<DbSecretValue>(secretValueString, _jsonSerializerOptions)!;

        Validator.ValidateObject(dbSecretValue, new ValidationContext(dbSecretValue), true);

        _logger.LogInformation("DbSecretValue found.");

        return dbSecretValue;
    }

    private static string GetSecretValueString(GetSecretValueResponse response)
    {
        string secretString;

        if (response.SecretString is not null)
        {
            secretString = response.SecretString;
        }
        else
        {
            using MemoryStream memoryStream = response.SecretBinary;
            using var reader = new StreamReader(memoryStream);
            secretString = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
        }

        return secretString;
    }
}