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
    public class SqlContext : IdentityDbContext<User, Roles, long>
    {
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
            builder.Entity<User>()
                .ToTable("Users", "dbo").Property(p => p.Id).HasColumnName("User_Id");
            builder.Entity<Roles>()
                .ToTable("Roles", "dbo").Property(p => p.Id).HasColumnName("Role_Id");
            builder.Entity<IdentityUserRole<long>>(entity =>
            {              
                entity.ToTable("UserRoles", "dbo");
                entity.Property(e => e.UserId).HasColumnName("User_Id");
                entity.Property(e => e.RoleId).HasColumnName("Role_Id");
                

            });
            builder.Entity<IdentityUserLogin<long>>(entity =>
            {
                entity.ToTable("UserLogins", "dbo");
                entity.Property(e => e.UserId).HasColumnName("User_Id");

            });
            builder.Entity<IdentityUserClaim<long>>(entity =>
            {
                entity.ToTable("UserClaims", "dbo");
                entity.Property(e => e.UserId).HasColumnName("User_Id");
                entity.Property(e => e.Id).HasColumnName("UserClaim_Id");

            });

            builder.Entity<IdentityRoleClaim<long>>(entity =>
            {
                entity.ToTable("RoleClaims", "dbo");
                entity.Property(e => e.Id).HasColumnName("RoleClaim_Id");
                entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            });

            builder.Entity<IdentityUserToken<long>>(entity =>
            {
                entity.ToTable("UserTokens", "dbo");
                entity.Property(e => e.UserId).HasColumnName("User_Id");

            });
            //builder.Entity<IdentityRole>().ToTable("MyRoles");
        }
    }
}
