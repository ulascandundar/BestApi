using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
	public class ProductAddDto
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public long Stock { get; set; }
		public DescriptionAddDto Description { get; set; }
		public List<long> CategoryIds { get; set; }
	}
}
