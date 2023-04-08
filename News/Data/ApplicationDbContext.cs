using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.Models;

namespace News.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CulturalNews>CulturalNews { get; set;}
        public DbSet<SportNews>SportNews { get; set;}
        public DbSet<PoliticalNews> PoliticalNews { get; set;}
    }
}