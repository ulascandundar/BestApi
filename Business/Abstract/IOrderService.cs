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
	public interface IOrderService
	{
		public IResult PlaceOrder(OrderDto orderDto,long id);
		public IDataResult<PagedResult<GetOrderDto>> GetOrdersWithPaging(PageInputDto pageInputDto, long userId);
		public IResult OrderStatusUpdate(OrderStatusUpdateDto orderStatusUpdateDto);
	}
}
