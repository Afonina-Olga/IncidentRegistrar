using System.Windows.Input;
using System.Collections.ObjectModel;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.Repositories;

namespace IncidentRegistrar.UI.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
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

		public ParticipantViewModel CurrentParticipant { get; set; } = new ParticipantViewModel();

		public NewIncidentViewModel NewIncident { get; }

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

		#region Error

		public MessageViewModel ErrorMessageViewModel { get; }

		public string ErrorMessage
		{
			set => ErrorMessageViewModel.Message = value;
		}

		#endregion

		#region Commands

		public ICommand LoadIncidentsCommand { get; }
		public ICommand AddParticipantCommand { get; }

		#endregion

		public MainViewModel(IIncidentRepository incidentRepository)
		{
			ErrorMessageViewModel = new MessageViewModel();
			LoadIncidentsCommand = new LoadIncidentsCommand(this, incidentRepository);
			LoadIncidentsCommand.Execute(null);
			AddParticipantCommand = new AddParticipantCommand(this);
			NewIncident = new NewIncidentViewModel(this, incidentRepository);

		}

		public override void Dispose()
		{
			ErrorMessageViewModel.Dispose();
			base.Dispose();
		}
	}
}
