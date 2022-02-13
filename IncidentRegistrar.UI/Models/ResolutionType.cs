namespace IncidentRegistrar.UI.Models
{
	public enum ResolutionType
	{
		// Отказано в возбуждении дел
		Refused,

		// Удовлетворено ходатайство о возбуждении уголовного дела с указанием регистрационный номера заведенного дела
		Initiated,

		// Отправлено по территориальному признаку
		Redirected
	}
}
