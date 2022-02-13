using System;

using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.ViewModels.Factories
{
	public class ViewModelFactory : IViewModelFactory
	{
		private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
		private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;

		public ViewModelFactory(
			CreateViewModel<LoginViewModel> createLoginViewModel,
			CreateViewModel<HomeViewModel> createHomeViewModel)
		{
			_createLoginViewModel = createLoginViewModel;
			_createHomeViewModel = createHomeViewModel;
		}

		public ViewModelBase CreateViewModel(ViewType type)
		{
			return type switch
			{
				ViewType.Login => _createLoginViewModel(),
				ViewType.Home => _createHomeViewModel(),
				_ => throw new ArgumentException("Неизвестный тип View", "viewType")
			};
		}
	}
}
