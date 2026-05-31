using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaCloud.Data;
using MinhaCloud.Models;
using System.Security.Claims;

namespace MinhaCloud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly string _storageRoot;

        public FilesController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _storageRoot = config["Storage:RootPath"] ?? "C:\\MinhaCloudStorage";
        }

        [HttpGet]
        public async Task<IActionResult> ListFiles()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var files = await _db.Files
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.CreatedAt)
                .Select(f => new 
                {
                    f.Id,
                    f.OriginalName,
                    f.ContentType,
                    f.SizeBytes,
                    f.CreatedAt,
                    HasSharedLink = f.ShareToken != null
                }).ToListAsync();

            return Ok(files);

        }

        [HttpPost("upload")]
        [RequestSizeLimit(10L * 1024 *  1024 * 1024)]
        [RequestFormLimits(MultipartBodyLengthLimit = 10L * 1024 * 1024 * 1024)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado");

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            //Cria Pasta do Usuario no HD se nao existir
            var userFolder = Path.Combine(_storageRoot, userId.ToString());
            Directory.CreateDirectory(userFolder);

            var uniqueName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(userFolder, uniqueName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileItem = new FileItem
            {
                UserId = userId,
                FileName = uniqueName,
                OriginalName = file.FileName,
                ContentType = file.ContentType,
                SizeBytes = file.Length,
                StoragePath = fullPath
            };

            _db.Files.Add(fileItem);
            await _db.SaveChangesAsync();

            return Ok(new {fileItem.Id, fileItem.OriginalName, fileItem.SizeBytes});
            
        }

        [HttpGet("{id}/download")]
        public async Task<IActionResult> Download(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var file = await _db.Files.FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

            if (file == null) return NotFound("Arquivo nao encontrado");
            if (!System.IO.File.Exists(file.StoragePath)) return NotFound("Arquivo nao encontrado");

            var stream = System.IO.File.OpenRead(file.StoragePath);
            return File(stream, file.ContentType, file.OriginalName);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var file = await _db.Files.FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

            if (file == null) return NotFound();

            if (System.IO.File.Exists(file.StoragePath))
                System.IO.File.Delete(file.StoragePath);

            _db.Files.Remove(file);
            await _db.SaveChangesAsync();

            return Ok("Arquivo Deletado");
        }

        [HttpPost("{id}/share")]
        public async Task<IActionResult> Share(int id, [FromBody] ShareRequest req)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var file = await _db.Files.FirstOrDefaultAsync(f => f.Id == id && f.UserId == userId);

            if (file == null) return NotFound();
            
            file.ShareToken = Guid.NewGuid().ToString("N");
            file.ShareExpiresAt = DateTime.UtcNow.AddHours(req.HoursValid);
            await _db.SaveChangesAsync();

            var shareUrl = $"{Request.Scheme}://{Request.Host}/api/files/shared/{file.ShareToken}";
            return Ok(new { url = shareUrl, expiresAt = file.ShareExpiresAt});

        }


        [HttpGet("shared/{token}")]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadShared(string token)
        {
            var file = await _db.Files.FirstOrDefaultAsync(f => f.ShareToken == token);

            if (file == null) return NotFound("Link Invalido");
            if (file.ShareExpiresAt < DateTime.UtcNow) return BadRequest("Link Expirado");
            if (!System.IO.File.Exists(file.StoragePath)) return NotFound("Arquivo nao encontrado");

            var stream = System.IO.File.OpenRead(file.StoragePath);
            return File(stream, file.ContentType, file.OriginalName);
    
        }
    }

    public record ShareRequest(int HoursValid);
}
