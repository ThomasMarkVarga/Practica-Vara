﻿using CompanyProject;
using MessageAPIObjectProject;

namespace BlazorWebAssembly.Services
{
    public interface IAPICallService
    {
        Task<Company[]> getAllCompanies();
        Task<Company[]> getAllCompaniesWithPagination(int pageSize, int pageNumber);
        Task<MessageObjectAPI> DeleteCompany(string CIF);
        Task<MessageObjectAPI> AddCompany(Company company);
        Task<MessageObjectAPI> UpdateCompany(string CIF, Company company);
        Task<Company> GetCompany(string CIF);
        Task<int> GetNoOfCompanies();
    }
}
