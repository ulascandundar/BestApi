using Business.Abstract;
using Business.Dtos;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Business.BaseControls;
using Core.Utilities.Results;
using DataAccess.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class CategoryService : BaseControls, ICategoryService
	{
		private BestApiDbContext _db;

		public CategoryService(BestApiDbContext db)
		{
			_db = db;
		}

		public IResult AddCategory(CategoryAddDto categoryAddDto)
		{
			Category category = new Category { Description = categoryAddDto.Description, Name = categoryAddDto.Name };
			_db.Category.Add(category);
			_db.SaveChanges();
			return new SuccessResult();
		}

		public IDataResult<PagedResult<CategoryDto>> GetCategoryWithPaging(PageInputDto pageInputDto)
		{
			var result = BusinessRules.Run(() => PagingInputControl(pageInputDto));
			if (result != null) return new ErrorDataResult<PagedResult<CategoryDto>>(result.Message);
			var data = _db.Category.Where(o=>o.IsActive).Select(o =>  new CategoryDto { Name = o.Name, Description = o.Description, CreatedDate = o.CreatedDate,
				UpdatedDate = o.UpdatedDate }).GetPaged(pageInputDto.PageIndex,pageInputDto.PageSize);
			return new SuccessDataResult<PagedResult<CategoryDto>>(data);
		}
	}
}
