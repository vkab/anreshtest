using Microsoft.Extensions.DependencyInjection;
using PurchasesRegistry.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchasesRegistry.Logic.Infrastructure
{
	internal static class ServiceProviderFactory
	{
		private static readonly IServiceProvider _provider;

		public static IServiceProvider Provider => _provider;

		static ServiceProviderFactory()
		{
			var collection = new ServiceCollection();

			collection.AddDbContext<PurchasesDbContext>(ServiceLifetime.Transient);

			collection.AddTransient<PurchasesDbContext>();
			
			_provider = collection.BuildServiceProvider();
		}
	}
}
