using System;
using System.Collections.Generic;
using System.Windows.Input;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.ViewModels
{
	public class ReadIncidentViewModel : ViewModelBase
	{
		public int Id { get; set; }

		public DateTime RegDate { get; set; }

		public string IncidentType { get; set; }

		public string ResolutionType { get; set; }

		public List<ParticipantViewModel> Participants { get; set; }

		public ICommand RenavigateHomeViewCommand { get; }

		public ReadIncidentViewModel(IRenavigator homeRenavigator, ICurrentIncidentStore currentIncidentStore)
		{
			Id = currentIncidentStore.Id;
			RegDate = currentIncidentStore.RegDate;
			IncidentType = currentIncidentStore.IncidentType;
			ResolutionType = currentIncidentStore.ResolutionType;
			Participants = currentIncidentStore.Participants;

			RenavigateHomeViewCommand = new RenavigateCommand(homeRenavigator);
		}
	}
}
