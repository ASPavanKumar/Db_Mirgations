using ACR.DIR.DatabaseMigrations.DbContexts.Provider.AmazonSecretsManager;
using Microsoft.Extensions.DependencyInjection;

namespace ACR.DIR.DatabaseMigrations.DbContexts.Provider;

internal static class DbSecretProviderServiceCollection
{
    public static IServiceCollection AddDbSecretProvider(this IServiceCollection services, Action<AmazonSecretsManagerOptions> options)
    {
        services.AddOptions<AmazonSecretsManagerOptions>().Configure(options);

        services.AddSingleton<AmazonSecretsManagerCacheFactory>();

        services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<AmazonSecretsManagerCacheFactory>().Create());
        
        services.AddSingleton<IDbSecretProvider, AmazonSecretProvider>();

        return services;
    }
}