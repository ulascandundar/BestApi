using Core.Data.Concrete.EntityFramework;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Results;
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
	public class EfOrderRepository : EfEntityRepositoryBase<Order>, IOrderRepository
	{
		public EfOrderRepository(DbContext context) : base(context)
		{

		}
		private BestApiDbContext BestApiDbContext
		{
			get
			{
				return _context as BestApiDbContext;
			}
		}

		public PagedResult<GetOrderDto> GetOrdersWithPaging(PageInputDto pageInputDto, long userId)
		{
			return BestApiDbContext.Order.Where(o => o.IsActive && o.AppUserId == userId).Select(o => new GetOrderDto
			{
				Id = o.Id,
				Created = o.CreatedDate,
				Description = o.Description,
				TotalPrice = o.TotalPrice
			}).GetPaged(pageInputDto.PageIndex, pageInputDto.PageSize);
		}

		public void OrderStatusUpdate(OrderStatusUpdateDto orderStatusUpdateDto)
		{
			Order order = BestApiDbContext.Order.Find(orderStatusUpdateDto.OrderId);
			order.OrderStatus=orderStatusUpdateDto.OrderStatus;
			BestApiDbContext.Order.Update(order);
		}

		public Order PlaceOrder(OrderDto orderDto, long id)
		{
			decimal totalPrice = 0;
			Order order = new Order(); order.Entries = new List<OrderEntry>();
			foreach (var item in orderDto.OrderEntries)
			{
				var orderentryprice = BestApiDbContext.Product.Where(o => o.Id == item.ProductId).Select(item => item.Price).FirstOrDefault();
				order.Entries.Add(new OrderEntry { ProductId = item.ProductId, Quantity = item.Quantity, IsActive = true, Price = orderentryprice });
				totalPrice += orderentryprice;
			}
			order.TotalPrice = totalPrice;
			//var commitwalletresult = CommitToWallet(id, totalPrice);
			//if (!commitwalletresult.Success) return result;
			BestApiDbContext.Order.Add(order);
			return order;
		}
	}
}
