using System;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.UI.State
{
	public interface IUserStore
	{
		event Action StateChanged;

		User User { get; set; }
	}
}
