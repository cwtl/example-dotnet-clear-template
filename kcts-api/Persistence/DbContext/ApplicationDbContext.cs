using Application.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "e24dde1f-a730-4aa7-a14e-fbbc0d5a64a9", Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });
            var hasher = new PasswordHasher<IdentityUser>();
            var adminUser = new ApplicationUser
            {
                Id = "f47f0a36-dbaa-4c27-9877-309af35f58b5",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                //PasswordHash = hasher.HashPassword(null, "kcts!pass4DB")
            };
            hasher.HashPassword(adminUser, "kcts!pass4DB");
            builder.Entity<ApplicationUser>().HasData(adminUser);

            //builder.Entity<ApplicationUser>().HasData(
            //    new ApplicationUser
            //    {
            //        Id = "f47f0a36-dbaa-4c27-9877-309af35f58b5",
            //        UserName = "admin",
            //        NormalizedUserName = "ADMIN",
            //        PasswordHash = hasher.HashPassword(null, "kcts!pass4DB")
            //    }
            //);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "e24dde1f-a730-4aa7-a14e-fbbc0d5a64a9",
                    UserId = "f47f0a36-dbaa-4c27-9877-309af35f58b5"
                }
            );
        }
    }

}
