using CompanyProject;

namespace BlazorWebAssembly.Services
{
    public interface IAPICallService
    {
        Task<Company[]> getAllCompanies();
        Task DeleteCompany(string CIF);
        Task AddCompany(Company company);
        Task UpdateCompany(string CIF, Company company);
        Task<Company> GetCompany(string CIF);
    }
}
