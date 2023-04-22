using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
	public class OrderDto
	{
		public List<OrderEntryDto> OrderEntries { get; set; }
	}
}
