using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecontDbContext.Models;
using DocumentTotalProj;

namespace RepositoryLayerProject
{
    public interface IDocumentRepository
    {
        Task<List<DocumentTotal>> GetDocuments(bool? IsActive);
        Task<Document> GetDocumentById(int ID);
        Task DeleteDocument(int ID);
        Task InsertDocument(Document doc);
        Task UpdateDocument(Document doc);
        Task InsertRand(RandDocument rand);
        Task UpdateRand(RandDocument rand);
        Task<RandDocument[]> GetRand(int docId);
    }
}
