using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

using IncidentRegistrar.UI.Services;
using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Commands
{
	public class RegisterCommand : AsyncCommandBase
	{
		private readonly RegisterViewModel _registerViewModel;
		private readonly IRenavigator _registerRenavigator;
		private readonly IAuthenticationService _authenticationService;

		public RegisterCommand(
			RegisterViewModel registerViewModel,
			IRenavigator registerRenavigator,
			IAuthenticationService authenticationService)
		{
			_registerViewModel = registerViewModel;
			_registerRenavigator = registerRenavigator;
			_authenticationService = authenticationService;

			_registerViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
		}

		public override bool CanExecute(object parameter)
		{
			return _registerViewModel.CanRegister && base.CanExecute(parameter);
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				var login = _registerViewModel.Login.Trim();
				var password = _registerViewModel.Password.Trim();
				var confirmPassword = _registerViewModel.ConfirmPassword;

				var result = await _authenticationService.Register(login, password, confirmPassword);

				if (result == RegistrationResult.LoginAlreadyExists)
				{
					MessageBox.Show("Логин уже зарегистрирован");
				}
				else if (result == RegistrationResult.PasswordsDoNotMatch)
				{
					MessageBox.Show("Пароли не совпадают");
				}
				else if (result == RegistrationResult.Success)
				{
					_registerRenavigator.Renavigate();
				}
			}
			catch
			{
				MessageBox.Show("Не удалось зарегистрироваться");
			}
		}

		private void RegisterViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(RegisterViewModel.CanRegister))
			{
				OnCanExecuteChanged();
			}
		}
	}
}
