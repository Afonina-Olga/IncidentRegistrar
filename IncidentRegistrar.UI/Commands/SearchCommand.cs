using System;
using System.Threading.Tasks;
using System.Windows;

using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.Commands
{
	public class SearchCommand : AsyncCommandBase
	{
		private readonly IIncidentStore _incidentStore;
		private readonly IIncidentRepository _incidentRepository;

		public SearchCommand(IIncidentStore incidentStore, IIncidentRepository incidentRepository)
		{
			_incidentStore = incidentStore;
			_incidentRepository = incidentRepository;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				var filter = parameter.ToString();
				var incidents = await _incidentRepository.GetByParticipantLastName(filter);
				_incidentStore.Incidents = incidents;
			}
			catch(Exception ex)
			{
				MessageBox.Show("Не удалось выполнить поиск");
			}
		}
	}
}
