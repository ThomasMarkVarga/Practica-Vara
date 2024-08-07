using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Console_Apelare_API
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

    public class Company
    {
        public string companyCIF { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyCounty { get; set; }
        public string companyPhone { get; set; }

        public override string ToString()
        {
            return "{\n" + this.companyCIF + "\n"
                + this.companyName + "\n"
                + this.companyAddress + "\n"
                + this.companyCounty + "\n"
                + this.companyPhone + "\n}";
        }
    }


    internal class DataRepository
    {
        private List<Company> companies { get; set; }

        public DataRepository()
        {
            InitializeAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeAsync()
        {
            companies = await StorageFile.ReadCompanies();

            await Task.Delay(100);
        }

        public Company[] getAllCompanies()
        {
            var company = (from comp in companies
                           select comp).ToArray();
            return company;
        }

        public Company getCompany(string CIF)
        {
            var company = this.companies.FirstOrDefault(c => c.companyCIF == CIF);

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

            this.companies.Add(company);
            await StorageFile.WriteCompaniesToFile(this.companies);
        }

        public Company[] searchName(string Name)
        {
            var company = (from comp in companies
                           where comp.companyName.ToLower().Contains(Name.ToLower())
                           select comp).ToArray();
            return company;
        }

        public Company[] seachAddress(string Address)
        {
            var company = (from comp in companies
                           where comp.companyAddress.ToLower().Contains(Address.ToLower())
                           select comp).ToArray();
            return company;
        }

        public Company[] searchCity(string City)
        {
            var company = (from comp in companies
                           where comp.companyAddress.ToLower().Contains(City.ToLower())
                           select comp).ToArray();
            return company;
        }

        public int noOfCompInCounty(string County)
        {
            var no = (from comp in companies
                      where comp.companyCounty.ToLower().Contains(County.ToLower())
                      select comp).Count();
            return no;
        }

        public async Task removeCompany(Company company)
        {
            this.companies.Remove(company);
            await StorageFile.WriteCompaniesToFile(this.companies);
        }

        public async Task updateCompany(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone)
        {
            Company company = this.companies.FirstOrDefault(c => c.companyCIF == CIF);

            company.companyCIF = newCIF;
            company.companyName = newName;
            company.companyAddress = newAddress;
            company.companyCounty = newCounty;
            company.companyPhone = newPhone;

            await StorageFile.WriteCompaniesToFile(this.companies);
        }
    }
}
