using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IncidentRegistrar.UI.Extentions;
using IncidentRegistrar.UI.Models;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Commands
{
	public class UpdateIncidentCommand : AsyncCommandBase
	{
		private readonly EditIncidentViewModel _viewModel;
		private readonly IIncidentRepository _incidentRepository;
		private readonly IIncidentStore _incidentStore;
		private readonly IRenavigator _homeRenavigator;

		public UpdateIncidentCommand(
			EditIncidentViewModel viewModel,
			IIncidentRepository incidentRepository,
			IIncidentStore incidentStore,
			IRenavigator homeRenavigator)
		{
			_viewModel = viewModel;
			_incidentRepository = incidentRepository;
			_incidentStore = incidentStore;
			_homeRenavigator = homeRenavigator;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				if (CanCreate())
				{
					var updatedIncident = await _incidentRepository.Update(_viewModel.Id, new Incident()
					{
						IncidentType = _viewModel.IncidentType.ToIncidentType(),
						RegDate = _viewModel.RegDate,
						ResolutionType = _viewModel.ResolutionType.ToResolutionType(),
						Participants = _viewModel.Participants.Select(participant => new Participant()
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

					_incidentStore.UpdateIncident(updatedIncident);

					_homeRenavigator.Renavigate();
				}
				else
					MessageBox.Show("Заполните все сведения об инциденте");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Не удалось добавить происшествие");
			}
		}

		private bool CanCreate()
		{
			return
				!string.IsNullOrEmpty(_viewModel.IncidentType) &&
				!string.IsNullOrEmpty(_viewModel.ResolutionType) &&
				_viewModel.RegDate.Year != 1 &&
				_viewModel.Participants.Any();
		}
	}
}
