using System.Threading.Tasks;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.UI.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		/// <summary>
		/// Получить пользователя по логину и паролю
		/// </summary>
		/// <param name="login">Логин</param>
		/// <param name="password">Пароль</param>
		/// <returns>Данные пользователя</returns>
		Task<User> Get(string login, string password);

		/// <summary>
		/// Получить пользователя по логину
		/// </summary>
		/// <param name="login">Логин</param>
		/// <returns>Данные пользователя</returns>
		Task<User> Get(string login);
	}
}
