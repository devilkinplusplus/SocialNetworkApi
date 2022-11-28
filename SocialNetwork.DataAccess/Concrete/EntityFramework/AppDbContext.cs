using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Entities.Concrete;
using SocialNetwork.Entities.Concrete;

namespace SocialNetwork.DataAccess.Concrete
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=WIN-0AVBIPRU9F2;Database=SocialNetworkDBB;Integrated Security = True;TrustServerCertificate=True;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}