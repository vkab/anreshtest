using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PurchasesRegistry.Models
{
	public class PurchaseListViewModel
	{
		public PurchaseListViewModel(
			IEnumerable<Logic.Domain.PurchaseList.PurchaseListItem> source, 
			int pageNum, int pageSize, int totalInDb)
		{
			Purchases = new PagedList.Core.StaticPagedList<PurchaseItem>(source
				.Select(i => new PurchaseItem
				{
					CreationDate = i.CreationDate,
					Id = i.Id,
					Name = i.Name
				}), pageNum, pageSize, totalInDb);
		}

		public PagedList.Core.StaticPagedList<PurchaseItem> Purchases { get; set; }

		public class PurchaseItem
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public DateTime CreationDate { get; set; }
		}
	}
}
