using DecontDbContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentTotalProj;

namespace RepositoryLayerProject
{
    public class DocumentRepository : IDocumentRepository
    {
        public readonly DecontDbContextCs _context;

        public DocumentRepository(DecontDbContextCs context)
        {
            _context = context;
        }

        public async Task<List<DocumentTotal>> GetDocuments(bool? IsActive)
        {
            List<DocumentTotal> docTotal = new List<DocumentTotal>();
            Document[] documents;

            if (IsActive == null)
                documents = await _context.Documents.ToArrayAsync();
            else
                documents = await _context.Documents.Where(d => d.IsActive == IsActive).ToArrayAsync();

            foreach (var document in documents) {
                RandDocument[] rand = await _context.RandDocuments.Where(r => r.DocumentId == document.Id).ToArrayAsync();
                decimal sum = 0;
                
                foreach (var r in rand) {
                    sum += r.Valoare;
                }

                docTotal.Add(new DocumentTotal(document, sum));
            }
 
            return docTotal;
        }

        public async Task<Document> GetDocumentById(int ID)
        {
            return await _context.Documents.Where(d => d.Id == ID).FirstOrDefaultAsync();
        }

        public async Task DeleteDocument(int ID)
        {
            Document doc = await _context.Documents.Where(d => d.Id == ID).FirstOrDefaultAsync();
            doc.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task InsertDocument(Document doc)
        {
            _context.Documents.Add(doc);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDocument(Document doc)
        {
            Document documentToUpdate = await _context.Documents.Where(d => d.Id == doc.Id).FirstOrDefaultAsync();

            documentToUpdate.Numar = doc.Numar;
            documentToUpdate.Data = doc.Data;
            documentToUpdate.Explicatie = doc.Explicatie;
            documentToUpdate.StatusId = doc.StatusId;
            documentToUpdate.DataPlata = doc.DataPlata;

            await _context.SaveChangesAsync();
        }

        public async Task InsertRand(RandDocument rand)
        {
            _context.RandDocuments.Add(rand);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRand(RandDocument rand)
        {
            RandDocument randToUpdate = await _context.RandDocuments.Where(r => r.Id == rand.Id).FirstOrDefaultAsync();
           
            randToUpdate.CheltuialaId = rand.CheltuialaId;
            randToUpdate.Explicatie = rand.Explicatie;
            randToUpdate.Valoare = rand.Valoare;

            await _context.SaveChangesAsync();
        }

        public async Task<RandDocument[]> GetRand(int docId)
        {
            return await _context.RandDocuments.Where(r => r.DocumentId == docId).ToArrayAsync();
        }
    }
}
