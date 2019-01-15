﻿using System;
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
		private readonly IUserStore<PurchaseIdentityUser> _userStore;

		public HomeController(IPurchaseReader purchaseReader, IUserStore<PurchaseIdentityUser> userStore)
		{
			_purchaseReader = purchaseReader;
			_userStore = userStore;
		}

		public async Task<IActionResult> Index(int? pageNumber = null, int? pageSize = null)
		{
			var user = await _userStore
				.FindByNameAsync(User.Identity.Name.ToUpper(), default)
				.ConfigureAwait(false);

			var purchases = await _purchaseReader
				.GetPurchasesAsync(new PurchaseListFilter { PageNumber = pageNumber ?? 0, PageSize = pageSize ?? 20, UserId = user.Id })
				.ConfigureAwait(false);

			return View(new PurchaseListViewModel(purchases));
		}
		
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
