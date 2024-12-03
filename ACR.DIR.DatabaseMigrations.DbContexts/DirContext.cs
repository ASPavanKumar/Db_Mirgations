using ACR.DIR.DbEntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace ACR.DIR.DatabaseMigrations.DbContexts
{
    public class DirContext : DbContext
    {
        public DirContext(DbContextOptions<DirContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DcmObject>();
            modelBuilder.Entity<DcmSeries>();
            modelBuilder.Entity<DcmStudy>();
            modelBuilder.Entity<DirTransaction>();
            modelBuilder.Entity<ReprocessAttempts>();

            modelBuilder.UseCbsForeignKeyConstraintNameConvention();
            modelBuilder.UseCbsIndexNameConvention();
        }
    }
}
