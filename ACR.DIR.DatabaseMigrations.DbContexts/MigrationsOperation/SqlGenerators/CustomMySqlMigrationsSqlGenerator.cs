#pragma warning disable EF1001

using ACR.DIR.DatabaseMigrations.DbContexts.MigrationsOperation;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;

using Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal;
using Pomelo.EntityFrameworkCore.MySql.Migrations;

namespace ACR.DIR.DatabaseMigrations.DbContexts.MigrationsOperation.SqlGenerators;

/// <summary>
/// It overrides the MySQL's generator to handle the custom operations.
/// It is based on Microsoft <a href="https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/operations">Custom Migrations Operations</a>.
/// </summary>
internal class CustomMySqlMigrationsSqlGenerator : MySqlMigrationsSqlGenerator
{
    public CustomMySqlMigrationsSqlGenerator(MigrationsSqlGeneratorDependencies dependencies, ICommandBatchPreparer commandBatchPreparer, IMySqlOptions options) : base(dependencies, commandBatchPreparer, options)
    {
    }

    protected override void Generate(MigrationOperation operation, IModel model, MigrationCommandListBuilder builder)
    {
        if (operation is CreateJsonIndexOperation createJsonIndexOperation)
        {
            Generate(createJsonIndexOperation, builder);
        }
        else
        {
            base.Generate(operation, model, builder);
        }
    }

    private void Generate(CreateJsonIndexOperation operation, MigrationCommandListBuilder builder)
    {
        ISqlGenerationHelper sqlHelper = Dependencies.SqlGenerationHelper;

        builder
            .Append("ALTER TABLE ")
            .Append(sqlHelper.DelimitIdentifier(operation.Table))
            .Append(" ADD INDEX ")
            .Append(sqlHelper.DelimitIdentifier(operation.Name))
            .Append(" (( ")
            .Append(operation.Expression)
            .Append(" )) ")
            .Append("USING BTREE")
            .Append(sqlHelper.StatementTerminator)
            .EndCommand();

    }
}