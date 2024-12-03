using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACR.DIR.DatabaseMigrations.DbContexts.DbConnection
{
    internal class ConnectionDetails
    {
        [Required]
        public string Server { get; set; } = string.Empty;

        [Range(minimum: 1, maximum: 65535)]
        public uint Port { get; set; }

        [Required]
        public string Database { get; set; } = string.Empty;

        [Required]
        public string Secret { get; set; } = string.Empty;
    }
}
