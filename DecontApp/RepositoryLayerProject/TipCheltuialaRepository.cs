using DecontDbContext;
using DecontDbContext.Models;
using BusinessModels;
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

        public async Task<BusinessModels.TipCheltuiala[]> GetTipCheltuiala(bool? IsActive)
        {
            if (IsActive == null)
                return await _context.TipCheltuialas.Select(tc => new BusinessModels.TipCheltuiala
                {
                    Id = tc.Id,
                    Denumire = tc.Denumire,
                    ValoareImplicita = (decimal)tc.ValoareImplicita,
                    IsActive = tc.IsActive
                }).ToArrayAsync();
            else
                return await _context.TipCheltuialas.Select(tc => new BusinessModels.TipCheltuiala
				{
					Id = tc.Id,
					Denumire = tc.Denumire,
					ValoareImplicita = (decimal)tc.ValoareImplicita,
					IsActive = tc.IsActive
				}).Where(tc => tc.IsActive == IsActive).ToArrayAsync();
        }

        public async Task<BusinessModels.TipCheltuiala> GetCheltuialaById(int ID)
        {
            return await _context.TipCheltuialas.Select(tc => new BusinessModels.TipCheltuiala
			{
				Id = tc.Id,
				Denumire = tc.Denumire,
				ValoareImplicita = (decimal)tc.ValoareImplicita,
				IsActive = tc.IsActive
			}).Where(tc => tc.Id == ID).FirstOrDefaultAsync();
        }

        public async Task DeleteTipCheltuiala(int ID)
        {
            DecontDbContext.Models.TipCheltuiala tipCheltuiala = await _context.TipCheltuialas.Where(tc => tc.Id == ID).FirstOrDefaultAsync();
            tipCheltuiala.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task InsertTipCheltuiala(BusinessModels.TipCheltuiala tipCheltuiala)
        {
            DecontDbContext.Models.TipCheltuiala tp = new DecontDbContext.Models.TipCheltuiala
            {
                Id = tipCheltuiala.Id,
                Denumire = tipCheltuiala.Denumire,
                ValoareImplicita = tipCheltuiala.ValoareImplicita,
                IsActive = tipCheltuiala.IsActive
            };
            _context.TipCheltuialas.Add(tp);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTipCheltuiala(BusinessModels.TipCheltuiala tipCheltuiala)
        {
            DecontDbContext.Models.TipCheltuiala tipCheltuialaToUpdate = await _context.TipCheltuialas.Where(tc => tc.Id == tipCheltuiala.Id).FirstOrDefaultAsync();
            
            tipCheltuialaToUpdate.Denumire = tipCheltuiala.Denumire;
            tipCheltuialaToUpdate.ValoareImplicita = tipCheltuiala.ValoareImplicita;
            tipCheltuialaToUpdate.IsActive = tipCheltuiala.IsActive;

            await _context.SaveChangesAsync();
        }
    }
}
