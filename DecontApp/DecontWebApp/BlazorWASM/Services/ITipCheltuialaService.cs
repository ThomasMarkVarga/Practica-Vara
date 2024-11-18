using BusinessModels;

namespace BlazorWASM.Services
{
	public interface ITipCheltuialaService
	{
		Task<TipCheltuiala[]> getTipCheltuiala(bool? IsActive);
		Task saveTipCheltuiala(TipCheltuiala[] tipcheltuiala);
	}
}
