using System;
using System.Collections.Generic;
using System.Linq;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.UI.State
{
	public class IncidentStore : IIncidentStore
	{
		public event Action<Incident> IncidentAdded;

		public event Action<Incident> IncidentDeleted;

		public List<Incident> Incidents { get; set; } = new List<Incident>();

		public void AddIncident(Incident incident)
		{
			Incidents.Add(incident);
			IncidentAdded?.Invoke(incident);
		}

		public void DeleteIncident(Incident incident)
		{
			Incidents.Remove(incident);
			IncidentDeleted?.Invoke(incident);
		}

		public void DeleteIncident(int id)
		{
			var incidentToRemove = Incidents.FirstOrDefault(incident => incident.Id == id);
			IncidentDeleted?.Invoke(incidentToRemove);
		}
	}
}
