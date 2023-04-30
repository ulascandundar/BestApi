using Business.Dtos;
using Core.Dtos;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IProductService
	{
		public IDataResult<PagedResult<ProductWithCategoryDto>> GetProductWithPaging(PageInputDto pageInputDto);
		public IResult AddProduct(ProductAddDto productAddDto);
		public IDataResult<byte[]> QrCodeToProduct(long productId);

	}
}
