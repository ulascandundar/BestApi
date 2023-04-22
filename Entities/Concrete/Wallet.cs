using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Wallet:BaseEntity
	{
		public long AppUserId { get; set; }
		public virtual AppUser AppUser { get; set; }
		public decimal Amount { get; set; }
		public virtual ICollection<WalletTransaction> WalletTransactions { get; set; }
	}
}
