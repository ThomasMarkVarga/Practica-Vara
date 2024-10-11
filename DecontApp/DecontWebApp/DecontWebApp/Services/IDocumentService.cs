using BusinessModels;

namespace DecontWebApp.Services
{
	public interface IDocumentService
	{
		Task<DocumentTotal[]> getDocuments(bool? isActive);
		Task<List<Status>> getStatuses();
	}
}
