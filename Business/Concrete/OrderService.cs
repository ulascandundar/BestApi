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
	public class OrderService : BaseControls, IOrderService
	{
		private BestApiDbContext _db;

		public OrderService(BestApiDbContext db)
		{
			_db = db;
		}

		public IResult PlaceOrder(OrderDto orderDto, long id)
		{
			var result = BusinessRules.Run(() => ProductIdsControl(orderDto.OrderEntries.Select(o => o.ProductId).ToList()));
			if (result != null) return result;
			decimal totalPrice = 0;
			Order order = new Order(); order.Entries = new List<OrderEntry>();
			foreach (var item in orderDto.OrderEntries)
			{
				var orderentryprice = _db.Product.Where(o => o.Id == item.ProductId).Select(item => item.Price).FirstOrDefault();
				order.Entries.Add(new OrderEntry { ProductId = item.ProductId, Quantity = item.Quantity, IsActive = true, Price = orderentryprice });
				totalPrice += orderentryprice;
			}
			order.TotalPrice = totalPrice;
			var commitwalletresult = CommitToWallet(id, totalPrice);
			if (!commitwalletresult.Success) return result;
			_db.Order.Add(order);

			_db.SaveChanges();
			return new SuccessResult();
		}
		public IDataResult<PagedResult<GetOrderDto>> GetOrdersWithPaging(PageInputDto pageInputDto,long userId)
		{
			var result = BusinessRules.Run(() => PagingInputControl(pageInputDto));
			if (result != null) return new ErrorDataResult<PagedResult<GetOrderDto>>(result.Message);
			var datas =  _db.Order.Where(o=>o.IsActive&&o.AppUserId==userId).Select(o=> new GetOrderDto 
			{
				Id = o.Id,
				Created=o.CreatedDate,
				Description=o.Description,
				TotalPrice=o.TotalPrice
			}).GetPaged(pageInputDto.PageIndex, pageInputDto.PageSize);
			return new SuccessDataResult<PagedResult<GetOrderDto>>(datas);
		}
		private IResult CommitToWallet(long id, decimal totalPrice)
		{
			var wallet = _db.Wallet.Where(o=>o.AppUserId== id).FirstOrDefault();
			if (totalPrice > wallet.Amount) return new ErrorResult("Bakiyeniz yetersiz");
			wallet.Amount-=totalPrice;
			_db.Wallet.Update(wallet);
			_db.WalletTransaction.Add(new WalletTransaction { WalletId = wallet.Id, IsActive = true, Amount = totalPrice, IsPositive = false });
			return new SuccessResult();
		}
		private IResult ProductIdsControl(List<long> ids)
		{
			foreach (var item in ids)
			{
				if (!_db.Product.Where(o => o.Id == item && o.IsActive).Any())
				{
					return new ErrorResult($"{item} id li ürün bulunamadı");
				}
			}
			return new SuccessResult();
		}
	}
}
