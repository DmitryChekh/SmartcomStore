﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartcomStore.Core;
using SmartcomStore.Data.Models;
using SmartcomStore.Data.Models.Identity;
using SmartcomStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartcomStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(e =>
            {
                e.ToTable("User");
            });


            builder.Entity<Role>(e =>
            {
                e.ToTable("Roles");

                e.HasData(new Role
                {
                    Id = new Guid("6e0b025c-2dc3-4367-a27c-8daf44c854f1"),
                    ConcurrencyStamp = "6e0b025c-2dc3-4367-a27c-8daf44c854f1",
                    Name = Constants.Roles.Customer,
                    NormalizedName = Constants.Roles.Customer.ToUpper()
                });
                e.HasData(new Role
                {
                    Id = new Guid("9721e9da-6622-4cc9-8e47-ce78637062b9"),
                    ConcurrencyStamp = "9721e9da-6622-4cc9-8e47-ce78637062b9",
                    Name = Constants.Roles.Manager,
                    NormalizedName = Constants.Roles.Manager.ToUpper()
                });
            });


            builder.Entity<UserRole>(userRole =>
            {
                userRole.ToTable("UserRoles");
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<Order>(order =>
            {
                order.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.CustomerId)
                    .IsRequired();

                order.Property(c => c.Status).HasConversion<string>();
            });


            builder.Entity<OrderItem>(orderItem =>
            {
                orderItem.HasOne(o => o.Order)
                    .WithMany(x => x.OrderItems)
                    .HasForeignKey(k => k.OrderId)
                    .IsRequired();

                orderItem.HasOne(p => p.Product)
                    .WithMany(x => x.OrderItems)
                    .HasForeignKey(k => k.ItemId)
                    .IsRequired();

            });


            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserToken>().ToTable("UserTokens");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");

            base.OnModelCreating(builder);
        }

        //entities
#pragma warning disable CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
#pragma warning restore CS0114 // Член скрывает унаследованный член: отсутствует ключевое слово переопределения

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }


    }
}
