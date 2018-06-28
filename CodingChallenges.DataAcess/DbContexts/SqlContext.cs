using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CodingChallenges.Domains.Users;
using CodingChallenges.Domains.Employees;
using Microsoft.AspNetCore.Identity;

namespace CodingChallenges.DataAcess.DbContexts
{
    public class SqlContext : IdentityDbContext<User, Roles, string>
    {
        public string CurrentUserId { get; set; }
        public SqlContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //builder.Entity<IdentityUser<long>>()
            //    .ToTable("Users", "dbo").Property(p => p.Id).HasColumnName("User_Id");
            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "dbo");
                entity.HasMany(u => u.Claims).WithOne().HasForeignKey(c => c.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(u => u.Roles).WithOne().HasForeignKey(r => r.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Roles>(entity => {
                entity.ToTable("Roles", "dbo");
                entity.HasMany(r => r.Claims).WithOne().HasForeignKey(c => c.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade); 
                entity.HasMany(r => r.Users).WithOne().HasForeignKey(r => r.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {              
                entity.ToTable("UserRoles", "dbo");                 
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "dbo"); 
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "dbo");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", "dbo");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "dbo");
            });
            //builder.Entity<IdentityRole>().ToTable("MyRoles");
        }
    }
}
