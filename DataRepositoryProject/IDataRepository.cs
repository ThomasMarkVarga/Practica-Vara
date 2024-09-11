using CompanyProject;
using System.ComponentModel;

namespace DataRepositoryProject
{
    public interface IDataRepository
    {
        Task<Company[]> getAllCompanies();
        Task<(int, Company[])> getAllCompaniesWithPagination(int skip, int pageSize, SortDirections sortDirection, string? sortString, Dictionary<string, string>? filters);
        Task<IQueryable<Company>> sortQuery(IQueryable<Company> query, SortDirections sortDirections, string sortString);
        Task<Company> getCompany(string CIF);
        Task insertCompany(string CIF, string Name, string Address, string County, string Phone);
        Task removeCompany(Company company);
        Task updateCompany(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone);
        Task<int> getCompanyNo(IQueryable<Company> query);
    }
}
