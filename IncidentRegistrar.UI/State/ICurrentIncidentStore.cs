using System;
using System.Collections.Generic;

using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.State
{
	public interface ICurrentIncidentStore
	{
		int Id { get; set; }

		DateTime RegDate { get; set; }

		string IncidentType { get; set; }

		string ResolutionType { get; set; }

		List<ParticipantViewModel> Participants { get; set; }

		void RemoveParticipant(int id);

		void AddParticipant(ParticipantViewModel participant);

		event Action<ParticipantViewModel> ParticipantRemoved;

		event Action<ParticipantViewModel> ParticipantAdded;
	}
}
