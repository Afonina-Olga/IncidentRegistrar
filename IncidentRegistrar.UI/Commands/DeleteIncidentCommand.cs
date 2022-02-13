using System.Linq;
using System.Threading.Tasks;

using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Commands
{
	public class DeleteIncidentCommand : AsyncCommandBase
	{
		private readonly HomeViewModel _homeViewModel;
		private readonly IIncidentRepository _incidentRepository;

		public DeleteIncidentCommand(HomeViewModel homeViewModel, IIncidentRepository incidentRepository)
		{
			_homeViewModel = homeViewModel;
			_incidentRepository = incidentRepository;
		}

		public override async Task ExecuteAsync(object parameter)
		{
			try
			{
				var id = int.Parse(parameter.ToString());
				await _incidentRepository.Delete(id);

				var incidentToRemove = _homeViewModel.Incidents.FirstOrDefault(incident => incident.Id == id);
				_homeViewModel.Incidents.Remove(incidentToRemove);
			}
			catch
			{
				_homeViewModel.ErrorMessage = "Не удалось удалить происшествие";
			}
		}
	}
}
