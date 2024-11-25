using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using CompanyProject;

namespace DbContextProj
{
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options) { }

            public DbSet<Company> Companies { get; set; }
        }
}
