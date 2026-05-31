using Microsoft.EntityFrameworkCore;
using MinhaCloud.Models;
using System.Security.Cryptography.X509Certificates;

namespace MinhaCloud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FileItem> Files { get; set; }
    }
}
