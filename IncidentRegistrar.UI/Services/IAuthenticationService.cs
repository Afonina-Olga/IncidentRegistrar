using IncidentRegistrar.UI.Models;
using System.Threading.Tasks;

namespace IncidentRegistrar.UI.Services
{
	public interface IAuthenticationService
	{
		Task<User> Login(string login, string password);

		Task<RegistrationResult> Register(string login, string password, string confirmPassword);
	}
}
