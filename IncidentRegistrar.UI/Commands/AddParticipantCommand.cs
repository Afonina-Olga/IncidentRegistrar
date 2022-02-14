using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Commands
{
	public class AddParticipantCommand : ICommand
	{
		private readonly CreateIncidentViewModel _viewModel;
		private readonly ICurrentIncidentStore _currentIncidentStore;

		public AddParticipantCommand(CreateIncidentViewModel viewModel)
		{
			_viewModel = viewModel;
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
				if (!CanAdd())
					MessageBox.Show("Заполните обязательные поля");

				else if (ParticipantExists())
					MessageBox.Show("Нельзя добавить участника дважды");
				else
					_viewModel.Participants.Add(
						new ParticipantViewModel(_currentIncidentStore)
						{
							Address = _viewModel.CurrentParticipant.Address,
							LastName = _viewModel.CurrentParticipant.LastName,
							FirstName = _viewModel.CurrentParticipant.FirstName,
							MiddleName = _viewModel.CurrentParticipant.MiddleName,
							PersonType = _viewModel.CurrentParticipant.SelectedPersonType
						});
			}
			catch
			{
				MessageBox.Show("Не удалось добавить участника");
			}
		}

		/// <summary>
		/// Все данные введены корректно => можно добавить участника
		/// </summary>
		public bool CanAdd()
		{
			return
				!string.IsNullOrEmpty(_viewModel.CurrentParticipant.LastName) &&
				!string.IsNullOrEmpty(_viewModel.CurrentParticipant.FirstName) &&
				!string.IsNullOrEmpty(_viewModel.CurrentParticipant.MiddleName) &&
				!string.IsNullOrEmpty(_viewModel.CurrentParticipant.Address) &&
				!string.IsNullOrEmpty(_viewModel.CurrentParticipant.SelectedPersonType);
		}

		/// <summary>
		/// Проверка, есть ли среди участников человек с такими данными (один человек может участвовать в происшествии только в одном статусе
		/// </summary>
		public bool ParticipantExists()
		{
			return
				_viewModel.Participants.Any(
					participant => participant.LastName == _viewModel.CurrentParticipant.LastName &&
					participant.FirstName == _viewModel.CurrentParticipant.FirstName &&
					participant.MiddleName == _viewModel.CurrentParticipant.MiddleName &&
					participant.Address == _viewModel.CurrentParticipant.Address);
		}
	}
}
