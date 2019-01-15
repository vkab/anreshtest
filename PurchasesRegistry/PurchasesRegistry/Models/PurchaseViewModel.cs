using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PurchasesRegistry.Models
{
	public class PurchaseViewModel
	{
		public PurchaseViewModel() { }

		public PurchaseViewModel(Logic.Domain.PurchaseInfo source)
		{
			Id = source.Id;
			Name = source.Name;
			CreationDate = source.CreationDate;
			Description = source.Description;
			Amount = source.Amount;
		}

		public Logic.Domain.PurchaseInfo ToPurchaseInfo(string userId)
			=> new Logic.Domain.PurchaseInfo
			{
				Amount = Amount,
				CreationDate = CreationDate,
				Description = Description,
				Id = Id,
				Name = Name,
				OwnerUserId = userId
			};

		[Required]
		public int Id { get; set; }

		[Display(Name="Наименование")]
		[StringLength(256, MinimumLength = 1, ErrorMessage ="Текстовое поле является обязательным и должно быть не длинее 256 символов")]
		[Required(ErrorMessage = "Данное поле является обязательным")]
		public string Name { get; set; }

		public DateTime CreationDate { get; set; }

		[Display(Name = "Подробное описание")]
		[StringLength(2048, ErrorMessage = "Текстовое поле должно быть не длинее 2048 символов")]
		public string Description { get; set; }

		[Display(Name = "Сумма закупки")]
		[Required(ErrorMessage = "Данное поле является обязательным")]
		public decimal Amount { get; set; }
	}
}
