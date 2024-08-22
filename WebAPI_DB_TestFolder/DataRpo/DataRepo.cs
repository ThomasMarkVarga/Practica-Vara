using Microsoft.EntityFrameworkCore;
using CompanyProj;
using DbContextProj;

namespace DataRpo
{
    public class DataRepo : IDataRepo
    {
        private readonly DataContext _context;

        public DataRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> getAllComp()
        {
            var comp = await _context.Companies.ToListAsync();
            return comp;
        }
    }
}
