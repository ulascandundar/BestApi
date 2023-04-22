using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class OrderEntry:BaseEntity
	{
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public long OrderId { get; set; }
		public virtual Order Order { get; set; }
		public long ProductId { get; set; }
		public virtual Product Product { get; set; }
	}
}
