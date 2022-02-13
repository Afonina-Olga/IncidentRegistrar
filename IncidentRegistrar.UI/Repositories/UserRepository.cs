using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.UI.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private readonly IncidentRegistrarDbContextFactory _contextFactory;

		public UserRepository(IncidentRegistrarDbContextFactory contextFactory)
			: base(contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public async Task<User> Get(string login, string password)
		{
			using var context = _contextFactory.CreateDbContext();
			var user = await context.Users.FirstOrDefaultAsync(
				user => user.Login == login && user.Password == password);
			return user;
		}

		public async Task<User> Get(string login)
		{
			using var context = _contextFactory.CreateDbContext();
			var user = await context.Users
				.FirstOrDefaultAsync(user => user.Login == login);
			return user;
		}
	}
}
