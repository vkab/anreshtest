﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PurchasesRegistry.DAL;
using PurchasesRegistry.Logic.Domain;
using PurchasesRegistry.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchasesRegistry.Logic
{
	public sealed class PurchaseReader : IPurchaseReader
	{
		public async Task<PurchaseInfo> GetPurchaseAsync(int id)
		=> await ServiceProviderFactory.Provider
				.GetService<PurchasesDbContext>()
				.Purchases
				.Where(i => i.Id == id)
				.Select(i => new PurchaseInfo
				{
					CreationDate = i.CreationDate,
					Id = i.Id,
					Name = i.Name,
					Amount = i.Amount,
					Description = i.Description,
					OwnerUserId = i.UserId
				})
				.SingleAsync()
				.ConfigureAwait(false);

		public async Task<PurchaseList> GetPurchasesAsync(PurchaseListFilter filter)
		{
			var resultQuery = ServiceProviderFactory.Provider
				  .GetService<PurchasesDbContext>()
				  .Purchases
				  .Where(i => i.UserId == filter.UserId);

			return new PurchaseList
			{
				Items = await resultQuery
					  .Select(i => new PurchaseList.PurchaseListItem
					  {
						  CreationDate = i.CreationDate,
						  Id = i.Id,
						  Name = i.Name
					  })
					  .Skip(filter.PageNumber * filter.PageSize)
					  .Take(filter.PageSize)
					  .ToListAsync()
					  .ConfigureAwait(false),
				TotalItems = resultQuery.Count()
			};
		}
	}
}
