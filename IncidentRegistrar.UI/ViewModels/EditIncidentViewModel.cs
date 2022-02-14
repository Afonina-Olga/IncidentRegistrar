using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;
using System.Collections.ObjectModel;

namespace IncidentRegistrar.UI.ViewModels
{
	public class EditIncidentViewModel : CreateIncidentViewModel
	{
		public EditIncidentViewModel(
			ICurrentIncidentStore currentIncidentStore,
			IIncidentStore incidentStore,
			IIncidentRepository incidentRepository,
			IRenavigator homeRenavigator)
			: base(currentIncidentStore, incidentStore, incidentRepository, homeRenavigator)
		{
			RegDate = currentIncidentStore.RegDate;
			ResolutionType = currentIncidentStore.ResolutionType;
			IncidentType = currentIncidentStore.IncidentType;
			Participants = new ObservableCollection<ParticipantViewModel>(currentIncidentStore.Participants);
		}
	}
}
