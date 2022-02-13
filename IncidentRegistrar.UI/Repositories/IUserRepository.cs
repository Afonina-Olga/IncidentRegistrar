using System.Threading.Tasks;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.UI.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<bool> IsValid(string login, string password);
	}
}
