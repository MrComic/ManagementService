using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ManagementService.Model.DbSets;
using ManagementService.Model.DbSets.User;
using ManagementService.Model.DbSets.Roles;
using ManagementService.Model.DbSets.Menu;
using Microsoft.AspNetCore.Identity;
using ManagementService.Model.DbSets.Orgs;

namespace ManagementService.Data
{
    public interface IDatabaseContext 
    {

    }

    public class DatabaseContext : IdentityDbContext<User, Role, Guid, UserClaim
        , UsersInRole, UserLogin, RoleClaim, UserToken
        >, IDatabaseContext
    {

        //public DatabaseContext(DbContextOptions<DatabaseContext> options)
        //    : base(options)
        //{
            
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*Home*/ optionsBuilder.UseSqlServer(@"Server=.;Database=ManagementService;Persist Security Info=True;User ID=sa;Password=Password@14282038;MultipleActiveResultSets=True");

                /*Mohammad*/
               // optionsBuilder.UseSqlServer(@"Server=.;Database=ManagementService;Trusted_Connection=True;MultipleActiveResultSets=true");

                /*work*/  //optionsBuilder.UseSqlServer(@"Server=.;Database=ManagementService;Persist Security Info=True;User ID=sa;Password=Prg@14282038;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            
            builder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                
                b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");
                
                
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
                
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);

                b.ToTable("Users");
            });
            builder.Ignore<IdentityUserClaim<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserLogin<Guid>>();

            builder.Entity<UserClaim>(b=> {
                b.HasKey(p => p.Id);
                b.ToTable("UserClaims");
            });

            
     

            builder.Entity<UserLogin>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
                b.Property(l => l.LoginProvider).HasMaxLength(128);
                b.Property(l => l.ProviderKey).HasMaxLength(128);
               
                b.ToTable("UserLogins");
            });

            builder.Entity<UserToken>(b =>
            {
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
                
               // b.Property(t => t.LoginProvider).HasMaxLength(maxKeyLength);
               // b.Property(t => t.Name).HasMaxLength(maxKeyLength);
               
                b.ToTable("UserTokens");
            });


            builder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);
                b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
                b.ToTable("Roles");
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);
                
            });

            builder.Entity<RoleClaim>(b =>
            {
                b.HasKey(rc => rc.Id);
                b.ToTable("RoleClaims");
            });

            builder.Entity<UsersInRole>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
                
                b.ToTable("UsersInRoles");
            });
            builder.Entity<Orgs>().ToTable("Orgs");
            builder.Entity<MenuAccess>().ToTable("MenuAccess").HasKey(p => new { p.MenuId, p.RoleId });
          //  builder.Entity<UsersInRoles>().ToTable("UsersInRoles");
            //builder.Entity<Menu>().HasMany(o => o.MenuAccesses);
            //builder.Entity<Role>().HasMany(o => o.MenuAccesses);
        }

        public DbSet<Orgs> Orgs { get; set; }
        public DbSet<Menu> Menus { get; set; }
    }
}
