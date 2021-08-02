using JikanAPI.Models;
using JikanAPI.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace JikanAPI.Repos
{
    public class JikanDbContext : DbContext
    {
        public DbSet<Watch> Watches { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.OrderId, od.WatchId });
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
        }
        public JikanDbContext(DbContextOptions<JikanDbContext> options) : base(options)
        { }
    }
}
