using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
	public class OrderStatusUpdateDtoValidator : AbstractValidator<OrderStatusUpdateDto>
	{
		public OrderStatusUpdateDtoValidator()
		{
			RuleFor(x => x.OrderId).NotNull().NotEmpty().WithMessage("Id boş olamaz").Must(o => o > 0).WithMessage("Order Id 0 dan büyük olmalı");
			RuleFor(x => x.OrderStatus).IsInEnum();
		}
	}
}
