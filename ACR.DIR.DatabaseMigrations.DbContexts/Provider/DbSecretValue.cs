using System.ComponentModel.DataAnnotations;

namespace ACR.DIR.DatabaseMigrations.DbContexts.Provider;

internal record DbSecretValue([Required] string Username, [Required] string Password);