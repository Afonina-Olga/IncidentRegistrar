using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using IncidentRegistrar.UI.Extentions;
using IncidentRegistrar.UI.Models;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Commands
{
	public class CreateIncidentCommand : AsyncCommandBase
	{
		private readonly HomeViewModel _homeViewModel;
		private readonly IIncidentRepository _incidentRepository;

		public CreateIncidentCommand(HomeViewModel homeViewModel, IIncidentRepository incidentRepository)
		{
			_homeViewModel = homeViewModel;
			_incidentRepository = incidentRepository;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				if (CanCreate())
				{
					var createdIncident = await _incidentRepository.Create(new Incident()
					{
						IncidentType = _homeViewModel.NewIncident.IncidentType.ToIncidentType(),
						RegDate = _homeViewModel.NewIncident.RegDate,
						ResolutionType = _homeViewModel.NewIncident.ResolutionType.ToResolutionType(),
						Participants = _homeViewModel.NewIncident.Participants.Select(participant => new Participant()
						{
							Person = new Person()
							{
								LastName = participant.LastName,
								MiddleName = participant.MiddleName,
								FirstName = participant.FirstName,
								Address = participant.Address,
								ConvictionsCount = participant.ConvictionsCount
							},
							PersonType = participant.PersonType.ToPersonType()
						})
						.ToList()
					});

					_homeViewModel.Incidents.Add(new IncidentViewModel(_homeViewModel, _incidentRepository)
					{
						IncidentType = _homeViewModel.NewIncident.IncidentType,
						Id = createdIncident.Id,
						RegDate = _homeViewModel.NewIncident.RegDate,
						ResolutionType = _homeViewModel.NewIncident.ResolutionType,
						Participants = string.Join(
							Environment.NewLine,
							_homeViewModel.NewIncident.Participants.Select(p => $"{p.LastName} {p.FirstName} {p.MiddleName}"))
					});
				}
				else
					MessageBox.Show("Заполните все сведения об инциденте");
			}
			catch (Exception ex)
			{
				_homeViewModel.ErrorMessage = "Не удалось добавить происшествие";
			}
		}

		private bool CanCreate()
		{
			return
				!string.IsNullOrEmpty(_homeViewModel.NewIncident.IncidentType) &&
				!string.IsNullOrEmpty(_homeViewModel.NewIncident.ResolutionType) &&
				_homeViewModel.NewIncident.RegDate.Year != 1 &&
				_homeViewModel.NewIncident.Participants.Any();
		}
	}
}
