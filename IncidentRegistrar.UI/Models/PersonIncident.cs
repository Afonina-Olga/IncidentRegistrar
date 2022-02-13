namespace IncidentRegistrar.UI.Models
{
	public class ParticipantIncident
	{
		public int ParticipantId { get; set; }
		public Participant Participant { get; set; }

		public int IncidentId { get; set; }
		public Incident Incident { get; set; }
	}
}
