using System.ComponentModel.DataAnnotations;

namespace IncidentRegistrar.UI.Models
{
	/// <summary>
	/// Пользователь
	/// </summary>
	public class User : DomainObject
	{
		[Required]
		public string Login { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
