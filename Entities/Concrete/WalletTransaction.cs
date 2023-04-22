using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class WalletTransaction:BaseEntity
	{
		public decimal Amount { get; set; }
		public long WalletId { get; set; }
		public virtual Wallet Wallet { get; set; }
		public bool IsPositive { get; set; }
	}
}
