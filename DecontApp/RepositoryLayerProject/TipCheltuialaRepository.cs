using DecontDbContext;
using DecontDbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayerProject
{
    public class TipCheltuialaRepository : ITipCheltuialaRepository
    {
        public readonly DecontDbContextCs _context;

        public TipCheltuialaRepository(DecontDbContextCs context)
        {
            _context = context;
        }

        public async Task<TipCheltuiala[]> GetTipCheltuiala(bool? IsActive)
        {
            if (IsActive == null)
                return await _context.TipCheltuialas.ToArrayAsync();
            else
                return await _context.TipCheltuialas.Where(tc => tc.IsActive == IsActive).ToArrayAsync();
        }

        public async Task<TipCheltuiala> GetCheltuialaById(int ID)
        {
            return await _context.TipCheltuialas.Where(tc => tc.Id == ID).FirstOrDefaultAsync();
        }

        public async Task DeleteTipCheltuiala(int ID)
        {
            TipCheltuiala tipCheltuiala = await _context.TipCheltuialas.Where(tc => tc.Id == ID).FirstOrDefaultAsync();
            tipCheltuiala.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task InsertTipCheltuiala(TipCheltuiala tipCheltuiala)
        {
            _context.TipCheltuialas.Add(tipCheltuiala);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTipCheltuiala(TipCheltuiala tipCheltuiala)
        {
            TipCheltuiala tipCheltuialaToUpdate = await _context.TipCheltuialas.Where(tc => tc.Id == tipCheltuiala.Id).FirstOrDefaultAsync();
            
            tipCheltuialaToUpdate.Denumire = tipCheltuiala.Denumire;
            tipCheltuialaToUpdate.ValoareImplicita = tipCheltuiala.ValoareImplicita;

            await _context.SaveChangesAsync();
        }
    }
}
