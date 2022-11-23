using CloudFileSql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace CloudFileSql.Data
{
    public interface IFileRepo
    {
        Task<ICollection<FileModel>> GetList();
        Task<IFileInfo> GetFile(Guid id);
        Task Add(FileModel description, IFormFile file);
        Task Delete(Guid id);
    }

    public class FileRepo : IFileRepo
    {
        private readonly StorageDbContext _dbContext;
        private readonly string _storage;

        public FileRepo(StorageDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _storage = Path.Combine(webHostEnvironment.ContentRootPath, "Storage");

        }

        public async Task<ICollection<FileModel>> GetList()
        {
            var result = await _dbContext.FileDescriptions.ToArrayAsync();
            return result;
        }

        public async Task<IFileInfo> GetFile(Guid id)
        {
            IFileInfo result = null;
            var provider = new PhysicalFileProvider(_storage);
            var file = await _dbContext.FileDescriptions.FindAsync(id);
            string path = Path.Combine(file.Id + "." + file.Extension);
            if (provider.GetFileInfo(path).Exists) result = await Task.Run(() => provider.GetFileInfo(path));
            return result;
        }

        public async Task Add(FileModel description, IFormFile file)
        {
            await _dbContext.AddAsync(description);
            await _dbContext.SaveChangesAsync();
            await using var fileStream = new FileStream(_storage + $"{description.Id}.{description.Extension}", FileMode.Create);
            await file.CopyToAsync(fileStream);
        }

        public async Task Delete(Guid id)
        {
            var description = await _dbContext.FileDescriptions.FindAsync(id);
            var file = new FileInfo(_storage + $"{description.Id}.{description.Extension}");
            if (file.Exists) await Task.Run(() => file.Delete());
            await Task.Run(() => _dbContext.Remove(id));
            _dbContext.SaveChangesAsync();
        }
    }
}
