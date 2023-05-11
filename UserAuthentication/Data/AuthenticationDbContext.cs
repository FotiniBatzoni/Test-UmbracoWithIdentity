using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserAuthentication.Entities;

namespace UserAuthentication.Data
{
    public class AuthenticationDbContext : IdentityDbContext<AuthenticationUser>
    {
        //Add-Migration Initial -Context AuthenticationDbContext -Project UserAuthentication -StartupProject Test.Web
        //Update-Database

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
                : base(options)
        {
        }

        public static AuthenticationDbContext Create(DbContextOptions<AuthenticationDbContext> options)
        {
            return new AuthenticationDbContext(options);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //This will add auth. in front of every table (easier for your eye)
            builder.HasDefaultSchema("auth");
            base.OnModelCreating(builder);
        }

    }
}
