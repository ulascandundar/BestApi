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
	public class OrderController : BaseController
	{
	    private	IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		[HttpPost("place-order")]
		[Authorize(Roles ="Customer")]
		public IActionResult PlaceOrder([FromBody] OrderDto orderDto)
		{
			var products = _orderService.PlaceOrder(orderDto,GetUserId());
			return Ok(products);
		}
		[HttpGet("orders")]
		[Authorize(Roles = "Customer")]
		public IActionResult GetOrders([FromQuery] PageInputDto pageInputDto)
		{
			var products = _orderService.GetOrdersWithPaging(pageInputDto,GetUserId());
			return Ok(products);
		}
		[HttpGet("detail/{id}")]
		[Authorize(Roles = "Customer")]
		public IActionResult GetDetail([FromQuery] long id)
		{
			return Ok();
		}
	}
}
