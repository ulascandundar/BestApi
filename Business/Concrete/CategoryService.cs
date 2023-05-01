using Business.Abstract;
using Entities.Dtos;
using Core.Dtos;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Business.BaseControls;
using Core.Utilities.Results;
using DataAccess.Abstract;
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
		protected IUnitOfWork UnitOfWork { get; }

		public CategoryService(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}

		public IResult AddCategory(CategoryAddDto categoryAddDto)
		{
			Category category = new Category { Description = categoryAddDto.Description, Name = categoryAddDto.Name };
			UnitOfWork.Categories.Add(category);
			UnitOfWork.Save();
			return new SuccessResult();
		}

		public IDataResult<PagedResult<CategoryDto>> GetCategoryWithPaging(PageInputDto pageInputDto)
		{
			var result = BusinessRules.Run(() => PagingInputControl(pageInputDto));
			if (result != null) return new ErrorDataResult<PagedResult<CategoryDto>>(result.Message);
			var data = UnitOfWork.Categories.GetCategoryWithPaging(pageInputDto);
			return new SuccessDataResult<PagedResult<CategoryDto>>(data);
		}
	}
}
