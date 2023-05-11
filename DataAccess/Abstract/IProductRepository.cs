using Core.Data.Abstract;
using Core.Dtos;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{	
	public interface IProductRepository : IEntityRepository<Product>
	{
		public PagedResult<ProductWithCategoryDto> GetProductWithPaging(PageInputDto pageInputDto);
		public ProductDto GetProductDtoById(long id);
		public void UpdateProductStock(UpdateProductStockDto updateProductStockDto);
	}
}
