using CloudFileSql.Data;
using CloudFileSql.Models;
using Microsoft.Extensions.FileProviders;

namespace CloudFileSql.Domain
{
    public interface IFileManager
    {
        Task<ICollection<FileModel>> GetList();
        Task<IFileInfo> GetFile(Guid id);
        Task Add(FileModel fileModel, IFormFile file);
        Task Delete(Guid id);
    }

    public class FileManager : IFileManager
    {
        private readonly IFileRepo _fileRepo;

        public FileManager(IFileRepo fileRepo)
        {
            _fileRepo = fileRepo;
        }

        public async Task<ICollection<FileModel>> GetList()
        {
            var result = await _fileRepo.GetList();
            return result;
        }

        public async Task<IFileInfo> GetFile(Guid id)
        {
            var result = await _fileRepo.GetFile(id);
            return result;
        }

        public async Task Add(FileModel description, IFormFile file)
        {
            await _fileRepo.Add(description, file);
        }

        public async Task Delete(Guid id)
        {
            await _fileRepo.Delete(id);
        }
    }
}
