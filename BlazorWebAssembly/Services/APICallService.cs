﻿using CompanyProject;
using Newtonsoft.Json;

namespace BlazorWebAssembly.Services
{
    public class APICallService : IAPICallService
    {
        HttpClient client;

        public APICallService()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("https://localhost:7051");
            this.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Company[]> getAllCompanies()
        {
            HttpResponseMessage response = await client.GetAsync("/GetAllCompanies");
            string contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Company[]>(contentString);
        }

        public async Task DeleteCompany(string CIF)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://localhost:7051/DeleteCompany?CIF=" + CIF)
            };
            await client.SendAsync(request);
        }

        public async Task AddCompany(Company company)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7051/InsertCompany?CIF=" + company.companyCIF + "&Name=" + company.companyName + "&Address=" + company.companyAddress + "&County=" + company.companyCounty + "&Phone=" + company.companyPhone)
            };

            await client.SendAsync(request);
        }

        public async Task UpdateCompany(string CIF, Company company)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("https://localhost:7051/UpdateCompany?CIF=" + CIF + "&newCIF=" + company.companyCIF + "&newName=" + company.companyName + "&newAddress=" + company.companyAddress + "&newCounty=" + company.companyCounty + "&newPhone=" + company.companyPhone)
            };
            await client.SendAsync(request);
        }

        public async Task<Company> GetCompany(string CIF)
        {
            HttpResponseMessage response = await client.GetAsync("/GetCompany?CIF=" + CIF);
            string contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Company>(contentString);
        }
    }
}