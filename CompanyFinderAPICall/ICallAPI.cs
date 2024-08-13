using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject;

namespace CompanyFinderAPICall
{
    public interface ICallAPI
    {
        Task<Company> getCompanyAPI(string CIF);
        Task<Company[]> getAllCompaniesAPI();
        Task deleteCompanyAPI(string CIF);
        Task insertCompanyAPI(string CIF, string Name, string Address, string County, string Phone);
        Task updateCompanyAPI(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone);
    }
}
