using Core.Entities;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
	public class BestApiDbContext:IdentityDbContext<AppUser, AppRole, long>
	{
		public BestApiDbContext(DbContextOptions<BestApiDbContext> options) : base(options)
		{
		}
		public DbSet<Category> Category { get; set; }
		public DbSet<Description> Description { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<ProductCategory> ProductCategory { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderEntry> OrderEntry { get; set; }
		public DbSet<Wallet> Wallet { get; set; }
		public DbSet<WalletTransaction> WalletTransaction { get; set; }
		public override int SaveChanges()
		{
			var addedentries = ChangeTracker
		.Entries()
		.Where(e => e.Entity is BaseEntity && (
				e.State == EntityState.Added));
			foreach (var entry in addedentries)
			{
				((BaseEntity)entry.Entity).CreatedDate = DateTime.UtcNow;
			}
			var updatedentries = ChangeTracker
		.Entries()
		.Where(e => e.Entity is BaseEntity && (
				e.State == EntityState.Modified));
			foreach (var entry in updatedentries)
			{
				((BaseEntity)entry.Entity).UpdatedDate = DateTime.UtcNow;
			}
			return base.SaveChanges();
		}
	}
}
