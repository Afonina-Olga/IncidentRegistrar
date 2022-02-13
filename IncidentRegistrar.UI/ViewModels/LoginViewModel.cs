using System.Windows.Input;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.Services;
using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		#region Login Property

		private string _login;
		public string Login
		{
			get { return _login; }
			set
			{
				_login = value;
				OnPropertyChanged(nameof(Login));
			}
		}

		#endregion

		#region Password Property

		private string _password;
		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}

		#endregion

		public bool CanLogin => !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);

		#region Commands

		public ICommand LoginCommand { get; }
		public ICommand ViewRegisterCommand { get; }

		#endregion

		public LoginViewModel(
			IRenavigator loginRenavigator,
			IRenavigator registerRenavigator,
			IUserStore userStore,
			IAuthenticationService authenticationService)
		{
			LoginCommand = new LoginCommand(this, loginRenavigator, userStore, authenticationService);
			ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
		}
	}
}
