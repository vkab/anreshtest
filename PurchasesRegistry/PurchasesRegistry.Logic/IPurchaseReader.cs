using PurchasesRegistry.Logic.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PurchasesRegistry.Logic
{
	public interface IPurchaseReader
	{
		Task<PurchaseInfo> GetPurchaseAsync(int id);
		Task<PurchaseList> GetPurchasesAsync(PurchaseListFilter filter);
	}
}
