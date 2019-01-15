using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PurchasesRegistry.DAL;
using Microsoft.EntityFrameworkCore.Design;
using PurchasesRegistry.DAL.Models;
using Microsoft.AspNetCore.Identity.UI;
using PurchasesRegistry.Logic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace PurchasesRegistry
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
			new PurchasesDbContext().Database.Migrate();
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<PurchasesDbContext>();

			services.AddDefaultIdentity<PurchaseIdentityUser>()
				.AddDefaultUI(UIFramework.Bootstrap4)
				.AddEntityFrameworkStores<PurchasesDbContext>();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddTransient<IPurchaseReader, PurchaseReader>();
			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}");
			});
		}
	}

	public class PurchaseDbContextFactory : IDesignTimeDbContextFactory<PurchasesDbContext>
	{
		public PurchasesDbContext CreateDbContext(string[] args)
		=> new PurchasesDbContext("PurchasesRegistry");
	}
}
