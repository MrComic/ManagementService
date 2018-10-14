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
                /*Home*/ //optionsBuilder.UseSqlServer(@"Server=.;Database=ManagementService;Persist Security Info=True;User ID=sa;Password=Password@14282038;MultipleActiveResultSets=True");

                /*Mohammad*/
                //optionsBuilder.UseSqlServer(@"Server=.;Database=ManagementService;Trusted_Connection=True;MultipleActiveResultSets=true");

                /*work*/
                optionsBuilder.UseSqlServer(@"Server=.;Database=ManagementService;Persist Security Info=True;User ID=sa;Password=Prg@14282038;MultipleActiveResultSets=True");
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
                b.HasOne(p => p.Org).WithMany(p => p.Users).HasForeignKey(p => p.OrgId).OnDelete(DeleteBehavior.Restrict);
                b.ToTable("Users");
            });

            builder.Entity<Orgs>().ToTable("Orgs").HasData(new Orgs() { Id = 1, Name = "شهر سیستم", ParentId = -1 });

            var userid = Guid.NewGuid();
            var roleid = Guid.NewGuid();
            builder.Entity<User>().HasData(new User
            {
                Id = userid,
                UserName = "administrator",
                NormalizedUserName = "ADMINISTRATOR",
                Email = "info@Admin.com",
                Firstname = "مدیر",
                LastName = "مدیری",
                PasswordHash = "AQAAAAEAACcQAAAAEKd8p8c6+ACBwvJR8YxDzm8sP32rhAzJuB5UIiB5qQC2v6FD4hf5FXBO1ee8FXAcRg==",
                SecurityStamp = "DHHZ5M7Y634ZRUL4UH6EG44NLXAB4NJK",
                OrgId = 1,
                MobileNumber = "00000000000",
                NationalCode = "0000000000",
                EmailConfirmed = true,
                ImageLink = "Admin.png"
            });

            builder.Entity<UsersInRole>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.ToTable("UsersInRoles");
                b.HasOne(p => p.User).WithMany(p => p.UserRoles).HasForeignKey(p => p.UserId);
                b.HasOne(p => p.Role).WithMany(p => p.UsedRoles).HasForeignKey(p => p.RoleId);
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
            builder.Entity<Role>().HasData(new Role() { Id = roleid, Name = "Administrators" });

            builder.Entity<UsersInRole>().HasData(new UsersInRole() { RoleId = roleid, UserId = userid });
            
            builder.Entity<Menu>().HasData(new Menu()
            {
                Id = 1,
                IsVisible = true,
                ParentId = -1,
                MenuIconName = "fa fa-home",
                Name = "خانه",
                Route = ""
            }, new Menu()
            {
                Id = 2,
                IsVisible = true,
                MenuIconName = "fa fa-users",
                Route = "/",
                ParentId = -1,
                Name = "کاربران"
            }, new Menu()
            {
                Id = 3,
                IsVisible = true,
                MenuIconName = "fa fa-clipboard",
                Route = "/role/createrole",
                ParentId = -1,
                Name = "مدیریت نقش ها"
            }, new Menu()
            {
                Id = 4,
                IsVisible = true,
                MenuIconName = "fa fa-user",
                Route = "/users/list",
                ParentId = 2,
                Name = "لیست کاربران"
            });

            builder.Entity<MenuAccess>().ToTable("MenuAccess").HasKey(p => new { p.MenuId, p.RoleId });
            builder.Entity<MenuAccess>().HasData(
                new MenuAccess() { MenuId = 1, RoleId = roleid },
                new MenuAccess() { MenuId = 2, RoleId = roleid },
                new MenuAccess() { MenuId = 3, RoleId = roleid },
                new MenuAccess() { MenuId = 4, RoleId = roleid });

            builder.Ignore<IdentityUserClaim<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserLogin<Guid>>();

            builder.Entity<UserClaim>(b =>
            {
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
                b.ToTable("UserTokens");
            });
            builder.Entity<RoleClaim>(b =>
            {
                b.HasKey(rc => rc.Id);
                b.ToTable("RoleClaims");
            });
        }

        public DbSet<Orgs> Orgs { get; set; }
        public DbSet<Menu> Menus { get; set; }
    }
}
