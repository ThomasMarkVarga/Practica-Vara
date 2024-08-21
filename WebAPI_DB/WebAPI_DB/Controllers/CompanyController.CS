using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_DB.Data;
using WebAPI_DB.Entities;

namespace WebAPI_DB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public readonly DataContext _context;

        public CompanyController(DataContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<ActionResult<List<Company>>> GetAllCompanies()
        {
            var companies = await _context.Companies.ToListAsync();

            return Ok(companies);
        }

        [HttpGet("{CIF}")]
        public async Task<ActionResult<Company>> GetCompany(string CIF)
        {
            var company =  _context.Companies.Where(c => c.CIF.ToUpper() == CIF.ToUpper());
            if (company is null)
                return NotFound("Company not found!");

            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult<Company>> AddCompany(Company company)
        {
            _context.Companies.Add(company);

            await _context.SaveChangesAsync();

            return Ok(_context.Companies.Where(c => c.CIF.ToUpper() == company.CIF.ToUpper()));
        }

        [HttpPut]
        public async Task<ActionResult<Company>> UpdateCompany(Company updatedCompany)
        {
            var dbCompany = await _context.Companies.FindAsync(updatedCompany.Id);

            if (dbCompany is null)
                return NotFound("Company not found!");

            dbCompany.CIF = updatedCompany.CIF;
            dbCompany.Name = updatedCompany.Name;
            dbCompany.Address = updatedCompany.Address;
            dbCompany.County = updatedCompany.County;
            dbCompany.Phone = updatedCompany.Phone;

            await _context.SaveChangesAsync();

            return Ok(_context.Companies.Where(c => c.CIF.ToUpper() == updatedCompany.CIF.ToUpper()));
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Company>>> DeleteCompany(int Id)
        {
            var company = await _context.Companies.FindAsync(Id);
            if (company is null)
                return NotFound("Company not found!");

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return Ok(await _context.Companies.ToListAsync());
        }
    }
}
