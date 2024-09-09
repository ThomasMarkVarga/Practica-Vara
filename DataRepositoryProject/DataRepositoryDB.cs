using CompanyProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbContextProj;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataRepositoryProject
{

    public enum SortDirections
    {
        None,
        Ascending,
        Descending
    }


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

        public async Task<Company[]> getAllCompaniesWithPagination(int skip,int pageSize, SortDirections sortDirection,string? sortString) {
            
            IQueryable<Company> query = _context.Companies;
            
            if((sortString != string.Empty || sortString != null) && sortDirection != SortDirections.None)
            {
                if(sortDirection == SortDirections.Descending)
                {
                    switch (sortString) {
                        case "companyCIF":
                            query = query.OrderByDescending(c => c.companyCIF);
                            break;
                        case "companyName":
                            query = query.OrderByDescending(c => c.companyName);
                            break;
                        case "companyAddress":
                            query = query.OrderByDescending(c => c.companyAddress);
                            break;
                        case "companyCounty":
                            query = query.OrderByDescending(c => c.companyCounty);
                            break;
                        case "companyPhone":
                            query = query.OrderByDescending(c => c.companyPhone);
                            break;
                    }
                }
                else if (sortDirection == SortDirections.Ascending)
                {
                    switch (sortString)
                    {
                        case "companyCIF":
                            query = query.OrderBy(c => c.companyCIF);
                            break;
                        case "companyName":
                            query = query.OrderBy(c => c.companyName);
                            break;
                        case "companyAddress":
                            query = query.OrderBy(c => c.companyAddress);
                            break;
                        case "companyCounty":
                            query = query.OrderBy(c => c.companyCounty);
                            break;
                        case "companyPhone":
                            query = query.OrderBy(c => c.companyPhone);
                            break;
                    }
                }
            }

            return await query.Skip(skip).Take(pageSize).ToArrayAsync();
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

        public async Task<int> getCompanyNo()
        {
            return _context.Companies.Count();
        }
    }
}
