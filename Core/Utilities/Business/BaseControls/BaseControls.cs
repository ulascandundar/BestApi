using Core.Dtos;
using Core.Entities;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core.Utilities.Business.BaseControls
{
	public class BaseControls
	{
		public IResult PagingInputControl(PageInputDto dto)
		{
			return dto.PageIndex < 1 ? new ErrorResult("Sayfa 1 den küçük olmamalı")
				: dto.PageSize < 1 ? new ErrorResult("Sayfa boyutu 1 den küçük olmamalı") :
				new SuccessResult();
		}
		public IResult IsIdNull(long id)
		{
			return id == 0 || id == null || id <0 ? new ErrorResult("Id değeri boş") : new SuccessResult();
		}
	}
}
