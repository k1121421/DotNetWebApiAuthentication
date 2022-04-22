using Microsoft.EntityFrameworkCore;

namespace WebApiUsingJWT.Models
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext()
        {
        }

        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        //public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder is null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("AuthSampleDb");
            }
        }
    }
}
