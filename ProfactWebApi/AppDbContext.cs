using Microsoft.EntityFrameworkCore;
using ProfactWebApi.Models;

namespace ProfactWebApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<Boundaries> Boundaries { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<PostData> PostDatas { get; set; }
        public string DbPath { get; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
