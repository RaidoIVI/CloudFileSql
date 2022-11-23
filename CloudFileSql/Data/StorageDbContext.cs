using CloudFileSql.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudFileSql.Data
{
    public class StorageDbContext : DbContext
    {
        public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options)
        {
        }

        public DbSet<FileModel> FileDescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileModel>().ToTable("Description");
        }
    }
}
