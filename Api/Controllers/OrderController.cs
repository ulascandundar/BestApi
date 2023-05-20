using Autofac.Core;
using Business.Abstract;
using Entities.Dtos;
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
			var result = _orderService.PlaceOrder(orderDto,GetUserId());
			return Ok(result);
		}
		[HttpGet("orders")]
		[Authorize(Roles = "Customer")]
		public IActionResult GetOrders([FromQuery] PageInputDto pageInputDto)
		{
			var result = _orderService.GetOrdersWithPaging(pageInputDto,GetUserId());
			return Ok(result);
		}
		[HttpGet("detail/{id}")]
		[Authorize(Roles = "Customer")]
		public IActionResult GetDetail(long id)
		{
			return Ok();
		}
		[HttpPut]
		[Authorize(Roles = "Admin")]
		public IActionResult OrderStatusUpdate([FromBody] OrderStatusUpdateDto orderStatusUpdateDto)
		{
			var result = _orderService.OrderStatusUpdate(orderStatusUpdateDto);
			return Ok(result);
		}
	}
}
