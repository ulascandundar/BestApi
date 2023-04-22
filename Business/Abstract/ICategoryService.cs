using Business.Dtos;
using Core.Dtos;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ICategoryService
	{
		public IDataResult<PagedResult<CategoryDto>> GetCategoryWithPaging(PageInputDto pageInputDto);
		public IResult AddCategory(CategoryAddDto categoryAddDto);
	}
}
