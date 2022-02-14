using System;
using System.Windows;
using System.Windows.Input;

using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels;
using IncidentRegistrar.UI.ViewModels.Factories;

namespace IncidentRegistrar.UI.Commands
{
	public class ReadIncidentCommand : ICommand
	{
		private readonly IncidentViewModel _viewModel;
		private readonly INavigator _navigator;
		private readonly IViewModelFactory _viewModelFactory;
		private readonly ICurrentIncidentStore _currentIncidentStore;

		public ReadIncidentCommand(
			IncidentViewModel viewModel,
			INavigator navigator,
			IViewModelFactory viewModelFactory,
			ICurrentIncidentStore currentIncidentStore)
		{
			_viewModel = viewModel;
			_navigator = navigator;
			_viewModelFactory = viewModelFactory;
			_currentIncidentStore = currentIncidentStore;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			try
			{
				_currentIncidentStore.Id = _viewModel.Id;
				_currentIncidentStore.RegDate = _viewModel.RegDate;
				_currentIncidentStore.IncidentType = _viewModel.IncidentType;
				_currentIncidentStore.ResolutionType = _viewModel.ResolutionType;
				_currentIncidentStore.Participants = _viewModel.Participants;

				if (parameter is ViewType)
				{
					var currentViewModelCommand = new UpdateCurrentViewModelCommand(_navigator, _viewModelFactory);
					currentViewModelCommand.Execute((ViewType)parameter);
				}
			}
			catch
			{
				MessageBox.Show("Не удалось получить сведения о происшествии");
			}
		}
	}
}
