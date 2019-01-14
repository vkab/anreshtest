using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PurchasesRegistry.DAL.Models
{
	public class PurchaseIdentityUser : IdentityUser
	{
		public ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
	}
}
