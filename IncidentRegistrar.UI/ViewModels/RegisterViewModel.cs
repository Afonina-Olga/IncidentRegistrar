using System.Windows.Input;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.ViewModels
{
	public class RegisterViewModel : ViewModelBase
	{
		#region Login Property

		private string _login;
		public string Login
		{
			get
			{
				return _login;
			}
			set
			{
				_login = value;
				OnPropertyChanged(nameof(Login));
				OnPropertyChanged(nameof(CanRegister));
			}
		}

		#endregion

		#region Password Property

		private string _password;
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
				OnPropertyChanged(nameof(CanRegister));
			}
		}

		#endregion

		#region ConfirmPassword Property

		private string _confirmPassword;
		public string ConfirmPassword
		{
			get
			{
				return _confirmPassword;
			}
			set
			{
				_confirmPassword = value;
				OnPropertyChanged(nameof(ConfirmPassword));
				OnPropertyChanged(nameof(CanRegister));
			}
		}

		#endregion

		public bool CanRegister =>
			!string.IsNullOrEmpty(Login) &&
			!string.IsNullOrEmpty(Password) &&
			!string.IsNullOrEmpty(ConfirmPassword);

		#region Commands

		public ICommand RegisterCommand { get; }

		public ICommand ViewLoginCommand { get; }

		#endregion Commands

		public RegisterViewModel(IRenavigator registerRenavigator, IRenavigator loginRenavigator)
		{
			RegisterCommand = new RegisterCommand(this, registerRenavigator);
			ViewLoginCommand = new RenavigateCommand(loginRenavigator);
		}
	}
}
