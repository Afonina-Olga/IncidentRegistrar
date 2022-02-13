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
		#region RegDate Property 

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

		#endregion

		#region IncidentType Property

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

		#endregion

		#region ResolutionType Property

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

		#endregion

		#region Participants Property

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

		#endregion

		public List<string> IncidentTypes { get; set; } = new List<string>() { "Ограбление", "Несчастный случай", "Драка", "Мелкое хулиганство", "Другое" };

		public List<string> ResolutionTypes { get; set; } = new List<string>() { "Отказано", "Удовлетворено", "Перенаправлено" };

		public ICommand CreateIncidentCommand { get; }

		public NewIncidentViewModel(HomeViewModel homeViewModel, IIncidentRepository incidentRepository)
		{
			CreateIncidentCommand = new CreateIncidentCommand(homeViewModel, incidentRepository);
			RegDate = DateTime.Now;
		}
	}
}
