using Newtonsoft.Json;
using System.Configuration;
using CompanyProject;

namespace DataRepositoryProject
{
    public class StorageFile
    {
        private static string path = ConfigurationManager.AppSettings["path"];

        public static async Task<List<Company>> ReadCompanies()
        {
            List<Company> companies = new List<Company>();

            using (StreamReader r = new StreamReader(path))
            {
                string json = await r.ReadToEndAsync();
                companies = JsonConvert.DeserializeObject<List<Company>>(json);
            }
            return companies;
        }

        public static async Task WriteCompaniesToFile(List<Company> companies)
        {
            string companiesString = JsonConvert.SerializeObject(companies.ToArray());
            using (StreamWriter writer = new StreamWriter(path))
            {
                await writer.WriteAsync(companiesString);
            }
        }
    }

    public class DataRepository : IDataRepository
    {
        private List<Company> companiesCache { get; set; }


        private async Task<List<Company>> getCompaniesCache()
        {
            if (companiesCache == null)
            {
                companiesCache = await StorageFile.ReadCompanies();
            }

            return companiesCache;
        }

        public async Task<Company[]> getAllCompanies()
        {
            var company = (from comp in await getCompaniesCache()
                           select comp).ToArray();
            return company;
        }

        public async Task<Company[]> getAllCompaniesWithPagination(
    int skip,
    int pageSize)
        {
            return null;
        }

        public async Task<Company> getCompany(string CIF)
        {
            var company = (await getCompaniesCache()).FirstOrDefault(c => c.companyCIF == CIF);

            return company;
        }

        public async Task insertCompany(string CIF, string Name, string Address, string County, string Phone)
        {
            Company company = new Company();

            company.companyCIF = CIF;
            company.companyName = Name;
            company.companyAddress = Address;
            company.companyCounty = County;
            company.companyPhone = Phone;

            (await getCompaniesCache()).Add(company);
            await StorageFile.WriteCompaniesToFile(await getCompaniesCache());
        }

        public async Task<Company[]> searchName(string Name)
        {
            var company = (from comp in await getCompaniesCache()
                           where comp.companyName.ToLower().Contains(Name.ToLower())
                           select comp).ToArray();
            return company;
        }

        public async Task<Company[]> seachAddress(string Address)
        {
            var company = (from comp in await getCompaniesCache()
                           where comp.companyAddress.ToLower().Contains(Address.ToLower())
                           select comp).ToArray();
            return company;
        }

        public async Task<Company[]> searchCity(string City)
        {
            var company = (from comp in await getCompaniesCache()
                           where comp.companyAddress.ToLower().Contains(City.ToLower())
                           select comp).ToArray();
            return company;
        }

        public async Task<int> noOfCompInCounty(string County)
        {
            var no = (from comp in await getCompaniesCache()
                      where comp.companyCounty.ToLower().Contains(County.ToLower())
                      select comp).Count();
            return no;
        }

        public async Task removeCompany(Company company)
        {
            (await getCompaniesCache()).Remove(company);
            await StorageFile.WriteCompaniesToFile(await getCompaniesCache());
        }

        public async Task updateCompany(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone)
        {
            Company company = (await getCompaniesCache()).FirstOrDefault(c => c.companyCIF == CIF);

            company.companyCIF = newCIF;
            company.companyName = newName;
            company.companyAddress = newAddress;
            company.companyCounty = newCounty;
            company.companyPhone = newPhone;

            await StorageFile.WriteCompaniesToFile(await getCompaniesCache());
        }

        public async Task<int> getCompanyNo()
        {
            return companiesCache.Count;
        }
    }
}