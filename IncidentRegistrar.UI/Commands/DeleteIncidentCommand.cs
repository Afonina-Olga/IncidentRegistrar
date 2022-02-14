using System.Threading.Tasks;
using System.Windows;

using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.Commands
{
	public class DeleteIncidentCommand : AsyncCommandBase
	{
		private readonly IIncidentStore _incidentStore;
		private readonly IIncidentRepository _incidentRepository;

		public DeleteIncidentCommand(IIncidentStore incidentStore, IIncidentRepository incidentRepository)
		{
			_incidentStore = incidentStore;
			_incidentRepository = incidentRepository;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				var id = int.Parse(parameter.ToString());
				await _incidentRepository.Delete(id);

				_incidentStore.DeleteIncident(id);
				
			}
			catch
			{
				MessageBox.Show("Не удалось удалить происшествие");
			}
		}
	}
}
