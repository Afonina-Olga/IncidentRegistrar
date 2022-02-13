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
		private readonly MainViewModel _mainViewModel;
		private readonly IIncidentRepository _incidentRepository;

		public CreateIncidentCommand(MainViewModel mainViewModel, IIncidentRepository incidentRepository)
		{
			_mainViewModel = mainViewModel;
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
						IncidentType = _mainViewModel.NewIncident.IncidentType.ToIncidentType(),
						RegDate = _mainViewModel.NewIncident.RegDate,
						ResolutionType = _mainViewModel.NewIncident.ResolutionType.ToResolutionType(),
						Participants = _mainViewModel.NewIncident.Participants.Select(participant => new Participant()
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

					_mainViewModel.Incidents.Add(new IncidentViewModel(_mainViewModel, _incidentRepository)
					{
						IncidentType = _mainViewModel.NewIncident.IncidentType,
						Id = createdIncident.Id,
						RegDate = _mainViewModel.NewIncident.RegDate,
						ResolutionType = _mainViewModel.NewIncident.ResolutionType,
						Participants = string.Join(
							Environment.NewLine,
							_mainViewModel.NewIncident.Participants.Select(p => $"{p.LastName} {p.FirstName} {p.MiddleName}"))
					});
				}
				else
					MessageBox.Show("Заполните все сведения об инциденте");
			}
			catch (Exception ex)
			{
				_mainViewModel.ErrorMessage = "Не удалось добавить происшествие";
			}
		}

		private bool CanCreate()
		{
			return
				!string.IsNullOrEmpty(_mainViewModel.NewIncident.IncidentType) &&
				!string.IsNullOrEmpty(_mainViewModel.NewIncident.ResolutionType) &&
				_mainViewModel.NewIncident.RegDate.Year != 1 &&
				_mainViewModel.NewIncident.Participants.Any();
		}
	}
}
