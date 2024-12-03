namespace ACR.DIR.DatabaseMigrations.DbContexts.Provider;

internal interface IDbSecretProvider
{
    Task<DbSecretValue> GetValueAsync(string secret, CancellationToken cancellationToken = default);
}