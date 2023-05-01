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
    public interface IOrderRepository : IEntityRepository<Order>
    {
        public Order PlaceOrder(OrderDto orderDto, long id);
		public PagedResult<GetOrderDto> GetOrdersWithPaging(PageInputDto pageInputDto, long userId);
        public void OrderStatusUpdate(OrderStatusUpdateDto orderStatusUpdateDto);
	}
}
