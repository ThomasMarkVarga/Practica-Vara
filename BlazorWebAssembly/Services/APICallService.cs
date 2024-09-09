using CompanyProject;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MessageAPIObjectProject;

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

        public async Task<Company[]> getAllCompaniesWithPagination(int pageSize, int pageNumber)
        {
            HttpResponseMessage response = await client.GetAsync("/GetAllCompaniesWithPagination?pageSize="+pageSize+"&pageNumber=" + pageNumber);
            string contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Company[]>(contentString);
        }

        public async Task<MessageObjectAPI> DeleteCompany(string CIF)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://localhost:7051/DeleteCompany?CIF=" + CIF)
            };
            HttpResponseMessage response = await client.SendAsync(request);

            MessageObjectAPI message = new MessageObjectAPI();
            if (response.IsSuccessStatusCode)
            {
                message.status = StatusCode.OK;
                message.SuccessMessage = "Company deleted succesfully";
            }
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                message.status = StatusCode.BadRequest;
                message.ErrorMessage = "Bad Request";
            }
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                message.status = StatusCode.NotFound;
                message.ErrorMessage = "Company not found!";
            }
            return message;
        }

        public async Task<MessageObjectAPI> AddCompany(Company company)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7051/InsertCompany?CIF=" + company.companyCIF + "&Name=" + company.companyName + "&Address=" + company.companyAddress + "&County=" + company.companyCounty + "&Phone=" + company.companyPhone)
            };

            HttpResponseMessage response = await client.SendAsync(request);

            MessageObjectAPI message = new MessageObjectAPI();
            if (response.IsSuccessStatusCode) { 
                message.status = StatusCode.OK;
            }
            else
            {
                message.status = StatusCode.BadRequest;
            }
            return message;
        }

        public async Task<MessageObjectAPI> UpdateCompany(string CIF, Company company)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri("https://localhost:7051/UpdateCompany?CIF=" + CIF + "&newCIF=" + company.companyCIF + "&newName=" + company.companyName + "&newAddress=" + company.companyAddress + "&newCounty=" + company.companyCounty + "&newPhone=" + company.companyPhone)
            };
            HttpResponseMessage response = await client.SendAsync(request);
            MessageObjectAPI message = new MessageObjectAPI();
            if (response.IsSuccessStatusCode)
                message.status = StatusCode.OK;
            else 
                message.status = StatusCode.BadRequest;

            return message;
        }

        public async Task<Company> GetCompany(string CIF)
        {
            HttpResponseMessage response = await client.GetAsync("/GetCompany?CIF=" + CIF);
            string contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Company>(contentString);
        }

        public async Task<int> GetNoOfCompanies()
        {
            HttpResponseMessage response = await client.GetAsync("/GetCompanyNo");
            string contentString = await response.Content.ReadAsStringAsync();
            return int.Parse(contentString);
        }
    }
}
