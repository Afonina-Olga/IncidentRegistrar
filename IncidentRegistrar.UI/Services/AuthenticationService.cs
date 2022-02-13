using System.Threading.Tasks;

using IncidentRegistrar.UI.Models;
using IncidentRegistrar.UI.Repositories;

namespace IncidentRegistrar.UI.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IUserRepository _userRepository;

		public AuthenticationService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<User> Login(string login, string password)
		{
			var storedUser = await _userRepository.Get(login, password);
			return storedUser;
		}

		public async Task<RegistrationResult> Register(string login, string password, string confirmPassword)
		{
			if (password != confirmPassword)
				return RegistrationResult.PasswordsDoNotMatch;

			var existingUser = await _userRepository.Get(login);

			if (existingUser != null)
				return RegistrationResult.LoginAlreadyExists;

			var newUser = await _userRepository.Create(
				new User()
				{
					Login = login,
					Password = password
				});

			return RegistrationResult.Success;
		}
	}
}
