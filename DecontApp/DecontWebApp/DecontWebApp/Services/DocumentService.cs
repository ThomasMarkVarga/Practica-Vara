using BusinessModels;
using Newtonsoft.Json;

namespace DecontWebApp.Services
{
	public class DocumentService : IDocumentService
	{
		public HttpClient client;

		public DocumentService() { 
			this.client = new HttpClient();
			this.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<DocumentTotal[]> getDocuments(bool? isActive)
		{
			HttpResponseMessage response = await client.GetAsync(new Uri("https://localhost:7299/Document/GetDocuments"));
			if (response.IsSuccessStatusCode)
			{
				string stringContent = await response.Content.ReadAsStringAsync();


				DocumentTotal[] documents = JsonConvert.DeserializeObject<DocumentTotal[]>(stringContent);

				return documents;
			}

			return null;
		}

		public async Task<List<Status>> getStatuses()
		{
			HttpResponseMessage response = await client.GetAsync(new Uri("https://localhost:7299/Document/GetStatuses"));
			if (response.IsSuccessStatusCode)
			{
				string contentString = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<List<Status>>(contentString);
			}
			return null;
		}
	}
}
