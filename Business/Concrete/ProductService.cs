using AutoMapper;
using Business.Abstract;
using Core.Dtos;
using Core.Utilities.Business;
using Core.Utilities.Business.BaseControls;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dtos;
using System.Text.Json;

namespace Business.Concrete
{
	public class ProductService : BaseControls, IProductService
	{
		protected IUnitOfWork UnitOfWork { get; }
		private readonly IMapper _mapper;
		private readonly IQrCodeService _qrCodeService;
		public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IQrCodeService qrCodeService)
		{
			UnitOfWork=unitOfWork;
			_mapper = mapper;
			_qrCodeService = qrCodeService;
		}

		public IDataResult<PagedResult<ProductWithCategoryDto>> GetProductWithPaging(PageInputDto pageInputDto)
		{
			var result = BusinessRules.Run(() => PagingInputControl(pageInputDto));
			if (result !=null) return new ErrorDataResult<PagedResult<ProductWithCategoryDto>>(result.Message);
			var data = UnitOfWork.Products.GetProductWithPaging(pageInputDto);
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
			UnitOfWork.Products.Add(product);
			UnitOfWork.Save();
			return new SuccessResult();
		}

		public IDataResult<byte[]> QrCodeToProduct(long productId)
		{
			var result = BusinessRules.Run(()=> IsIdNull(productId),() => ProductAnyControl(productId));
			if (result != null) return new ErrorDataResult<byte[]>(result.Message);
			ProductDto productDto = UnitOfWork.Products.GetProductDtoById(productId);
			string plainText = JsonSerializer.Serialize(productDto);
			var qrCode = _qrCodeService.GenerateQrCode(plainText);
			return new SuccessDataResult<byte[]>(qrCode);
		}
		public IResult UpdateProductStock(UpdateProductStockDto updateProductStockDto)
		{
			var result = BusinessRules.Run(() => IsIdNull(updateProductStockDto.ProductId), () => ProductAnyControl(updateProductStockDto.ProductId));
			if (result != null) return result;
			UnitOfWork.Products.UpdateProductStock(updateProductStockDto);
			UnitOfWork.Save();
			return new SuccessResult();
		}
		private IResult NameControl(string productName)
		{
			var isAny = UnitOfWork.Products.Any(o=>o.IsActive&&o.Name.ToLower() == productName.ToLower());
			return isAny ? new ErrorResult("Ürün ismi daha önce kullanılmış") : new SuccessResult();
		}
		private IResult CategoryControl(List<long> categoryIds)
		{
			foreach (var item in categoryIds)
			{
				var isAny = UnitOfWork.Categories.Any(o => o.Id == item && o.IsActive);
				if (!isAny) return new ErrorResult($"{item} id li kategori bulunamadı");
			}
			return new SuccessResult();
		}
		private IResult ProductAnyControl(long id)
		{
			var isAny = UnitOfWork.Products.Any(o => o.Id == id && o.IsActive);
			return isAny ? new SuccessResult() : new ErrorResult("Böyle bir ürün bulunamadı");
		}

	}
}
