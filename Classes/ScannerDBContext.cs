using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskScanner.Classes
{
    public class ScannerDBContext : DbContext
    {
        public ScannerDBContext() : base("DbConnectionString")
        {

        }
        public DbSet<ScannerData> data { get; set; }

        
    }
}
