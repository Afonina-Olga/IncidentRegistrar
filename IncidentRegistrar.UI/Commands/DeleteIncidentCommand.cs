using System.Linq;
using System.Threading.Tasks;

using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Commands
{
	public class DeleteIncidentCommand : AsyncCommandBase
	{
		private readonly MainViewModel _mainViewModel;
		private readonly IIncidentRepository _incidentRepository;

		public DeleteIncidentCommand(MainViewModel mainViewModel, IIncidentRepository incidentRepository)
		{
			_mainViewModel = mainViewModel;
			_incidentRepository = incidentRepository;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				var id = int.Parse(parameter.ToString());
				await _incidentRepository.Delete(id);

				var incidentToRemove = _mainViewModel.Incidents.FirstOrDefault(incident => incident.Id == id);
				_mainViewModel.Incidents.Remove(incidentToRemove);
			}
			catch
			{
				_mainViewModel.ErrorMessage = "Не удалось удалить происшествие";
			}
		}
	}
}
