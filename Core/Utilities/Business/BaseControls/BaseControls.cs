using Core.Dtos;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business.BaseControls
{
	public class BaseControls
	{
		public IResult PagingInputControl(PageInputDto dto)
		{
			return dto.PageIndex < 1 ? new Result(false, "Sayfa 1 den küçük olmamalı")
				: dto.PageSize < 1 ? new Result(false, "Sayfa boyutu 1 den küçük olmamalı") :
				new Result(true);
		}
	}
}
