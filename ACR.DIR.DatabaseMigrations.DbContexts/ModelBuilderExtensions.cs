using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACR.DIR.DatabaseMigrations.DbContexts
{
    internal static class ModelBuilderExtensions
    {
        internal static ModelBuilder UseCbsForeignKeyConstraintNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                string? table = entityType.GetTableName();

                if (table is null) continue;

                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    foreach (IMutableForeignKey fk in entityType.FindForeignKeys(property))
                    {
                        string? referencedTable = fk.PrincipalEntityType.GetTableName();

                        if (referencedTable is null) continue;

                        string referencedTableColumns = string.Join('_', fk.PrincipalKey.Properties.Select(key => key.GetColumnName()));

                        string cbsConstraintName = $"fk_{table}_{referencedTable}_{referencedTableColumns}";

                        fk.SetConstraintName(cbsConstraintName);
                    }
                }
            }

            return modelBuilder;
        }

        internal static ModelBuilder UseCbsIndexNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                string? table = entityType.GetTableName();

                if (table is null) continue;

                foreach (IMutableIndex index in entityType.GetIndexes())
                {
                    if (!string.IsNullOrEmpty(index.Name)) continue;

                    bool hasForeignKeyColumn = index.Properties.Any(property => property.IsForeignKey());

                    string columns = string.Join('_', index.Properties.Select(property => property.GetColumnName()));

                    string cbsIndexName = $"{(hasForeignKeyColumn ? "fk_" : string.Empty)}{table}_{columns}{(index.IsUnique ? "_UNIQUE" : "_idx")}";

                    index.SetDatabaseName(cbsIndexName);
                }
            }

            return modelBuilder;
        }
    }
}
