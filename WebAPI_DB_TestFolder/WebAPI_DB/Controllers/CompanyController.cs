using DataRpo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyProj;

namespace WebAPI_DB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public readonly IDataRepo _repo;

        public CompanyController(IDataRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetAllCompanies()
        {
            var comps = await _repo.getAllComp();
            if(comps is null)
                return NotFound();
            return Ok(comps);
        }
    }
}
