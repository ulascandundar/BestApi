using Core.Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
	public class Order : BaseEntity
	{
		public string Description { get; set; }
		public long AppUserId { get; set; }
		[ForeignKey("AppUserId")]
		public virtual AppUser AppUser { get; set; }
		public virtual ICollection<OrderEntry> Entries { get; set; }
		public decimal TotalPrice { get; set; }
		public OrderStatus OrderStatus { get; set; }
	}
}
