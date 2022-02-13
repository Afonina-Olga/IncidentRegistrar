using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using IncidentRegistrar.UI.ViewModels;

namespace IncidentRegistrar.UI.Commands
{
	public class AddParticipantCommand : ICommand
	{
		private readonly HomeViewModel _homeViewModel;

		public AddParticipantCommand(HomeViewModel homeViewModel)
		{
			_homeViewModel = homeViewModel;
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
					_homeViewModel.NewIncident.Participants.Add(
						new ParticipantViewModel()
						{
							Address = _homeViewModel.CurrentParticipant.Address,
							LastName = _homeViewModel.CurrentParticipant.LastName,
							FirstName = _homeViewModel.CurrentParticipant.FirstName,
							MiddleName = _homeViewModel.CurrentParticipant.MiddleName,
							PersonType = _homeViewModel.CurrentParticipant.SelectedPersonType
						});
			}
			catch
			{
				_homeViewModel.ErrorMessage = "Не удалось добавить участника";
			}
		}

		/// <summary>
		/// Все данные введены корректно => можно добавить участника
		/// </summary>
		public bool CanAdd()
		{
			return
				!string.IsNullOrEmpty(_homeViewModel.CurrentParticipant.LastName) &&
				!string.IsNullOrEmpty(_homeViewModel.CurrentParticipant.FirstName) &&
				!string.IsNullOrEmpty(_homeViewModel.CurrentParticipant.MiddleName) &&
				!string.IsNullOrEmpty(_homeViewModel.CurrentParticipant.Address) &&
				!string.IsNullOrEmpty(_homeViewModel.CurrentParticipant.SelectedPersonType);
		}

		/// <summary>
		/// Проверка, есть ли среди участников человек с такими данными (один человек может участвовать в происшествии только в одном статусе
		/// </summary>
		public bool ParticipantExists()
		{
			return 
				_homeViewModel.NewIncident.Participants.Any(
					participant => participant.LastName == _homeViewModel.CurrentParticipant.LastName &&
					participant.FirstName == _homeViewModel.CurrentParticipant.FirstName &&
					participant.MiddleName == _homeViewModel.CurrentParticipant.MiddleName &&
					participant.Address == _homeViewModel.CurrentParticipant.Address);
		}
	}
}
