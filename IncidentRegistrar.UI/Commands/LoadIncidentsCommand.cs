using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.ViewModels;
using IncidentRegistrar.UI.Extentions;

namespace IncidentRegistrar.UI.Commands
{
	public class LoadIncidentsCommand : AsyncCommandBase
	{
		private readonly IIncidentRepository _incidentRepository;
		private readonly HomeViewModel _homeViewModel;
		private readonly IIncidentStore _incidentStore;

		public LoadIncidentsCommand(
			HomeViewModel homeViewModel, 
			IIncidentRepository incidentRepository, 
			IIncidentStore incidentStore)
		{
			_homeViewModel = homeViewModel;
			_incidentRepository = incidentRepository;
			_incidentStore = incidentStore;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				var incidents = await _incidentRepository.Get();
				_homeViewModel.Incidents = new ObservableCollection<IncidentViewModel>(
					incidents
						.Select(incident => new IncidentViewModel(_incidentStore, _incidentRepository)
						{
							Id = incident.Id,
							IncidentType = incident.IncidentType.FromIncidentType(),
							ResolutionType = incident.ResolutionType.FromResolutionType(),
							RegDate = incident.RegDate,
							Participants = incident.Participants.ToPersonString()
						}));
			}
			catch
			{
				MessageBox.Show("Ошибка загрузки данных о происшествиях");
			}
		}
	}
}
