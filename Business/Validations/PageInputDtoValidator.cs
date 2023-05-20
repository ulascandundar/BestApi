using Core.Dtos;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
	public class PageInputDtoValidator : AbstractValidator<PageInputDto>
	{
		public PageInputDtoValidator()
		{
			RuleFor(x => x.PageIndex).Must(o => o > 0).WithMessage("Sayfa 1 den küçük olamaz");
			RuleFor(x => x.PageSize).Must(o => o > 0).WithMessage("Sayfa boyutu 1 den küçük olamaz");
		}
	}
}
