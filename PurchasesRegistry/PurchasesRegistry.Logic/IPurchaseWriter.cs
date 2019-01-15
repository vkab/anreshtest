using PurchasesRegistry.Logic.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PurchasesRegistry.Logic
{
	public interface IPurchaseWriter
	{
		Task SavePurchaseAsync(PurchaseInfo purchase);
	}
}
