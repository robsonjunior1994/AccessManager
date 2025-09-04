using AccessManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessManager.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId) // ⬅️ Aqui você usa a FK
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
