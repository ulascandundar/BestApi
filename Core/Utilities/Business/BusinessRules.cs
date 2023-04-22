using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
	public class BusinessRules
	{
		public static IResult Run(params Func<IResult>[] logics)
		{
			for (int i = 0; i < logics.Length; i++)
			{
				var result = logics[i]();
				if (!result.Success)
				{
					return result;
				}
			}

			return null;
		}
	}
}
