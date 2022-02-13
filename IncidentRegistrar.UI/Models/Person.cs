namespace IncidentRegistrar.UI.Models
{
	/// <summary>
	/// Лицо, участвовавшее в происшествии
	/// </summary>
	public class Person : DomainObject
	{
		/// <summary>
		/// Имя
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Фамилия
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// Отчество
		/// </summary>
		public string MiddleName { get; set; }

		/// <summary>
		/// Адрес
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		/// Количество судимостей
		/// </summary>
		public int ConvictionsCount { get; set; }
	}
}
