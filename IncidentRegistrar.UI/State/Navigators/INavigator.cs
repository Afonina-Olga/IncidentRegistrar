using System;

using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.State
{
	public enum ViewType
	{
		Login,
		Home,
		Create,
		Read,
		Edit,
		Summary
	}

	public interface INavigator
	{
		ViewModelBase CurrentViewModel { get; set; }
		event Action StateChanged;
	}
}
