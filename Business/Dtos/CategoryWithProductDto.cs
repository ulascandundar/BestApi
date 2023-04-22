using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
	public class CategoryWithProductsDto : CategoryDto
	{
		public List<ProductDto> Products { get; set; }
	}
}
