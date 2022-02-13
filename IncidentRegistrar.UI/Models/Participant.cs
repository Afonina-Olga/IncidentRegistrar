using System.Collections.Generic;

namespace IncidentRegistrar.UI.Models
{
	public class Participant : DomainObject
	{
		/// <summary>
		/// Персональные данные
		/// </summary>
		public Person Person { get; set; }

		/// <summary>
		/// Вид лица, участвовавшего в происшествии
		/// </summary>
		public PersonType PersonType { get; set; }

		/// <summary>
		/// Связанные с участником инциденты
		/// </summary>
		public List<Incident> Incidents { get; set; }
	}
}
