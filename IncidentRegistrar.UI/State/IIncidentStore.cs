using System;
using System.Collections.Generic;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.UI.State
{
	public interface IIncidentStore
	{
		event Action<Incident> IncidentAdded;

		event Action<Incident> IncidentDeleted;

		List<Incident> Incidents { get; set; }

		void AddIncident(Incident incident);

		void DeleteIncident(Incident incident);

		void DeleteIncident(int id);
	}
}
