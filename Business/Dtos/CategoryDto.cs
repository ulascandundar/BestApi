using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
	public class CategoryDto:BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
