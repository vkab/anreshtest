using System;
using System.Collections.Generic;
using System.Text;

namespace PurchasesRegistry.Logic.Domain
{
	public sealed class PurchaseListFilter
	{
		public string UserId { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
