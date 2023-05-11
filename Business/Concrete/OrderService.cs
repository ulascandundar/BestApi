using Business.Abstract;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Business.BaseControls;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class OrderService : BaseControls, IOrderService
	{
		protected IUnitOfWork UnitOfWork { get; }

		public OrderService(IUnitOfWork unitOfWork)
		{
			UnitOfWork= unitOfWork;
		}

		public IResult PlaceOrder(OrderDto orderDto, long id)
		{
			var result = BusinessRules.Run(() => ProductIdsControl(orderDto.OrderEntries.Select(o => o.ProductId).ToList()));
			if (result != null) return result;
			Order order = UnitOfWork.Orders.PlaceOrder(orderDto, id);
			var commitwalletresult = CommitToWallet(id, order.TotalPrice);
			if (!commitwalletresult.Success) return result;
			UnitOfWork.Save();
			return new SuccessResult();
		}
		public IDataResult<PagedResult<GetOrderDto>> GetOrdersWithPaging(PageInputDto pageInputDto,long userId)
		{
			var result = BusinessRules.Run(() => PagingInputControl(pageInputDto));
			if (result != null) return new ErrorDataResult<PagedResult<GetOrderDto>>(result.Message);
			var datas =  UnitOfWork.Orders.GetOrdersWithPaging(pageInputDto, userId);
			return new SuccessDataResult<PagedResult<GetOrderDto>>(datas);
		}
		public IResult OrderStatusUpdate(OrderStatusUpdateDto orderStatusUpdateDto)
		{
			var result = BusinessRules.Run(() => IsIdNull(orderStatusUpdateDto.OrderId),() => OrderAnyControl(orderStatusUpdateDto.OrderId));
			if (result != null) return result;
			UnitOfWork.Orders.OrderStatusUpdate(orderStatusUpdateDto);
			UnitOfWork.Save();
			return new SuccessResult();
		}
		private IResult CommitToWallet(long id, decimal totalPrice)
		{
			var wallet = UnitOfWork.Wallets.GetAsQueryable().Where(o=>o.AppUserId== id).FirstOrDefault();
			if (totalPrice > wallet.Amount) return new ErrorResult("Bakiyeniz yetersiz");
			wallet.Amount-=totalPrice;
			UnitOfWork.Wallets.Update(wallet);
			return new SuccessResult();
		}
		private IResult ProductIdsControl(List<long> ids)
		{
			foreach (var item in ids)
			{
				if (!UnitOfWork.Products.Any(o => o.Id == item && o.IsActive))
				{
					return new ErrorResult($"{item} id li ürün bulunamadı");
				}
			}
			return new SuccessResult();
		}
		private IResult OrderAnyControl(long orderId)
		{
			return UnitOfWork.Products.Any(o => o.IsActive && o.Id == orderId) ? new SuccessResult() : new ErrorResult("Böyle bir sipariş yok");
		}
	}
}
