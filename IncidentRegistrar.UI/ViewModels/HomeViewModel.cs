using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace IncidentRegistrar.UI.ViewModels
{
	public class HomeViewModel : ViewModelBase
	{
		#region AllIncidents

		private ObservableCollection<IncidentViewModel> _incidents;
		public ObservableCollection<IncidentViewModel> Incidents
		{
			get { return _incidents; }
			set
			{
				_incidents = value;
				OnPropertyChanged(nameof(Incidents));
			}
		}

		#endregion

		#region Filter Property

		private string _filter;
		public string Filter
		{
			get
			{
				return _filter;
			}
			set
			{
				_filter = value;
				OnPropertyChanged(nameof(Filter));
			}
		}

		#endregion

		public ParticipantViewModel CurrentParticipant { get; set; } = new ParticipantViewModel();

		public NewIncidentViewModel NewIncident { get; }

		#region Commands

		public ICommand LoadIncidentsCommand { get; }
		public ICommand AddParticipantCommand { get; }

		#endregion

		#region Error

		public MessageViewModel ErrorMessageViewModel { get; }

		public string ErrorMessage
		{
			set => ErrorMessageViewModel.Message = value;
		}

		#endregion

		public HomeViewModel(IIncidentRepository incidentRepository)
		{
			#region ViewModels

			ErrorMessageViewModel = new MessageViewModel();
			NewIncident = new NewIncidentViewModel(this, incidentRepository);

			#endregion

			LoadIncidentsCommand = new LoadIncidentsCommand(this, incidentRepository);
			LoadIncidentsCommand.Execute(null);
			AddParticipantCommand = new AddParticipantCommand(this);
		}
	}
}
