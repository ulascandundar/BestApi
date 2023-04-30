using Business.Abstract;
using Business.Dtos;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _service;

		public ProductsController(IProductService service)
		{
			_service = service;
		}
		/// GET api/products
		[HttpGet]
		[Authorize]
		public IActionResult GetAllWithPaging([FromQuery]PageInputDto pageInputDto)
		{
			var products = _service.GetProductWithPaging(pageInputDto);
			return Ok(products);
		}
		/// POST api/products
		[HttpPost]
		public IActionResult AddProduct([FromBody] ProductAddDto productAddDto)
		{
			var products = _service.AddProduct(productAddDto);
			//test
			return Ok(products);
		}
		[HttpGet("qr-code")]
		[Authorize]
		public IActionResult GetProductQrCode([FromQuery] long productId)
		{
			var qrcode = _service.QrCodeToProduct(productId);
			return Ok(qrcode);
		}
	}
}
