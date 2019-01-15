using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PurchasesRegistry.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace PurchasesRegistry.DAL
{
	public class PurchasesDbContext : IdentityDbContext<PurchaseIdentityUser, IdentityRole, string>
	{
		#region Constructors
		//Support all initialization scenarios

		public PurchasesDbContext(DbContextOptions options) : base(options)
		{ }

		public PurchasesDbContext(bool isMigrating)
			=> isMigrating = true;
		
		public PurchasesDbContext(string migrationAssemblyName)
		{
			_migrationAssemblyName = migrationAssemblyName;
		}

		public PurchasesDbContext()
		{ }
		#endregion

		#region Configuration
		private bool _isConfigured = false;

		private readonly string _migrationAssemblyName;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!_isConfigured)
			{
				var config = new ConfigurationBuilder()
							.SetBasePath(Directory.GetCurrentDirectory())
							.AddJsonFile("appsettings.json")
							.Build();

				optionsBuilder.UseSqlServer(
					config.GetConnectionString("DefaultConnection"), 
					op=> op.MigrationsAssembly(_migrationAssemblyName ?? Assembly.GetExecutingAssembly().GetName().Name));

				_isConfigured = true;
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Purchase>(e =>
				{
					e.ToTable("Purchases");
					e.HasKey(i => i.Id);
					e.HasIndex(i => i.UserId)
						.IsUnique(false);

					e.HasOne(i => i.User)
						.WithMany(i => i.Purchases)
						.HasForeignKey(i => i.UserId)
						.OnDelete(DeleteBehavior.Restrict)
						.IsRequired();
					
					e.Property(i => i.Name)
						.HasMaxLength(256);

					e.Property(i => i.Description)
						.HasMaxLength(2048)
						.IsRequired(false);
				});
		}
		#endregion

		public DbSet<Purchase> Purchases { get; set; }
	}
}
