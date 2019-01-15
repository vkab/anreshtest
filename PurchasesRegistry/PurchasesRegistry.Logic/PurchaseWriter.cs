using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PurchasesRegistry.DAL;
using PurchasesRegistry.Logic.Domain;
using PurchasesRegistry.Logic.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using PurchasesRegistry.DAL.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PurchasesRegistry.Logic
{
	public class PurchaseWriter : IPurchaseWriter
	{
		public async Task SavePurchaseAsync(PurchaseInfo purchase)
		{
			using (var context = ServiceProviderFactory.Provider.GetService<PurchasesDbContext>())
			{
				if(purchase.Id == 0)
				{
					var dalItem = new Purchase
					{
						Amount = purchase.Amount,
						CreationDate = DateTime.UtcNow,
						Description = purchase.Description,
						Name = purchase.Name,
						UserId = purchase.OwnerUserId
					};

					context.Purchases.Add(dalItem);
				}
				else
				{
					var existing = await context
						.Purchases
						.SingleAsync(i => i.Id == purchase.Id && i.UserId == purchase.OwnerUserId)
						.ConfigureAwait(false);

					//not updatevalues way bcs we dont use mapping
					existing.Name = purchase.Name;
					existing.Description = purchase.Description;
					existing.Amount = purchase.Amount;
				}

				await context.SaveChangesAsync()
						.ConfigureAwait(false);
			}
		}
	}
}
