using Microsoft.EntityFrameworkCore;
using WebAPI_DB.Entities;

namespace WebAPI_DB.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Company> Companies { get; set; }
    }
}
