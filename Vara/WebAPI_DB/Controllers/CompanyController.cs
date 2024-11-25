using DataRepositoryProject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CompanyProject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPI_DB.Controllers
{
    public enum SortDirection
    {
        None,
        Ascending,
        Descending
    }

    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public readonly IDataRepository _dataLayer;

        public CompanyController(IDataRepository repo)
        {
            _dataLayer = repo;
        }

        [HttpGet(Name = "Get Company")]
        public async Task<IActionResult> GetCompany(string CIF)
        {
            Company comp = await _dataLayer.getCompany(CIF);
            if (comp == null)
            {
                return NotFound();
            }
            return Ok(comp);
        }

        [HttpGet(Name = "Get All Companies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            Company[] comp = await _dataLayer.getAllCompanies();
            if (comp == null)
            {
                return NoContent();
            }
            return Ok(comp);
        }

        [HttpGet(Name = "Get All Companies With Pagination")]
        public async Task<IActionResult> GetAllCompaniesWithPagination(int pageSize, int pageNumber, SortDirection sortDirection, string? sortString, [FromQuery]Dictionary<string, string>? filters)
        {
            int skip = (pageNumber - 1) * pageSize;

            var (count, comp) = await _dataLayer.getAllCompaniesWithPagination(skip, pageSize, (DataRepositoryProject.SortDirections)sortDirection, sortString, filters);
            if (comp == null)
            {
                return NoContent();
            }
            return Ok(new
            {
                count = count,
                comp=comp
            });

        }

        [HttpPost(Name = "Insert Company")]
        public async Task<IActionResult> InsertCompany(string CIF, string Name, string Address, string County, string Phone)
        {
            await _dataLayer.insertCompany(CIF, Name, Address, County, Phone);
            Company comp = await _dataLayer.getCompany(CIF);
            if (comp == null)
            {
                return NotFound();
            }
            return Ok(comp);
        }

        [HttpDelete(Name = "Delete Company")]
        public async Task<IActionResult> DeleteCompany(string CIF)
        {
            Company comp = await _dataLayer.getCompany(CIF);
            if (comp == null)
            {
                return NotFound();
            }
            await _dataLayer.removeCompany(comp);
            comp = await _dataLayer.getCompany(CIF);
            if (comp != null)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut(Name = "Update Company")]
        public async Task<IActionResult> UpdateCompany(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone)
        {
            Company comp = await _dataLayer.getCompany(CIF);
            if (comp == null)
            {
                return NotFound();
            }
            await _dataLayer.updateCompany(CIF, newCIF, newName, newAddress, newCounty, newPhone);
            comp = await _dataLayer.getCompany(newCIF);
            return Ok(comp);
        }
    }
}
