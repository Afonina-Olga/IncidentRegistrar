using System.Collections.Generic;
using System.Threading.Tasks;

namespace IncidentRegistrar.UI.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<TEntity> Create(TEntity entity);

		Task<TEntity> Get(int id);

		Task<IEnumerable<TEntity>> Get();

		Task<bool> Delete(int id);

		Task<TEntity> Update(int id, TEntity entity);
	}
}
