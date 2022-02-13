using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.State
{
	public class ViewModelRenavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
	{
		private readonly INavigator _navigator;
		private readonly CreateViewModel<TViewModel> _createViewModel;

		public ViewModelRenavigator(INavigator navigator, CreateViewModel<TViewModel> createViewModel)
		{
			_navigator = navigator;
			_createViewModel = createViewModel;
		}

		public void Renavigate()
		{
			_navigator.CurrentViewModel = _createViewModel();
		}
	}
}
