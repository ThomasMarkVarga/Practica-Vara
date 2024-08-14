using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CompanyProject;
using MessageAPIObjectProject;

namespace CompanyFinderAPICall
{
    internal class CallAPI : ICallAPI
    {
        HttpClient client;

        public CallAPI()
        {
            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        // takes specific company from api using CIF
        public async Task<(Company,MessageObjectAPI)> getCompanyAPI(string CIF)
        {
            MessageObjectAPI messageObj = new MessageObjectAPI();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7234/GetCompany?CIF=" + CIF);

            if (!response.IsSuccessStatusCode)
            {
                messageObj.status = StatusCode.NotFound;
                messageObj.ErrorMessage = "Company not found!";

                return (null, messageObj);
            }
            string contentString = await response.Content.ReadAsStringAsync();
            Company comp = JsonConvert.DeserializeObject<Company>(contentString);
            messageObj.status = StatusCode.OK;

            return (comp, messageObj);
        }

        // takes all companies saved from the API
        public async Task<(Company[], MessageObjectAPI)> getAllCompaniesAPI()
        {
            MessageObjectAPI messageObj = new MessageObjectAPI();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7234/GetAllCompanies");

            if (!response.IsSuccessStatusCode)
            {
                messageObj.status = StatusCode.NoContent;
                messageObj.ErrorMessage = "No contents found!";

                return (null, messageObj);
            }
            string contentString = await response.Content.ReadAsStringAsync();
            Company[] comp = JsonConvert.DeserializeObject<Company[]>(contentString);
            messageObj.status = StatusCode.OK;

            return (comp, messageObj);
        }

        // delete company using CIF 
        public async Task<MessageObjectAPI> deleteCompanyAPI(string CIF)
        {
            MessageObjectAPI message = new MessageObjectAPI();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://localhost:7234/DeleteCompany?CIF=" + CIF)
            };

            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                message.status = StatusCode.OK;
                message.SuccessMessage = "Company deleted succesfully";
            }
            if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                message.status = StatusCode.BadRequest;
                message.ErrorMessage = "Bad Request";
            }
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                message.status = StatusCode.NotFound;
                message.ErrorMessage = "Company not found!";
            }
            return message;
        }

        public async Task<(Company, MessageObjectAPI)> insertCompanyAPI(string CIF, string Name, string Address, string County, string Phone)
        {
            MessageObjectAPI message = new MessageObjectAPI();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7234/InsertCompany?CIF=" + CIF + "&Name=" + Name + "&Address=" + Address + "&County=" + County + "&Phone=" + Phone)
            };

            var response = await client.SendAsync(request);
            
            if(response.IsSuccessStatusCode)
            {
                message.status = StatusCode.OK;
                message.SuccessMessage = "Inserted successfuly!";

                string jsonString = await response.Content.ReadAsStringAsync();
                Company comp = JsonConvert.DeserializeObject<Company>(jsonString);

                return (comp, message);
            }
            else
            {
                message.status = StatusCode.NotFound;
                message.ErrorMessage = "Couldn't insert company!";

                return (null, message);
            }
        }

        public async Task<(Company, MessageObjectAPI)> updateCompanyAPI(string CIF, string newCIF, string newName, string newAddress, string newCounty, string newPhone)
        {
            MessageObjectAPI message = new MessageObjectAPI();
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Put, 
                RequestUri = new Uri("https://localhost:7234/UpdateCompany?CIF=" + CIF + "&newCIF=" + newCIF + "&newName=" + newName + "&newAddress=" + newAddress + "&newCounty=" + newCounty + "&newPhone=" + newPhone)
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                message.status = StatusCode.OK;
                message.SuccessMessage = "Company updated successfuly!";

                string jsonString = await response.Content.ReadAsStringAsync();
                Company comp = JsonConvert.DeserializeObject<Company>(jsonString);

                return (comp, message);
            }
            else
            {
                message.status = StatusCode.NotFound;
                message.ErrorMessage = "No company found to update!";

                return (null, message);
            }
        }

    }
}
