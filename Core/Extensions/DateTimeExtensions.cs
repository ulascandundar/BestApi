using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime ToTurkishLocal(this DateTime dateTime)
		{
			dateTime = dateTime.ToUniversalTime();
			return dateTime.AddHours(3);
		}
	}
}
