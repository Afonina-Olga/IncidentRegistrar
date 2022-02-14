using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;

namespace IncidentRegistrar.UI.ViewModels
{
	public class CreateIncidentViewModel : ViewModelBase
	{
		private readonly ICurrentIncidentStore _currentIncidentStore;

		public List<string> IncidentTypes { get; set; } = new List<string>() { "Ограбление", "Несчастный случай", "Драка", "Мелкое хулиганство", "Другое" };

		public List<string> ResolutionTypes { get; set; } = new List<string>() { "Отказано", "Удовлетворено", "Перенаправлено" };

		public ParticipantViewModel CurrentParticipant { get; set; }

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

		#region Commands

		public ICommand RenavigateHomeViewCommand { get; }

		public ICommand AddParticipantCommand { get; }

		public ICommand CreateIncidentCommand { get; }

		#endregion

		public CreateIncidentViewModel(
			ICurrentIncidentStore currentIncidentStore,
			IIncidentStore incidentStore,
			IIncidentRepository incidentRepository,
			IRenavigator homeRenavigator)
		{
			CurrentParticipant = new ParticipantViewModel(currentIncidentStore);
			RenavigateHomeViewCommand = new RenavigateCommand(homeRenavigator);
			CreateIncidentCommand = new CreateIncidentCommand(this, incidentRepository, incidentStore, homeRenavigator);
			AddParticipantCommand = new AddParticipantCommand(this, currentIncidentStore);
			RegDate = DateTime.Now;

			_currentIncidentStore = currentIncidentStore;

			_currentIncidentStore.ParticipantAdded += OnParticipantAdded;
			_currentIncidentStore.ParticipantRemoved += OnParticipantRemoved;
		}

		public override void Dispose()
		{
			_currentIncidentStore.ParticipantAdded -= OnParticipantAdded;
			_currentIncidentStore.ParticipantRemoved -= OnParticipantRemoved;
			base.Dispose();
		}

		private void OnParticipantAdded(ParticipantViewModel participant)
		{
			Participants.Add(participant);
		}

		private void OnParticipantRemoved(ParticipantViewModel participant)
		{
			Participants.Remove(participant);
		}
	}
}
