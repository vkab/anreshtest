using System;
using System.Collections.Generic;
using System.Text;

namespace PurchasesRegistry.Logic.Domain
{
	public class PurchaseList
	{
		public IEnumerable<PurchaseListItem> Items { get; set; }
		public int TotalItems { get; set; }

		public class PurchaseListItem
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public DateTime CreationDate { get; set; }
		}
	}
}
