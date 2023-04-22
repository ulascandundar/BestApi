using AutoMapper;
using Business.Abstract;
using Business.Dtos;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Business.BaseControls;
using Core.Utilities.Results;
using DataAccess.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class ProductService : BaseControls, IProductService
	{
		private BestApiDbContext _db;
		private readonly IMapper _mapper;
		public ProductService(BestApiDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
		}

		public IDataResult<PagedResult<ProductWithCategoryDto>> GetProductWithPaging(PageInputDto pageInputDto)
		{
			var result = BusinessRules.Run(() => PagingInputControl(pageInputDto));
			if (result !=null) return new ErrorDataResult<PagedResult<ProductWithCategoryDto>>(result.Message);
			var data = _db.Product.Where(o => o.IsActive).Select(o => new ProductWithCategoryDto
			{
				CreatedDate = o.CreatedDate,
				Id = o.Id,
				Name = o.Name,
				Price = o.Price,
				Stock = o.Stock,
				UpdatedDate = o.UpdatedDate,
				Desciption = new DescriptionDto { Id = o.DescriptionId, Content = o.Description.Content, Title = o.Description.Title },
				Categories= o.ProductCategory.Select(o=>new CategoryDto { Name=o.Category.Name,Id=o.Category.Id,Description=o.Category.Description}).ToList()
			}).GetPaged(pageInputDto.PageIndex, pageInputDto.PageSize);
			return new SuccessDataResult<PagedResult<ProductWithCategoryDto>>(data);
		}

		public IResult AddProduct(ProductAddDto productAddDto)
		{
			var result = BusinessRules.Run(() => NameControl(productAddDto.Name),() => CategoryControl(productAddDto.CategoryIds));
			if (result != null) return result;
			Product product = new Product
			{
				Name = productAddDto.Name,
				Price = productAddDto.Price,
				Stock = productAddDto.Stock,
				IsActive = true,
				Description = new Description { IsActive = true, Content = productAddDto.Description.Content,
					Title = productAddDto.Description.Title },
				ProductCategory = new List<ProductCategory>()
			};
			foreach (var item in productAddDto.CategoryIds)
			{
				product.ProductCategory.Add(new ProductCategory { CategoryId = item, IsActive = true });
			}
			_db.Product.Add(product);
			//product.ProductCategory.Add(new ProductCategory { IsActive= true,CategoryId=productAddDto.CategoryId });
			_db.SaveChanges();
			return new SuccessResult();
		}



		private IResult NameControl(string productName)
		{
			var isAny = _db.Product.Where(o=>o.IsActive&&o.Name.ToLower() == productName.ToLower()).Any();
			return isAny ? new ErrorResult("Ürün ismi daha önce kullanılmış") : new SuccessResult();
		}
		private IResult CategoryControl(List<long> categoryIds)
		{
			foreach (var item in categoryIds)
			{
				var isAny = _db.Category.Where(o => o.Id == item && o.IsActive).Any();
				if (!isAny) return new ErrorResult($"{item} id li kategori bulunamadı");
			}
			return new SuccessResult();
		}
	}
}
