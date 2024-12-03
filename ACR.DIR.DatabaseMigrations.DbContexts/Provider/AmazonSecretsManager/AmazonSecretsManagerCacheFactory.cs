using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Extensions.Caching;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ACR.DIR.DatabaseMigrations.DbContexts.Provider.AmazonSecretsManager;
internal class AmazonSecretsManagerCacheFactory
{
    private readonly IOptions<AmazonSecretsManagerOptions> _options;
    private readonly ILogger<AmazonSecretsManagerCacheFactory> _logger;

    public AmazonSecretsManagerCacheFactory(IOptions<AmazonSecretsManagerOptions> options, ILogger<AmazonSecretsManagerCacheFactory> logger)
    {
        _options = options;
        _logger = logger;
    }

    /// <returns><see cref="SecretsManagerCache"/></returns>
    public ISecretsManagerCache Create()
    {
        var config = new AmazonSecretsManagerConfig();

        _logger.LogInformation("Detected Region: {regionEndpoint}", config.RegionEndpoint);

        if (config.RegionEndpoint == null)
        {
            config.RegionEndpoint = RegionEndpoint.USEast1;
        }
        
        if (!string.IsNullOrEmpty(_options.Value.ServiceUrl))
        {
            config.ServiceURL = _options.Value.ServiceUrl;
        }

        var client = new AmazonSecretsManagerClient(config);

        var cache = new SecretsManagerCache(client);

        return cache;
    }
}