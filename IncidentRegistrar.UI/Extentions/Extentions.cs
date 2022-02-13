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
				ResolutionType.Refused => "Отказано в возбуждении дела",
				ResolutionType.Initiated => "Удовлетворено ходатайство",
				ResolutionType.Redirected => "Отправлено по территориальному признаку",
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

		public static string ToPersonString(this List<Participant> participants)
		{
			var res = participants.Select(participant =>
			{
				if (participant != null && participant.Person != null)
				{
					return $"{participant.Person.LastName} {participant.Person.FirstName} {participant.Person.MiddleName}";
				}
				else
					return "";
			}).ToList();

			return string.Join(Environment.NewLine, res);
		}
	}
}