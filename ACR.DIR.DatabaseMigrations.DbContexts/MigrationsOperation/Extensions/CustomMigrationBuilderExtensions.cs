using ACR.DIR.DatabaseMigrations.DbContexts.MigrationsOperation;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace ACR.DIR.DatabaseMigrations.DbContexts.MigrationsOperation.Extensions;

internal static class CustomMigrationBuilderExtensions
{
    internal static OperationBuilder<CreateJsonIndexOperation> CreateJsonIndex(this MigrationBuilder migrationBuilder, string name, string table, string expression)
    {
        var operation = new CreateJsonIndexOperation(name, table, expression);

        migrationBuilder.Operations.Add(operation);

        return new OperationBuilder<CreateJsonIndexOperation>(operation);
    }
}