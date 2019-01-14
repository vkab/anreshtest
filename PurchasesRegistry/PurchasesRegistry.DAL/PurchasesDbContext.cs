using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PurchasesRegistry.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PurchasesRegistry.DAL
{
	public class PurchasesDbContext : IdentityDbContext
	{
		#region Configuration
		private bool _isConfigured = false;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!_isConfigured)
			{
				var config = new ConfigurationBuilder()
							.SetBasePath(Directory.GetCurrentDirectory())
							.AddJsonFile("appsettings.json")
							.Build();
				
				optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

				_isConfigured = true;
			}
		}
		#endregion

		public DbSet<Purchase> Purchases { get; set; }
	}
}
