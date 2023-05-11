using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		ICategoryRepository Categories { get; }
		IProductRepository Products { get; }
		IOrderRepository Orders { get; }
		IWalletRepository Wallets { get; }
		int Save();
	}
}
