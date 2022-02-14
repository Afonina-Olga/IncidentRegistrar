using System;
using System.Windows.Input;

using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.Commands
{
	public class HomeRanavigateCommand : ICommand
	{
		private readonly IRenavigator _renavigator;

		public HomeRanavigateCommand(IRenavigator renavigator)
		{
			_renavigator = renavigator;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			_renavigator.Renavigate();
		}
	}
}
