namespace ACR.DIR.DatabaseMigrations.DbContexts.Provider.AmazonSecretsManager;

internal record AmazonSecretsManagerOptions
{
    public string? ServiceUrl { get; set; }
}