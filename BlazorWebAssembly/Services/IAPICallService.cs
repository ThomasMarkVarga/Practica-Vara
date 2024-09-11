using BlazorBootstrap;
using CompanyProject;
using MessageAPIObjectProject;

namespace BlazorWebAssembly.Services
{
    public interface IAPICallService
    {
        Task<Company[]> getAllCompanies();
        Task<(int,Company[])> getAllCompaniesWithPagination(IEnumerable<FilterItem> filters, int pageSize, int pageNumber, string sortString, SortDirection sortDirection);
        Task<MessageObjectAPI> DeleteCompany(string CIF);
        Task<MessageObjectAPI> AddCompany(Company company);
        Task<MessageObjectAPI> UpdateCompany(string CIF, Company company);
        Task<Company> GetCompany(string CIF);
        Task<int> GetNoOfCompanies();
    }
}
