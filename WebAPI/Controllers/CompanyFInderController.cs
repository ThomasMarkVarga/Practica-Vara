using Microsoft.AspNetCore.Mvc;
using DataRepositoryProject;
using CompanyProject;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class CompanyFinderController : ControllerBase
    {
        private readonly IDataRepository _dataLayer;

        public CompanyFinderController(IDataRepository dataLayer)
        {
            _dataLayer = dataLayer;
        }

        [HttpGet(Name = "Get Company")]
        public async Task<IActionResult> GetCompany(string CIF)
        {
            Company comp = await _dataLayer.getCompany(CIF);
            if(comp == null)
            {
                return NotFound();
            }
            return Ok(comp);
        }

        [HttpGet(Name = "Get All Companies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            Company[] comp = await _dataLayer.getAllCompanies();
            if(comp == null)
            {
                return NoContent();
            }
            return Ok(comp);
        }

        [HttpPost(Name = "Insert Company")]
        public async Task<IActionResult> InsertCompany(string CIF, string Name, string Address, string County, string Phone)
        {
            await _dataLayer.insertCompany(CIF, Name, Address, County, Phone);
            Company comp = await _dataLayer.getCompany(CIF);
            if(comp == null)
            {
                return NotFound(); 
            }
            return Ok(comp);
        }

        [HttpDelete(Name = "Delete Company")]
        public async Task<IActionResult> DeleteCompany(string CIF)
        {
            Company comp = await _dataLayer.getCompany(CIF);
            if(comp == null)
            {
                return NotFound();
            }
            await _dataLayer.removeCompany(comp);
            comp = await _dataLayer.getCompany(CIF);
            if(comp != null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut(Name = "Update Company")]
        public async Task<IActionResult> UpdateCompany(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone)
        {
            Company comp = await _dataLayer.getCompany(CIF);
            if(comp == null)
            {
                return NotFound();
            }
            await _dataLayer.updateCompany(CIF, newCIF, newName, newAddress, newCounty, newPhone);
            comp = await _dataLayer.getCompany(newCIF);
            return Ok(comp);
        }
    }
}