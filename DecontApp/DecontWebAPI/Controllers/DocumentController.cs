using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using RepositoryLayerProject;
using DecontDbContext.Models;
using DocumentTotalProj;
using Microsoft.OpenApi.Models;

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
        public async Task<ActionResult<RandDocument[]>> GetRand(int docId)
        {
            RandDocument[] response = await _documentRepo.GetRand(docId);

            if(response == null || response.Length == 0) 
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDocument(int ID)
        {
            await _documentRepo.DeleteDocument(ID);

            Document doc = await _documentRepo.GetDocumentById(ID);

            if (doc.IsActive == false)
                return Ok();

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> SaveDocument([FromBody] Document document)
        {
            if (document.IsActive == null)
                await _documentRepo.InsertDocument(document);
            else
                await _documentRepo.UpdateDocument(document);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SaveRand([FromBody] RandDocument rand)
        {
            if(rand.IsActive == null)
                await _documentRepo.InsertRand(rand);
            else
                await _documentRepo.UpdateRand(rand);

            return Ok();
        }
    }
}
