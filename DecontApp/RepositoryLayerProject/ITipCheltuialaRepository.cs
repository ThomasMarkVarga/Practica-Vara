using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecontDbContext.Models;
using BusinessModels;

namespace RepositoryLayerProject
{
    public interface ITipCheltuialaRepository
    {
        Task<BusinessModels.TipCheltuiala[]> GetTipCheltuiala(bool? IsActive);
        Task<BusinessModels.TipCheltuiala> GetCheltuialaById(int ID);
        Task DeleteTipCheltuiala(int id);
        Task InsertTipCheltuiala(BusinessModels.TipCheltuiala tipCheltuiala);
        Task UpdateTipCheltuiala(BusinessModels.TipCheltuiala tipCheltuiala);
    }
}
