using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.ViewModels;
using IncidentRegistrar.UI.Extentions;
using IncidentRegistrar.UI.ViewModels.Factories;

namespace IncidentRegistrar.UI.Commands
{
	public class LoadIncidentsCommand : AsyncCommandBase
	{
		private readonly IIncidentRepository _incidentRepository;
		private readonly HomeViewModel _homeViewModel;
		private readonly IIncidentStore _incidentStore;
		private readonly INavigator _navigator;
		private readonly IViewModelFactory _viewModelFactory;

		public LoadIncidentsCommand(
			HomeViewModel homeViewModel,
			IIncidentRepository incidentRepository,
			IIncidentStore incidentStore,
			INavigator navigator,
			IViewModelFactory viewModelFactory)
		{
			_homeViewModel = homeViewModel;
			_incidentRepository = incidentRepository;
			_incidentStore = incidentStore;
			_viewModelFactory = viewModelFactory;
			_navigator = navigator;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				var incidents = await _incidentRepository.Get();
				_incidentStore.Incidents = incidents.ToList();
				
			}
			catch
			{
				MessageBox.Show("Ошибка загрузки данных о происшествиях");
			}
		}
	}
}
