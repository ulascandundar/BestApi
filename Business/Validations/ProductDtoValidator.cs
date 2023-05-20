using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
	public class ProductDtoValidator : AbstractValidator<ProductAddDto>
	{
		public ProductDtoValidator()
		{
			RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
			RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
			RuleFor(x => x.Price).Must(o => o >= 0).WithMessage("Fiyat 0 dan düşük olamaz");
			RuleFor(x => x.Stock).Must(o => o >= 0).WithMessage("Stok 0 dan düşük olamaz");
		}
	}
}
