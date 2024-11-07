using BusinessModels;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DecontWebApp.Client.Services
{
	public class DocumentService : IDocumentService
	{
		public HttpClient client;

		public DocumentService()
		{
			this.client = new HttpClient();
			this.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<DocumentTotal[]> getDocuments(bool? isActive)
		{
			Uri link;
			if (isActive == null)
				link = new Uri("https://localhost:7299/Document/GetDocuments");
			else
				link = new Uri("https://localhost:7299/Document/GetDocuments?IsActive=" + isActive);

			HttpResponseMessage response = await client.GetAsync(link);
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

		public async Task<int> getMaxDocNo()
		{
			HttpResponseMessage response = await client.GetAsync(new Uri("https://localhost:7299/Document/DocumentMax"));
			if (response.IsSuccessStatusCode)
			{
				string contentString = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<int>(contentString);
			}

			return 0;
		}

		public async Task<Document> GetDocumentByID(string ID)
		{
			HttpResponseMessage response = await client.GetAsync(new Uri("https://localhost:7299/Document/GetDocumentById?ID=" + ID));
			if (response.IsSuccessStatusCode) { 
				string contentString = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<Document>(contentString);
			}

			return null;
		}

		public async Task<string> SaveDocument(Document document)
		{
			HttpResponseMessage response = await client.PostAsJsonAsync(new Uri("https://localhost:7299/Document/SaveDocument"), document);
			return response.StatusCode.ToString();
		}

		public async Task DeleteDocument(int ID)
		{
			HttpResponseMessage response = await client.PostAsync(new Uri("https://localhost:7299/Document/DeleteDocument?ID=" + ID), null);
		}
	}
}
