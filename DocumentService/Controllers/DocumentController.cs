using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocumentService.DTOs;
using DocumentService.Core.Entities;
using DocumentService.Infrastructure.Data;

namespace DocumentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateDocumentDto dto)
        {
            var document = new Document
            {
                FileName = dto.FileName,
                Content = dto.Content,
                Summary = dto.Summary
            };

            _context.Documents.Add(document);

            await _context.SaveChangesAsync();

            return Ok(document);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var documents =
                await _context.Documents
                .Where(x => x.IsActive)
                .ToListAsync();

            return Ok(documents);
        }
    }
}