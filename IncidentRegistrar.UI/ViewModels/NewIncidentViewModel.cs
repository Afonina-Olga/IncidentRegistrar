using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.Repositories;

namespace IncidentRegistrar.UI.ViewModels
{
	public class NewIncidentViewModel : ViewModelBase
	{
		private DateTime _regDate;
		public DateTime RegDate
		{
			get { return _regDate; }
			set
			{
				_regDate = value;
				OnPropertyChanged(nameof(RegDate));
			}
		}

		private string _incidentType;
		public string IncidentType
		{
			get { return _incidentType; }
			set
			{
				_incidentType = value;
				OnPropertyChanged(nameof(IncidentType));
			}
		}

		private string _resolutionType;
		public string ResolutionType
		{
			get { return _resolutionType; }
			set
			{
				_resolutionType = value;
				OnPropertyChanged(nameof(ResolutionType));
			}
		}

		private ObservableCollection<ParticipantViewModel> _participants = new ObservableCollection<ParticipantViewModel>();

		public ObservableCollection<ParticipantViewModel> Participants
		{
			get { return _participants; }
			set
			{
				_participants = value;
				OnPropertyChanged(nameof(Participants));
			}
		}

		public List<string> IncidentTypes { get; set; } = new List<string>() { "Ограбление", "Несчастный случай", "Драка", "Мелкое хулиганство", "Другое" };

		public List<string> ResolutionTypes { get; set; } = new List<string>() { "Отказано", "Удовлетворено", "Перенаправлено" };

		public ICommand CreateIncidentCommand { get; }

		public NewIncidentViewModel(MainViewModel mainViewModel, IIncidentRepository incidentRepository)
		{
			CreateIncidentCommand = new CreateIncidentCommand(mainViewModel, incidentRepository);
			RegDate = DateTime.Now;
		}
	}
}
