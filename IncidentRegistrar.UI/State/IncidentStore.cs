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

		public event Action<List<Incident>> IncidentsLoaded;

		private List<Incident> _incidents = new List<Incident>();
		public List<Incident> Incidents
		{
			get { return _incidents; }
			set
			{
				_incidents = value;
				IncidentsLoaded?.Invoke(value);
			}
		}

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
