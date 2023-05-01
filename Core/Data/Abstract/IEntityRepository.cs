using Core.Dtos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Abstract
{
	public interface IEntityRepository<T> where T : BaseEntity, new()
	{
		T Add(T entity);
		T Update(T entity);
		void Delete(T entity);
		T Get(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
		bool Any(Expression<Func<T, bool>> predicate);
		int Count(Expression<Func<T, bool>> predicate = null);
		IQueryable<T> GetAsQueryable();
		T GetById(long id);
	}
}
