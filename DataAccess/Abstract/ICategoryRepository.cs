﻿using Core.Data.Abstract;
using Core.Dtos;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
	public interface ICategoryRepository : IEntityRepository<Category>
	{
		public PagedResult<CategoryDto> GetCategoryWithPaging(PageInputDto pageInputDto);
	}
}
