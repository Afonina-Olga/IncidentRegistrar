using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Commands
{
	public class AddParticipantCommand : ICommand
	{
		private readonly MainViewModel _mainViewModel;

		public AddParticipantCommand(MainViewModel mainViewModel)
		{
			_mainViewModel = mainViewModel;
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
					_mainViewModel.NewIncident.Participants.Add(
						new ParticipantViewModel()
						{
							Address = _mainViewModel.CurrentParticipant.Address,
							LastName = _mainViewModel.CurrentParticipant.LastName,
							FirstName = _mainViewModel.CurrentParticipant.FirstName,
							MiddleName = _mainViewModel.CurrentParticipant.MiddleName,
							PersonType = _mainViewModel.CurrentParticipant.SelectedPersonType
						});
			}
			catch
			{
				_mainViewModel.ErrorMessage = "Не удалось добавить участника";
			}
		}

		/// <summary>
		/// Все данные введены корректно => можно добавить участника
		/// </summary>
		public bool CanAdd()
		{
			return
				!string.IsNullOrEmpty(_mainViewModel.CurrentParticipant.LastName) &&
				!string.IsNullOrEmpty(_mainViewModel.CurrentParticipant.FirstName) &&
				!string.IsNullOrEmpty(_mainViewModel.CurrentParticipant.MiddleName) &&
				!string.IsNullOrEmpty(_mainViewModel.CurrentParticipant.Address) &&
				!string.IsNullOrEmpty(_mainViewModel.CurrentParticipant.SelectedPersonType);
		}

		/// <summary>
		/// Проверка, есть ли среди участников человек с такими данными (один человек может участвовать в происшествии только в одном статусе
		/// </summary>
		public bool ParticipantExists()
		{
			return 
				_mainViewModel.NewIncident.Participants.Any(
					participant => participant.LastName == _mainViewModel.CurrentParticipant.LastName &&
					participant.FirstName == _mainViewModel.CurrentParticipant.FirstName &&
					participant.MiddleName == _mainViewModel.CurrentParticipant.MiddleName &&
					participant.Address == _mainViewModel.CurrentParticipant.Address);
		}
	}
}
