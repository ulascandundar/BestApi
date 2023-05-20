using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
	public class UpdateProductStockDtoValidator : AbstractValidator<UpdateProductStockDto>
	{
		public UpdateProductStockDtoValidator()
		{
			RuleFor(x => x.Stock).InclusiveBetween(0, int.MaxValue).WithMessage("Stok 0 den küçük olamaz");
			RuleFor(x => x.ProductId).InclusiveBetween(0, int.MaxValue).WithMessage("ProductId 0 den küçük olamaz");
		}
	}
}
