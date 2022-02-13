using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.ViewModels.Factories
{
	public interface IViewModelFactory
	{
		ViewModelBase CreateViewModel(ViewType viewType);
	}
}
