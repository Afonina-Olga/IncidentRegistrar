using System.Collections.ObjectModel;
using System.Windows.Input;
using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.ViewModels
{
	public class EditIncidentViewModel : CreateIncidentViewModel
	{
		private readonly ICurrentIncidentStore _currentIncidentStore;

		public int Id { get; set; }

		public ICommand UpdateIncidentCommand { get; }

		public EditIncidentViewModel(
			ICurrentIncidentStore currentIncidentStore,
			IIncidentStore incidentStore,
			IIncidentRepository incidentRepository,
			IRenavigator homeRenavigator)
			: base(currentIncidentStore, incidentStore, incidentRepository, homeRenavigator)
		{
			Id = currentIncidentStore.Id;
			RegDate = currentIncidentStore.RegDate;
			ResolutionType = currentIncidentStore.ResolutionType;
			IncidentType = currentIncidentStore.IncidentType;
			Participants = new ObservableCollection<ParticipantViewModel>(currentIncidentStore.Participants);

			_currentIncidentStore = currentIncidentStore;

			_currentIncidentStore.ParticipantAdded += OnParticipantAdded;
			_currentIncidentStore.ParticipantRemoved += OnParticipantRemoved;

			UpdateIncidentCommand = new UpdateIncidentCommand(this, incidentRepository, incidentStore, homeRenavigator);
		}

		private void OnParticipantAdded(ParticipantViewModel participant)
		{
			//Participants.Add(participant);
		}

		public override void Dispose()
		{
			_currentIncidentStore.ParticipantRemoved -= OnParticipantRemoved;
			_currentIncidentStore.ParticipantAdded -= OnParticipantAdded;
			base.Dispose();
		}

		private void OnParticipantRemoved(ParticipantViewModel participant)
		{
			Participants.Remove(participant);
		}
	}
}
