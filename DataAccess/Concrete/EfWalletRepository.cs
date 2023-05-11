using Core.Data.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
	public class EfWalletRepository : EfEntityRepositoryBase<Wallet>, IWalletRepository
	{
		public EfWalletRepository(DbContext context) : base(context)
		{

		}
	}
}
