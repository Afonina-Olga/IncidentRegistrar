using System;
using System.Collections.Generic;
using System.Linq;
using IncidentRegistrar.UI.Models;
using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Extentions
{
	public static class Extentions
	{
		public static string FromIncidentType(this IncidentType type)
		{
			return type switch
			{
				IncidentType.Accident => "Несчастный случай",
				IncidentType.Fight => "Драка",
				IncidentType.Hooliganism => "Мелкое хулиганство",
				IncidentType.Robbery => "Ограбление",
				IncidentType.Unknown => "Другое",
				_ => throw new ArgumentException("Неизвестный тип происшествия")
			};
		}

		public static string FromResolutionType(this ResolutionType type)
		{
			return type switch
			{
				ResolutionType.Refused => "Отказано",
				ResolutionType.Initiated => "Удовлетворено",
				ResolutionType.Redirected => "Отправлено",
				_ => throw new ArgumentException("Неизвестный тип резолюции")
			};
		}

		public static IncidentType ToIncidentType(this string type)
		{
			return type switch
			{
				"Ограбление" => IncidentType.Robbery,
				"Несчастный случай" => IncidentType.Accident,
				"Драка" => IncidentType.Fight,
				"Мелкое хулиганство" => IncidentType.Hooliganism,
				"Другое" => IncidentType.Unknown,
				_ => throw new ArgumentException("Неизвестный тип происшествия")
			};
		}

		public static ResolutionType ToResolutionType(this string type)
		{
			return type switch
			{
				"Отказано" => ResolutionType.Refused,
				"Удовлетворено" => ResolutionType.Initiated,
				"Перенаправлено" => ResolutionType.Redirected,
				_ => throw new ArgumentException("Неизвестный тип резолюции")
			};
		}

		public static PersonType ToPersonType(this string type)
		{
			return type switch
			{
				"Потерпевший" => PersonType.Victim,
				"Виновник" => PersonType.Culprit,
				"Подозреваемый" => PersonType.Suspect,
				"Свидетель" => PersonType.Witness,
				_ => throw new ArgumentException("Неизвестный тип участника")
			};
		}

		public static string FromPersonType(this PersonType type)
		{
			return type switch
			{
				PersonType.Victim => "Потерпевший",
				PersonType.Culprit => "Виновник",
				PersonType.Suspect => "Подозреваемый",
				PersonType.Witness => "Свидетель",
				_ => throw new ArgumentException("Неизвестный тип участника")
			};
		}

		public static string ToPersonString(this List<Participant> participants)
		{
			var names = participants.Select(participant =>
			{
				return $"{participant.Person.LastName} {participant.Person.FirstName} {participant.Person.MiddleName}";
			});

			return string.Join(Environment.NewLine, names);
		}
	}
}