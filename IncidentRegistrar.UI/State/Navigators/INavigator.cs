using System;

using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.State
{
	public enum ViewType
	{
		Login,
		Home,
		Edit,
		Create,
		Read,
		Summary
	}

	public interface INavigator
	{
		ViewModelBase CurrentViewModel { get; set; }
		event Action StateChanged;
	}
}
