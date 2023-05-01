using Core.Data.Concrete.EntityFramework;
using Core.Dtos;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
	public class EfProductRepository : EfEntityRepositoryBase<Product>, IProductRepository
	{
		public EfProductRepository(DbContext context) : base(context)
		{

		}
		private BestApiDbContext BestApiDbContext
		{
			get
			{
				return _context as BestApiDbContext;
			}
		}

		public ProductDto GetProductDtoById(long id)
		{
			return BestApiDbContext.Product.Where(o => o.Id == id).Select(o => new ProductDto
			{
				Name = o.Name,
				Id = o.Id,
				Price = o.Price,
				Stock = o.Stock,
				CreatedDate = o.CreatedDate,
				Desciption = new DescriptionDto
				{
					Id = o.DescriptionId,
					Content = o.Description.Content,
					Title = o.Description.Title,
				},
				UpdatedDate = o.UpdatedDate
			}).FirstOrDefault();
		}

		public PagedResult<ProductWithCategoryDto> GetProductWithPaging(PageInputDto pageInputDto)
		{
			return BestApiDbContext.Product.Where(o => o.IsActive).Select(o => new ProductWithCategoryDto
			{
				CreatedDate = o.CreatedDate,
				Id = o.Id,
				Name = o.Name,
				Price = o.Price,
				Stock = o.Stock,
				UpdatedDate = o.UpdatedDate,
				Desciption = new DescriptionDto { Id = o.DescriptionId, Content = o.Description.Content, Title = o.Description.Title },
				Categories = o.ProductCategory.Select(o => new CategoryDto { Name = o.Category.Name, Id = o.Category.Id, Description = o.Category.Description }).ToList()
			}).GetPaged(pageInputDto.PageIndex, pageInputDto.PageSize);
		}

		public void UpdateProductStock(UpdateProductStockDto updateProductStockDto)
		{
			Product product = BestApiDbContext.Product.Find(updateProductStockDto.ProductId);
			product.Stock = updateProductStockDto.Stock;
			BestApiDbContext.Product.Update(product);
		}
	}
}
