﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
	public class OrderEntryDto
	{
		public long ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
