using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels.Factories;

namespace IncidentRegistrar.UI.ViewModels
{
	/// <summary>
	/// Для отображения в таблице
	/// </summary>
	public class IncidentViewModel : ViewModelBase
	{
		public int Id { get; set; }

		public DateTime RegDate { get; set; }

		public string IncidentType { get; set; }

		public string ResolutionType { get; set; }

		public List<ParticipantViewModel> Participants { get; set; }

		public string ParticipantsListing { get; set; }

		public ICommand DeleteIncidentCommand { get; }

		public ICommand ReadIncidentCommand { get; }

		public ICommand UpdateCurrentViewModelCommand { get; }

		public IncidentViewModel(
			IIncidentStore incidentStore,
			ICurrentIncidentStore currentIncidentStore,
			IIncidentRepository incidentRepository,
			INavigator navigator,
			IViewModelFactory viewModelFactory)
		{
			DeleteIncidentCommand = new DeleteIncidentCommand(incidentStore, incidentRepository);
			UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);
			ReadIncidentCommand = new ReadIncidentCommand(this, navigator, viewModelFactory, currentIncidentStore);
		}
	}
}
