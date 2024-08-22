using CompanyProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbContextProj;
using Microsoft.EntityFrameworkCore;

namespace DataRepositoryProject
{
    public class DataRepositoryDB : IDataRepository
    {
        public readonly DataContext _context;

        public DataRepositoryDB(DataContext context) {
            _context = context;
        }
        public async Task<Company[]> getAllCompanies()
        {
            var companies = await _context.Companies.ToArrayAsync();
            return companies;
        }

        public async Task<Company> getCompany(string CIF)
        {
            var company = await _context.Companies.Where(c => c.companyCIF == CIF).FirstOrDefaultAsync();
            return company;
        }

        public async Task insertCompany(string CIF, string Name, string Address, string County, string Phone)
        {
            Company comp = new Company();
            comp.companyCIF = CIF;
            comp.companyName = Name;
            comp.companyAddress = Address;
            comp.companyCounty = County;
            comp.companyPhone = Phone;

            _context.Companies.Add(comp);
            await _context.SaveChangesAsync();
        }

        public async Task removeCompany(Company company)
        {
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }

        public async Task updateCompany(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone)
        {
            var company = _context.Companies.Where(c => c.companyCIF == CIF).FirstOrDefault();
            company.companyCIF = newCIF;
            company.companyName = newName;
            company.companyAddress = newAddress;
            company.companyCounty = newCounty;
            company.companyPhone = newPhone;

            await _context.SaveChangesAsync();
        }
    }
}
