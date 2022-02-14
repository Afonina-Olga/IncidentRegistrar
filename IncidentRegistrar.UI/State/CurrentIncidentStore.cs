using System;
using System.Collections.Generic;
using System.Linq;

using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.State
{
	public class CurrentIncidentStore : ICurrentIncidentStore
	{
		public int Id { get; set; }

		public DateTime RegDate { get; set; }

		public string IncidentType { get; set; }

		public string ResolutionType { get; set; }

		public List<ParticipantViewModel> Participants { get; set; } = new List<ParticipantViewModel>();

		public event Action<ParticipantViewModel> ParticipantRemoved;

		public event Action<ParticipantViewModel> ParticipantAdded;

		public void AddParticipant(ParticipantViewModel participant)
		{
			Participants.Add(participant);
			ParticipantAdded?.Invoke(participant);
		}

		public void RemoveParticipant(int id)
		{
			var itemToRemove = Participants.FirstOrDefault(participant => participant.Id == id);
			if (itemToRemove != null)
			{
				Participants.Remove(itemToRemove);
				ParticipantRemoved?.Invoke(itemToRemove);
			}
		}
	}
}
