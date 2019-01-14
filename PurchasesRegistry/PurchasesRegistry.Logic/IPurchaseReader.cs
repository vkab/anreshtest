using PurchasesRegistry.Logic.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PurchasesRegistry.Logic
{
	public interface IPurchaseReader
	{
		Task<PurchaseInfo> GetPurchase(int id);
		Task<IEnumerable<PurchaseListItem>> GetPurchases(PurchaseListFilter filter);
	}
}
