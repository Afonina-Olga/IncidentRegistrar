using System;
using System.Collections.Generic;

using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.State
{
	public class CurrentIncidentStore : ICurrentIncidentStore
	{
		public int Id { get; set; }

		public DateTime RegDate { get; set; }

		public string IncidentType { get; set; }

		public string ResolutionType { get; set; }

		public List<ParticipantViewModel> Participants { get; set; }
	}
}
