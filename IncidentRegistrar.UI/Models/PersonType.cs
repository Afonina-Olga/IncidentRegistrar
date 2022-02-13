namespace IncidentRegistrar.UI.Models
{
	/// <summary>
	/// Тип лица, участвовавшего в происшествии
	/// </summary>
	public enum PersonType
	{
		// Потерпевший
		Victim,
		
		// Виновник
		Culprit,

		// Подозреваемый
		Suspect,

		// Свидетель
		Witness
	}
}
