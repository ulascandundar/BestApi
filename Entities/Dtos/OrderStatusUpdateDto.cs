using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
	public class OrderStatusUpdateDto
	{
		public long OrderId { get; set; }
		public OrderStatus OrderStatus { get; set; }
	}
}
