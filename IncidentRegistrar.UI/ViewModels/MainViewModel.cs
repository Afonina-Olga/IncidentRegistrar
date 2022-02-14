using System.Windows.Input;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels.Factories;

namespace IncidentRegistrar.UI.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly INavigator _navigator;

		#region IsLoggedIn

		private bool _IsLoggedIn;
		public bool IsLoggedIn
		{
			get { return _IsLoggedIn; }
			set
			{
				_IsLoggedIn = value;
				OnPropertyChanged(nameof(IsLoggedIn));
			}
		}

		#endregion

		public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

		#region Commands

		public ICommand UpdateCurrentViewModelCommand { get; }

		#endregion

		public MainViewModel(
			INavigator navigator,
			IViewModelFactory viewModelFactory)
		{
			_navigator = navigator;

			UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);
			UpdateCurrentViewModelCommand.Execute(ViewType.Login);

			_navigator.StateChanged += Navigator_StateChanged;
		}

		private void Navigator_StateChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}

		public override void Dispose()
		{
			_navigator.StateChanged -= Navigator_StateChanged;
			base.Dispose();
		}
	}
}
