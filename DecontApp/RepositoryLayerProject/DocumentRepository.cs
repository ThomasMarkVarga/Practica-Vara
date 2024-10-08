using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModels;
using DecontDbContext;

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
            List<Document> documents = new List<Document>();
            if (IsActive == null)
                 documents = _context.Documents
                                        .Select(doc => new Document
                                        {
                                            Id = doc.Id,
                                            Numar = doc.Numar,
                                            Data = doc.Data,
                                            Explicatie = doc.Explicatie,
                                            StatusId = doc.StatusId,
                                            DataPlata = doc.DataPlata,
                                            IsActive = doc.IsActive,
                                            RandDocuments = doc.RandDocuments
                                                              .Select(rd => new BusinessModels.RandDocument
                                                              {
                                                                  Id = rd.Id,
                                                                  DocumentId = rd.DocumentId,
                                                                  CheltuialaId = rd.CheltuialaId,
                                                                  Explicatie = rd.Explicatie,
                                                                  Valoare = rd.Valoare,
                                                                  IsActive = rd.IsActive
                                                              })
                                                              .ToList()
                                        })
                                        .ToList();

            else
                documents = _context.Documents
                                        .Select(doc => new Document
                                        {
                                            Id = doc.Id,
                                            Numar = doc.Numar,
                                            Data = doc.Data,
                                            Explicatie = doc.Explicatie,
                                            StatusId = doc.StatusId,
                                            DataPlata = doc.DataPlata,
                                            IsActive = doc.IsActive,
                                            RandDocuments = doc.RandDocuments
                                                              .Select(rd => new BusinessModels.RandDocument
                                                              {
                                                                  Id = rd.Id,
                                                                  DocumentId = rd.DocumentId,
                                                                  CheltuialaId = rd.CheltuialaId,
                                                                  Explicatie = rd.Explicatie,
                                                                  Valoare = rd.Valoare,
                                                                  IsActive = rd.IsActive
                                                              })
                                                              .ToList()
                                        })
                                        .ToList();


            foreach (var document in documents) {

                decimal totalRand = document.RandDocuments.Select(rd => rd.Valoare).Sum();

                docTotal.Add(new DocumentTotal(document, totalRand));
            }
 
            return docTotal;
        }

        public async Task<Document> GetDocumentById(int ID)
        {
            return  await _context.Documents.Where(d => d.Id == ID)
                                           .Select(doc => new Document
                                           {
                                               Id = doc.Id,
                                               Numar = doc.Numar,
                                               Data = doc.Data,
                                               Explicatie = doc.Explicatie,
                                               StatusId = doc.StatusId,
                                               DataPlata = doc.DataPlata,
                                               IsActive = doc.IsActive,
                                               RandDocuments = doc.RandDocuments
                                                              .Select(rd => new BusinessModels.RandDocument
                                                              {
                                                                  Id = rd.Id,
                                                                  DocumentId = rd.DocumentId,
                                                                  CheltuialaId = rd.CheltuialaId,
                                                                  Explicatie = rd.Explicatie,
                                                                  Valoare = rd.Valoare,
                                                                  IsActive = rd.IsActive
                                                              })
                                                              .ToList()
                                           }).FirstOrDefaultAsync();
        }

        public async Task DeleteDocument(int ID)
        {
            DecontDbContext.Models.Document doc = await _context.Documents.Where(d => d.Id == ID).FirstOrDefaultAsync();
            doc.IsActive = false;
            await _context.SaveChangesAsync();
        }

        public async Task InsertDocument(Document doc)
        {
            var docDB = new DecontDbContext.Models.Document {
                Id = doc.Id,
                Numar = doc.Numar,
                Data = doc.Data,
                Explicatie = doc.Explicatie,
                StatusId = doc.StatusId,
                DataPlata = doc.DataPlata,
                IsActive = doc.IsActive,
                RandDocuments = doc.RandDocuments
                                      .Select(rd => new DecontDbContext.Models.RandDocument
                                      {
                                          Id = rd.Id,
                                          DocumentId = rd.DocumentId,
                                          CheltuialaId = rd.CheltuialaId,
                                          Explicatie = rd.Explicatie,
                                          Valoare = rd.Valoare,
                                          IsActive = rd.IsActive
                                      })
                                      .ToList()
            };

            _context.Documents.Add(docDB);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDocument(Document doc)
        {
            var docDB = new DecontDbContext.Models.Document
            {
                Id = doc.Id,
                Numar = doc.Numar,
                Data = doc.Data,
                Explicatie = doc.Explicatie,
                StatusId = doc.StatusId,
                DataPlata = doc.DataPlata,
                IsActive = doc.IsActive,
                RandDocuments = doc.RandDocuments
                                      .Select(rd => new DecontDbContext.Models.RandDocument
                                      {
                                          Id = rd.Id,
                                          DocumentId = rd.DocumentId,
                                          CheltuialaId = rd.CheltuialaId,
                                          Explicatie = rd.Explicatie,
                                          Valoare = rd.Valoare,
                                          IsActive = rd.IsActive
                                      })
                                      .ToList()
            };

            DecontDbContext.Models.Document documentToUpdate = await _context.Documents.Where(d => d.Id == doc.Id).FirstOrDefaultAsync();

            documentToUpdate.Numar = docDB.Numar;
            documentToUpdate.Data = docDB.Data;
            documentToUpdate.Explicatie = docDB.Explicatie;
            documentToUpdate.StatusId = docDB.StatusId;
            documentToUpdate.DataPlata = docDB.DataPlata;
            documentToUpdate.RandDocuments = docDB.RandDocuments;

            await _context.SaveChangesAsync();
        }
    }
}
