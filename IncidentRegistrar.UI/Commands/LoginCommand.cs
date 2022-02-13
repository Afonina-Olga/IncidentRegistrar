using System;
using System.Threading.Tasks;

using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Commands
{
	public class LoginCommand : AsyncCommandBase
	{
		private readonly LoginViewModel _loginViewModel;
		private readonly IRenavigator _renavigator;

		public LoginCommand(LoginViewModel loginViewModel, IRenavigator renavigator)
		{
			_loginViewModel = loginViewModel;
			_renavigator = renavigator;
		}

		public override Task ExecuteAsync(object parameter)
		{
			throw new NotImplementedException();
		}
	}
}
