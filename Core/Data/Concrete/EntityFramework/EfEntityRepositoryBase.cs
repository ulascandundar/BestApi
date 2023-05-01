using Core.Data.Abstract;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Concrete.EntityFramework
{
	public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
		where TEntity : BaseEntity, new()
	{
		protected readonly DbContext _context;

		public EfEntityRepositoryBase(DbContext context)
		{
			_context = context;
		}

		public TEntity Add(TEntity entity)
		{
			_context.Set<TEntity>().Add(entity);
			return entity;
		}

		public bool Any(Expression<Func<TEntity, bool>> predicate)
		{
			return _context.Set<TEntity>().Any(predicate);
		}

		public int Count(Expression<Func<TEntity, bool>> predicate = null)
		{
			return predicate == null ? _context.Set<TEntity>().Count(o=>o.IsActive) : _context.Set<TEntity>().Where(o=>o.IsActive).Count(predicate);
		}

		public void Delete(TEntity entity)
		{
			entity.IsActive= false;
			_context.Set<TEntity>().Update(entity);
		}

		public TEntity Get(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includeProperties)
		{
			IQueryable<TEntity> query = _context.Set<TEntity>();
			query = query.Where(filter);
			if (includeProperties.Any())
			{
				foreach (var includeProperty in includeProperties)
				{
					query = query.Include(includeProperty);
				}
			}
			return query.AsNoTracking().SingleOrDefault();
		}

		public IQueryable<TEntity> GetAsQueryable()
		{
			return _context.Set<TEntity>().AsQueryable();
		}

		public TEntity Update(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
			return entity;
		}
		public TEntity GetById(long id)
		{
			return _context.Set<TEntity>().Find(id);
		}
	}
}
