using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
	public class GetOrderDto
	{
		public long Id { get; set; }
		public string Description { get; set; }
		public decimal TotalPrice { get; set; }
		public DateTime Created { get; set; }
	}
}
