using System.Windows;
using System.Threading.Tasks;

using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels;
using IncidentRegistrar.UI.Services;
using System;

namespace IncidentRegistrar.UI.Commands
{
	public class LoginCommand : AsyncCommandBase
	{
		private readonly LoginViewModel _loginViewModel;
		private readonly IRenavigator _renavigator;
		private readonly IUserStore _userStore;
		private readonly IAuthenticationService _authenticationService;

		public LoginCommand(
			LoginViewModel loginViewModel,
			IRenavigator renavigator,
			IUserStore userStore,
			IAuthenticationService authenticationService)
		{
			_loginViewModel = loginViewModel;
			_renavigator = renavigator;
			_userStore = userStore;
			_authenticationService = authenticationService;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				var login = _loginViewModel.Login.Trim();
				var password = _loginViewModel.Password.Trim();

				var user = await _authenticationService.Login(login, password);

				if (user != null)
				{
					_renavigator.Renavigate();
					_userStore.User = user;
				}
				else
					MessageBox.Show("Неверный логин или пароль");
			}
			catch(Exception ex)
			{
				MessageBox.Show("Не удалось войти в систему");
			}
		}
	}
}
