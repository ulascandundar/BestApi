using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class ProductCategory:BaseEntity
	{
		public long ProductId { get; set; }
		public virtual Product Product { get; set; }
		public long CategoryId { get; set; }
		public virtual Category Category { get; set; }
	}
}
