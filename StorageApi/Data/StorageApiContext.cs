using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StorageApi.Data
{
    public class StorageApiContext : DbContext
    {
        public StorageApiContext (DbContextOptions<StorageApiContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
    }
}
