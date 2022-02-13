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
		private readonly MainViewModel _mainViewModel;

		public LoadIncidentsCommand(MainViewModel mainViewModel, IIncidentRepository incidentRepository)
		{
			_mainViewModel = mainViewModel;
			_incidentRepository = incidentRepository;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				var incidents = await _incidentRepository.Get();
				_mainViewModel.Incidents = new ObservableCollection<IncidentViewModel>(
					incidents
						.Select(incident => new IncidentViewModel(_mainViewModel, _incidentRepository)
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
				_mainViewModel.ErrorMessage = "Ошибка загрузки данных о происшествиях";
			}
		}
	}
}
