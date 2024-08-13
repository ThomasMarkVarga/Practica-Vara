using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CompanyProject;
using Newtonsoft.Json;

namespace CompanyFinderAPICall
{
    internal class CallAPI
    {
        HttpClient client;

        public CallAPI()
        {
            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        // takes specific company from api using CIF
        public async Task<Company> getCompanyAPI(string CIF)
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7234/GetCompany?CIF=" + CIF);

            if (response.IsSuccessStatusCode)
            {
                string contentString = await response.Content.ReadAsStringAsync();

                Company comp = JsonConvert.DeserializeObject<Company>(contentString);
                return comp;
            }
            return null;
        }

        // takes all companies saved from the API
        public async Task<Company[]> getAllCompaniesAPI()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7234/GetAllCompanies");

            if (response.IsSuccessStatusCode)
            {
                string contentString = await response.Content.ReadAsStringAsync();

                Company[] comp = JsonConvert.DeserializeObject<Company[]>(contentString);
                return comp;
            }
            return null;
        }

        // delete company using CIF 
        public async Task deleteCompanyAPI(string CIF)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                //Content = new StringContent(CIF,Encoding.UTF8,"application/json"),
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://localhost:7234/DeleteCompany?CIF=" + CIF)
            };

            await client.SendAsync(request);
        }

        public async Task insertCompanyAPI(string CIF, string Name, string Address, string County, string Phone)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7234/InsertCompany?CIF=" + CIF + "&Name=" + Name + "&Address=" + Address + "&County=" + County + "&Phone=" + Phone)
            };

            await client.SendAsync(request);
        }

        public async Task updateCompanyAPI(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Put, 
                RequestUri = new Uri("https://localhost:7234/UpdateCompany?CIF=" + CIF + "&newCIF=" + newCIF + "&newName=" + newName + "&newAddress=" + newAddress + "&newCounty=" + newCounty + "&newPhone=" + newPhone)
            };

            await client.SendAsync(request);
        }

    }
}
