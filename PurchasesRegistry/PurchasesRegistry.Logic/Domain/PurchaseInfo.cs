using System;
using System.Collections.Generic;
using System.Text;

namespace PurchasesRegistry.Logic.Domain
{
	public class PurchaseInfo
	{
		public int Id { get; set; }
		public string OwnerUserId { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
	}
}
