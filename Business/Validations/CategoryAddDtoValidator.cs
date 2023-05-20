using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
	public class CategoryAddDtoValidator : AbstractValidator<CategoryAddDto>
	{
		public CategoryAddDtoValidator()
		{
			RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("İsim boş olamaz").
				MaximumLength(30).MinimumLength(5).WithMessage("Kategori adı 5 ile 30 karakter arası olmalıdır.");
			RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Açıklama boş olamaz").
				MaximumLength(200).MinimumLength(5).WithMessage("Açıklama 5 ile 200 karakter arası olmalıdır.");
		}
	}
}
