using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.ViewModels;
using IncidentRegistrar.UI.Extentions;

namespace IncidentRegistrar.UI.Commands
{
	public class LoadIncidentsCommand : AsyncCommandBase
	{
		private readonly IIncidentRepository _incidentRepository;
		private readonly HomeViewModel _homeViewModel;

		public LoadIncidentsCommand(HomeViewModel homeViewModel, IIncidentRepository incidentRepository)
		{
			_homeViewModel = homeViewModel;
			_incidentRepository = incidentRepository;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				var incidents = await _incidentRepository.Get();
				_homeViewModel.Incidents = new ObservableCollection<IncidentViewModel>(
					incidents
						.Select(incident => new IncidentViewModel(_homeViewModel, _incidentRepository)
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
				_homeViewModel.ErrorMessage = "Ошибка загрузки данных о происшествиях";
			}
		}
	}
}
