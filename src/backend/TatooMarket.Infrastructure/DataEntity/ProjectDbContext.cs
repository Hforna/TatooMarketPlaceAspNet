﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Communication;
using TatooMarket.Domain.Entities.Identity;
using TatooMarket.Domain.Entities.Tattoo;

namespace TatooMarket.Infrastructure.DataEntity
{
    public class ProjectDbContext : IdentityDbContext<UserEntity, RoleEntity, long>
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        public DbSet<TattooEntity> Tattos { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEntity>()
                .HasOne(u => u.Studio)
                .WithOne(d => d.Owner)
                .HasForeignKey<UserEntity>(d => d.StudioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RoleEntity>().HasData(new RoleEntity()
            {
                CreatedOn = DateTime.UtcNow,
                Id = 1,
                Name = "seller",
                NormalizedName = "SELLER",
                ConcurrencyStamp = $"{Guid.NewGuid}"
            }, 
            new RoleEntity()
            {
                CreatedOn = DateTime.UtcNow,
                Id = 2,
                Name = "customer",
                NormalizedName = "CUSTOMER",
                ConcurrencyStamp = $"{Guid.NewGuid}"
            });
        }
    }
}
