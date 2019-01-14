using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchasesRegistry.DAL
{
	public class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
		where TContext : DbContext, new()
	{
		public TContext CreateDbContext(string[] args)
		=> new TContext();
	}
}
