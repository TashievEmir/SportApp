using Microsoft.EntityFrameworkCore;
using SportApp.Entities;

namespace SportApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(x => x.Role)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .Property(e => e.CreatedAt)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<User>()
                .Property(e => e.UpdatedAt)
                .HasConversion(v => v.ToUniversalTime(), v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<User>()
                .HasIndex(x => x.UserName).IsUnique();
        }
    }
}
