using System;

using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.ViewModels.Factories
{
	public class ViewModelFactory : IViewModelFactory
	{
		private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
		private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
		private readonly CreateViewModel<CreateIncidentViewModel> _createIncidentViewModel;
		private readonly CreateViewModel<ReadIncidentViewModel> _createReadIncidentViewModel;
		private readonly CreateViewModel<EditIncidentViewModel> _createEditIncidentViewModel;

		public ViewModelFactory(
			CreateViewModel<LoginViewModel> createLoginViewModel,
			CreateViewModel<HomeViewModel> createHomeViewModel,
			CreateViewModel<CreateIncidentViewModel> createIncidentViewModel,
			CreateViewModel<ReadIncidentViewModel> createReadIncidentViewModel,
			CreateViewModel<EditIncidentViewModel> createEditIncidentViewModel)
		{
			_createLoginViewModel = createLoginViewModel;
			_createHomeViewModel = createHomeViewModel;
			_createIncidentViewModel = createIncidentViewModel;
			_createReadIncidentViewModel = createReadIncidentViewModel;
			_createEditIncidentViewModel = createEditIncidentViewModel;
		}

		public ViewModelBase CreateViewModel(ViewType type)
		{
			return type switch
			{
				ViewType.Login => _createLoginViewModel(),
				ViewType.Home => _createHomeViewModel(),
				ViewType.Create => _createIncidentViewModel(),
				ViewType.Read => _createReadIncidentViewModel(),
				ViewType.Edit => _createEditIncidentViewModel(),
				_ => throw new ArgumentException("Неизвестный тип View", "viewType")
			};
		}
	}
}
