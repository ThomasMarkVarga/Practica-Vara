using BusinessModels;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace DecontWebApp.Client.Services
{
	public class TipCheltuialaService : ITipCheltuialaService
	{
		public HttpClient client;

		public TipCheltuialaService()
		{
			this.client = new HttpClient();
			this.client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<TipCheltuiala[]> getTipCheltuiala(bool? IsActive)
		{
			Uri link;
			if (IsActive == null)
				link = new Uri("https://localhost:7299/TipCheltuiala/GetTipCheltuiala");
			else
				link = new Uri("https://localhost:7299/TipCheltuiala/GetTipCheltuiala?IsActive=" + IsActive);

			HttpResponseMessage response = await this.client.GetAsync(link);
			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();

				TipCheltuiala[] tc = JsonConvert.DeserializeObject<TipCheltuiala[]>(content);

				return tc;
			}
			return null;
		}

		public async Task saveTipCheltuiala(TipCheltuiala[] tipcheltuiala)
		{
			HttpResponseMessage response = await client.PostAsJsonAsync(new Uri("https://localhost:7299/TipCheltuiala/SaveTipCheltuiala"),tipcheltuiala);
		}
	}
}
