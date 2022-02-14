using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.Commands
{
	public class DeleteParticipantCommand : ICommand
	{
		private readonly ICurrentIncidentStore _currentIncidentStore;

		public DeleteParticipantCommand(ICurrentIncidentStore currentIncidentStore)
		{
			_currentIncidentStore = currentIncidentStore;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			try
			{
				var id = int.Parse(parameter.ToString());
				_currentIncidentStore.RemoveParticipant(id);
			}
			catch
			{
				MessageBox.Show("Не удалось удалить участника");
			}
		}
	}
}
