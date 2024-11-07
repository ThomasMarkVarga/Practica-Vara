using BusinessModels;

namespace DecontWebApp.Client.Services
{
	public interface ITipCheltuialaService
	{
		Task<TipCheltuiala[]> getTipCheltuiala(bool? IsActive);
		Task saveTipCheltuiala(TipCheltuiala[] tipcheltuiala);
	}
}
