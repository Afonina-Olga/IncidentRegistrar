using System;
using System.Windows.Input;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.ViewModels
{
	/// <summary>
	/// Для отображения в таблице
	/// </summary>
	public class IncidentViewModel : ViewModelBase
	{
		public int Id { get; set; }

		public DateTime RegDate { get; set; }

		public string IncidentType { get; set; }

		public string ResolutionType { get; set; }

		public string Participants { get; set; }

		public ICommand DeleteIncidentCommand { get; }

		public IncidentViewModel(IIncidentStore incidentStore, IIncidentRepository incidentRepository)
		{
			DeleteIncidentCommand = new DeleteIncidentCommand(incidentStore, incidentRepository);
		}
	}
}
