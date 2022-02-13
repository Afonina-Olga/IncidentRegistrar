using System.Windows.Input;
using System.Collections.ObjectModel;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels.Factories;

namespace IncidentRegistrar.UI.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly INavigator _navigator;

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

		#region IsLoggedIn

		private bool _IsLoggedIn;
		public bool IsLoggedIn
		{
			get { return _IsLoggedIn; }
			set
			{
				_IsLoggedIn = value;
				OnPropertyChanged(nameof(IsLoggedIn));
			}
		}

		#endregion

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

		public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

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
		public ICommand UpdateCurrentViewModelCommand { get; }

		#endregion

		public MainViewModel(IIncidentRepository incidentRepository, INavigator navigator, IViewModelFactory viewModelFactory)
		{
			_navigator = navigator;

			#region ViewModels

			ErrorMessageViewModel = new MessageViewModel();
			NewIncident = new NewIncidentViewModel(this, incidentRepository);

			#endregion

			#region Commands

			LoadIncidentsCommand = new LoadIncidentsCommand(this, incidentRepository);
			LoadIncidentsCommand.Execute(null);
			AddParticipantCommand = new AddParticipantCommand(this);
			UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);
			UpdateCurrentViewModelCommand.Execute(ViewType.Login);

			#endregion

			_navigator.StateChanged += Navigator_StateChanged;
		}

		private void Navigator_StateChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}

		public override void Dispose()
		{
			ErrorMessageViewModel.Dispose();
			base.Dispose();
		}
	}
}
