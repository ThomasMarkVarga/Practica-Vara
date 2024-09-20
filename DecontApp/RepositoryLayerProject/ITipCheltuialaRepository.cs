using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecontDbContext.Models;

namespace RepositoryLayerProject
{
    public interface ITipCheltuialaRepository
    {
        Task<TipCheltuiala[]> GetTipCheltuiala(bool? IsActive);
        Task<TipCheltuiala> GetCheltuialaById(int ID);
        Task DeleteTipCheltuiala(int id);
        Task InsertTipCheltuiala(TipCheltuiala tipCheltuiala);
        Task UpdateTipCheltuiala(TipCheltuiala tipCheltuiala);
    }
}
