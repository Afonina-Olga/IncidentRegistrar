using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.UI.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity>
		where TEntity : DomainObject
	{
		private readonly IncidentRegistrarDbContextFactory _contextFactory;

		public Repository(IncidentRegistrarDbContextFactory contextFactory)
			=> _contextFactory = contextFactory;

		public virtual async Task<TEntity> Create(TEntity entity)
		{
			using var context = _contextFactory.CreateDbContext();
			var createdEntity = await context.Set<TEntity>().AddAsync(entity);
			await context.SaveChangesAsync();
			return createdEntity.Entity;
		}

		public virtual async Task<bool> Delete(int id)
		{
			using var context = _contextFactory.CreateDbContext();
			var entity = await Get(id);
			if (entity != null)
			{
				context.Set<TEntity>().Remove(entity);
				await context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public virtual async Task<TEntity> Get(int id)
		{
			using var context = _contextFactory.CreateDbContext();
			TEntity entity = await context.Set<TEntity>().FindAsync(id);
			return entity;
		}

		public virtual async Task<IEnumerable<TEntity>> Get()
		{
			using var context = _contextFactory.CreateDbContext();
			var entities = await context.Set<TEntity>().ToListAsync();
			return entities;
		}

		public virtual async Task<TEntity> Update(int id, TEntity entity)
		{
			using var context = _contextFactory.CreateDbContext();
			var entityToUpdate = await Get(id);
			if (entityToUpdate != null)
			{
				entity.Id = id;
				var updatedEntity = context.Set<TEntity>().Update(entity);
				await context.SaveChangesAsync();
				return updatedEntity.Entity;
			}
			return null;
		}
	}
}
