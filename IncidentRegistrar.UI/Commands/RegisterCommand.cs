using System;
using System.ComponentModel;
using System.Threading.Tasks;

using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Commands
{
	public class RegisterCommand : AsyncCommandBase
	{
		private readonly RegisterViewModel _registerViewModel;
		private readonly IRenavigator _registerRenavigator;

		public RegisterCommand(RegisterViewModel registerViewModel, IRenavigator registerRenavigator)
		{
			_registerViewModel = registerViewModel;
			_registerRenavigator = registerRenavigator;

			_registerViewModel.PropertyChanged += RegisterViewModel_PropertyChanged;
		}

		public override bool CanExecute(object parameter)
		{
			return _registerViewModel.CanRegister && base.CanExecute(parameter);
		}

		public override Task ExecuteAsync(object parameter)
		{
			throw new NotImplementedException();
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
