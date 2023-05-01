using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
	public class ProductDto:BaseDto
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public long Stock { get; set; }
		public DescriptionDto Desciption { get; set; }
	}
}
