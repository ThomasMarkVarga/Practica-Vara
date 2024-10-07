using DecontDbContext.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayerProject;

namespace DecontWebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TipCheltuialaController : ControllerBase
    {
        public readonly ITipCheltuialaRepository _cheltuialaRepo;

        public TipCheltuialaController(ITipCheltuialaRepository cheltuialaRepo)
        {
            _cheltuialaRepo = cheltuialaRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetTipCheltuiala(bool? IsActive)
        {
            return Ok(await _cheltuialaRepo.GetTipCheltuiala(IsActive));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTipCheltuiala(int ID)
        {
            await _cheltuialaRepo.DeleteTipCheltuiala(ID);

            TipCheltuiala tipCheltuiala = await _cheltuialaRepo.GetCheltuialaById(ID);

            if (tipCheltuiala.IsActive == false)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> SaveTipCheltuiala([FromBody] TipCheltuiala[] tipCheltuiala)
        { 
            foreach(var tc in tipCheltuiala)
            {
                if (tc.IsActive == null)
                    await _cheltuialaRepo.InsertTipCheltuiala(tc);
                else
                    await _cheltuialaRepo.UpdateTipCheltuiala(tc);
            }

            if (tipCheltuiala.Length == (await _cheltuialaRepo.GetTipCheltuiala(null)).Length)
                return Ok();
            return BadRequest();
        }
    }
}
