using BusinessModels;

namespace DecontWebApp.Client.Services
{
	public interface IDocumentService
	{
		Task<DocumentTotal[]> getDocuments(bool? isActive);
		Task<List<Status>> getStatuses();
		Task<int> getMaxDocNo();
		Task<Document> GetDocumentByID(string ID);
		Task<string> SaveDocument(Document document);
		Task DeleteDocument(int ID);
	}
}
