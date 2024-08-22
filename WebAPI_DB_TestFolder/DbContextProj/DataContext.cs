using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using CompanyProj;

namespace DbContextProj
{
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options) { }

            public DbSet<Company> Companies { get; set; }
        }
}
