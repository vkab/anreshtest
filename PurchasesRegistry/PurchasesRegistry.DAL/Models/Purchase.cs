using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchasesRegistry.DAL.Models
{
	public class Purchase
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }

		public PurchaseIdentityUser User { get; set; }
	}
}
