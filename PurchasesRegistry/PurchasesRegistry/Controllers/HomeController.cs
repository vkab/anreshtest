using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PurchasesRegistry.DAL.Models;
using PurchasesRegistry.Logic;
using PurchasesRegistry.Logic.Domain;
using PurchasesRegistry.Models;

namespace PurchasesRegistry.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly IPurchaseReader _purchaseReader;
		private readonly IPurchaseWriter _purchaseWriter;
		private readonly IUserStore<PurchaseIdentityUser> _userStore;

		public HomeController(
			IPurchaseReader purchaseReader, 
			IPurchaseWriter purchaseWriter,
			IUserStore<PurchaseIdentityUser> userStore)
		{
			_purchaseReader = purchaseReader;
			_purchaseWriter = purchaseWriter;
			_userStore = userStore;
		}

		public async Task<IActionResult> Index([FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 20)
		{
			var user = await _userStore
				.FindByNameAsync(User.Identity.Name.ToUpper(), default)
				.ConfigureAwait(false);
			
			var purchases = await _purchaseReader
				.GetPurchasesAsync(new PurchaseListFilter {
					PageNumber = pageNumber - 1,
					PageSize = pageSize,
					UserId = user.Id })
				.ConfigureAwait(false);

			return View(new PurchaseListViewModel(purchases.Items, pageNumber, pageSize, purchases.TotalItems));
		}

		[Route("{Id}")]
		public async Task<IActionResult> Details(int id)
		{
			if(id > 0)
			{
				var user = await _userStore
				   .FindByNameAsync(User.Identity.Name.ToUpper(), default)
				   .ConfigureAwait(false);

				var item = await _purchaseReader
					.GetPurchaseAsync(id, user.Id)
					.ConfigureAwait(false);

				return View(new PurchaseViewModel(item));
			}
			else
			{
				return View(new PurchaseViewModel());
			}
		}



		[HttpPost]
		public async Task<IActionResult> Save(PurchaseViewModel data)
		{
			if (ModelState.IsValid)
			{
				var user = await _userStore
					  .FindByNameAsync(User.Identity.Name.ToUpper(), default)
					  .ConfigureAwait(false);

				var domainModel = data.ToPurchaseInfo(user.Id);

				await _purchaseWriter.SavePurchaseAsync(domainModel)
					.ConfigureAwait(false);

				return RedirectToAction(nameof(HomeController.Index));
			}
			else
			{
				return View(nameof(HomeController.Details), data);
			}
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
