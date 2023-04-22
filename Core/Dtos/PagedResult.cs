using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
	public class PagedResult<T>
	{
		public int CurrentPage { get; set; }
		public int PageCount { get; set; }
		public int PageSize { get; set; }
		public int RowCount { get; set; }
		public virtual int TotalPages => (int)Math.Ceiling(decimal.Divide(RowCount, PageSize));
		public virtual bool ShowPrevious => CurrentPage > 1;
		public virtual bool ShowNext => CurrentPage < TotalPages;
		public IList<T> Results { get; set; }
	}
}
