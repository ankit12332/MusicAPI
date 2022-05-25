using Microsoft.EntityFrameworkCore;
using Music_Api.Models;

namespace Music_Api.Data
{
    public class ApiDbContext: DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext>options): base(options)
        {

        }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }

    }
}
