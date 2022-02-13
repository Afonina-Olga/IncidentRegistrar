using System;
using System.Collections.Generic;

namespace IncidentRegistrar.UI.Models
{
	/// <summary>
	/// Сообщения о происшествиях
	/// </summary>
	public class Incident : DomainObject
	{
		/// <summary>
		/// Дата регистрации
		/// </summary>
		public DateTime RegDate { get; set; }

		/// <summary>
		/// Тип происшествия
		/// </summary>
		public IncidentType IncidentType { get; set; }

		/// <summary>
		/// Информация о принятом по происшествию решении
		/// </summary>
		public ResolutionType ResolutionType { get; set; }
		
		/// <summary>
		/// Лица, участвовавшие в инциденте
		/// </summary>
		public List<Participant> Participants { get; set; }
	}
}
