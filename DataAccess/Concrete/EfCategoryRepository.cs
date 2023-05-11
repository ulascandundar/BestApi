using Core.Data.Concrete.EntityFramework;
using Core.Dtos;
using Core.Extensions;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
	public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
	{
		public EfCategoryRepository(DbContext context) : base(context)
		{
		}
		private BestApiDbContext BestApiDbContext
		{
			get
			{
				return _context as BestApiDbContext;
			}
		}
		public PagedResult<CategoryDto> GetCategoryWithPaging(PageInputDto pageInputDto)
		{
			return BestApiDbContext.Category.Where(o => o.IsActive).Select(o => new CategoryDto
			{
				Name = o.Name,
				Description = o.Description,
				CreatedDate = o.CreatedDate,
				UpdatedDate = o.UpdatedDate
			}).GetPaged(pageInputDto.PageIndex, pageInputDto.PageSize);
		}
	}
}
