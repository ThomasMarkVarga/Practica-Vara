using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProject;
using MessageAPIObjectProject;

namespace CompanyFinderAPICall
{
    public interface ICallAPI
    {
        Task<(Company, MessageObjectAPI)> getCompanyAPI(string CIF);
        Task<(Company[], MessageObjectAPI)> getAllCompaniesAPI();
        Task<MessageObjectAPI> deleteCompanyAPI(string CIF);
        Task<(Company, MessageObjectAPI)> insertCompanyAPI(string CIF, string Name, string Address, string County, string Phone);
        Task<(Company, MessageObjectAPI)> updateCompanyAPI(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone);
    }
}
