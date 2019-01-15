using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchasesRegistry.Models
{
	public class PurchaseListViewModel
	{
		public PurchaseListViewModel(IEnumerable<Logic.Domain.PurchaseListItem> source)
		{
			Purchases = source
				.Select(i => new PurchaseItem
				{
					CreationDate = i.CreationDate,
					Id = i.Id,
					Name = i.Name
				}).ToList();
		}

		public IEnumerable<PurchaseItem> Purchases { get; set; }

		public class PurchaseItem
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public DateTime CreationDate { get; set; }
		}
	}
}
