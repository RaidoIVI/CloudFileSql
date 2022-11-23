using CloudFileSql.Domain;
using CloudFileSql.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileSql.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileManager _fileManager;

        public FileController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _fileManager.GetList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(Guid id)
        {
            var result = await _fileManager.GetFile(id);
            return PhysicalFile(result.PhysicalPath, contentType: "application/octet-stream", fileDownloadName: new Guid() + result.PhysicalPath,
                enableRangeProcessing: true);
        }

        [HttpPost]
        public async Task Add(FileModel fileDescription, IFormFile file)
        {
            await _fileManager.Add(fileDescription,file);
        }

        [HttpDelete]
        public async Task Delete(Guid id)
        {
            await _fileManager.Delete(id);
        }
    }
}
    

