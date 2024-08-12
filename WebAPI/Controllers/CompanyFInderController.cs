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
        public async Task<Company> GetCompany(string CIF)
        {
            return await _dataLayer.getCompany(CIF);
        }

        [HttpGet(Name = "Get All Companies")]
        public async Task<Company[]> GetAllCompanies()
        {
            return await _dataLayer.getAllCompanies();
        }

        [HttpPost(Name = "Insert Company")]
        public async Task InsertCompany(string CIF, string Name, string Address, string County, string Phone)
        {
            await _dataLayer.insertCompany(CIF, Name, Address, County, Phone);
        }

        [HttpDelete(Name = "Delete Company")]
        public async Task DeleteCompany(string CIF)
        {
            Company comp = await _dataLayer.getCompany(CIF);
            await _dataLayer.removeCompany(comp);
        }

        [HttpPut(Name = "Update Company")]
        public async Task UpdateCompany(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone)
        {
            await _dataLayer.updateCompany(CIF, newCIF, newName, newAddress, newCounty, newPhone);
        }
    }
}