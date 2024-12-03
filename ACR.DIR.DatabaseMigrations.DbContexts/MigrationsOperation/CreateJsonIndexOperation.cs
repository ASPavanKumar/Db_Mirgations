using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace ACR.DIR.DatabaseMigrations.DbContexts.MigrationsOperation;

internal class CreateJsonIndexOperation : MigrationOperation
{
    public CreateJsonIndexOperation(string name, string table, string expression)
    {
        Name = name;
        Table = table;
        Expression = expression;
    }

    /// <summary>
    /// Index Name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Table Name.
    /// </summary>
    public string Table { get; }

    /// <summary>
    /// Expression involving the table's columns.
    /// </summary>
    public string Expression { get; }
}