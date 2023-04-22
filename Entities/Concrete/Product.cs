using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Product: BaseEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public long Stock { get; set; }
		public virtual ICollection<ProductCategory> ProductCategory { get; set; }
		public long DescriptionId { get; set; }
		public virtual Description Description { get; set; }
	}
}
