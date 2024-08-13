using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject;

namespace CompanyFinderAPICall
{
    public interface IDataRepository
    {
        Task<Company[]> getAllCompanies();
        Task<Company> getCompany(string CIF);
        Task insertCompany(string CIF, string Name, string Address, string County, string Phone);
        Task removeCompany(Company company);
        Task updateCompany(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone);
    }
}
