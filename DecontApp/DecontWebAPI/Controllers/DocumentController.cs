using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using RepositoryLayerProject;
using DecontDbContext.Models;
using Microsoft.AspNetCore.Identity;
using BusinessModels;

namespace DecontWebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        public readonly IDocumentRepository _documentRepo;

        public DocumentController(IDocumentRepository documentRepo)
        {
            _documentRepo = documentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments(bool? IsActive)
        {
            List<DocumentTotal> response = await _documentRepo.GetDocuments(IsActive);

            return Ok(response.ToArray());
        }

        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            List<BusinessModels.Status> response = await _documentRepo.GetStatuses();
            return Ok(response);
        }

        [HttpGet]
        public async Task<BusinessModels.Document> GetDocumentById(int ID)
        {
            BusinessModels.Document doc = await _documentRepo.GetDocumentById(ID);

            return doc;
        }

        [HttpGet]
        public async Task<IActionResult> DocumentMax()
        {
            return Ok(await _documentRepo.MaxDocNo());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDocument(int ID)
        {
            await _documentRepo.DeleteDocument(ID);

            BusinessModels.Document doc = await _documentRepo.GetDocumentById(ID);

            if (doc.IsActive == false)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> SaveDocument([FromBody] BusinessModels.Document document)
        {
            if (document.IsActive == null)
                await _documentRepo.InsertDocument(document);
            else
                await _documentRepo.UpdateDocument(document);

            return Ok();
        }

    }
}
