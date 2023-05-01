using DataAccess.Abstract;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly BestApiDbContext _context;
		private EfCategoryRepository _categoryRepository;
		private EfProductRepository _productRepository;
		private EfOrderRepository _orderRepository;
		private EfWalletRepository _walletRepository;
		public UnitOfWork(BestApiDbContext context)
		{
			_context = context;
		}
		public ICategoryRepository Categories => _categoryRepository ??= new EfCategoryRepository(_context);
		public IProductRepository Products => _productRepository ??= new EfProductRepository(_context);
		public IOrderRepository Orders => _orderRepository ??= new EfOrderRepository(_context);
		public IWalletRepository Wallets => _walletRepository ??= new EfWalletRepository(_context);

		public async ValueTask DisposeAsync()
		{
			await _context.DisposeAsync();
		}

		public int Save()
		{
			return _context.SaveChanges();
		}
	}
}
