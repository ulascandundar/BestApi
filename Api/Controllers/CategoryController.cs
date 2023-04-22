using Autofac.Core;
using Business.Abstract;
using Business.Dtos;
using Core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private ICategoryService _categoryService;

		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		//[HttpGet]
		//public IActionResult GetAllWithPaging([FromQuery] PageInputDto pageInputDto)
		//{
		//	//
		//}
		/// POST api/category
		[HttpPost]
		[Authorize]
		public IActionResult AddProduct([FromBody] CategoryAddDto categoryAddDto)
		{
			var products = _categoryService.AddCategory(categoryAddDto);
			return Ok(products);
		}
	}
}

