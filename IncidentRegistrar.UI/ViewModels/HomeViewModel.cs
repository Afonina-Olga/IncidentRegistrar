using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

using IncidentRegistrar.UI.Commands;
using IncidentRegistrar.UI.Extentions;
using IncidentRegistrar.UI.Models;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels.Factories;

namespace IncidentRegistrar.UI.ViewModels
{
	public class HomeViewModel : ViewModelBase
	{
		private readonly IIncidentStore _incidentStore;
		private readonly ICurrentIncidentStore _currentIncidentStore;
		private readonly IIncidentRepository _incidentRepository;
		private readonly INavigator _navigator;
		private readonly IViewModelFactory _viewModelFactory;

		#region Incidents Property

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

		#region Commands

		public ICommand LoadIncidentsCommand { get; }

		public ICommand UpdateCurrentViewModelCommand { get; }

		public ICommand SearchCommand { get; }

		#endregion

		public HomeViewModel(
			IIncidentRepository incidentRepository,
			INavigator navigator,
			IViewModelFactory viewModelFactory,
			IIncidentStore incidentStore,
			ICurrentIncidentStore currentIncidentStore)
		{
			_incidentStore = incidentStore;
			_currentIncidentStore = currentIncidentStore;
			_incidentRepository = incidentRepository;
			_navigator = navigator;
			_viewModelFactory = viewModelFactory;

			_incidentStore.IncidentDeleted += OnIncidentDeleted;
			_incidentStore.IncidentAdded += OnIncidentAdded;
			_incidentStore.IncidentsLoaded += OnIncidentsLoaded;
			_incidentStore.IncidentUpdated += OnIncidentUpdated;

			LoadIncidentsCommand = new LoadIncidentsCommand(this, incidentRepository, incidentStore, navigator, viewModelFactory);
			LoadIncidentsCommand.Execute(null);

			UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);

			SearchCommand = new SearchCommand(incidentStore, incidentRepository);

			PropertyChanged += OnPropertyChanged;
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(Filter))
			{
				SearchCommand.Execute(Filter);
			}
		}

		private void OnIncidentUpdated(Incident incident)
		{
			var currentIncident = Incidents.FirstOrDefault(i => i.Id == incident.Id);
			currentIncident.IncidentType = incident.IncidentType.FromIncidentType();
			currentIncident.ResolutionType = incident.ResolutionType.FromResolutionType();
			currentIncident.RegDate = incident.RegDate;
			currentIncident.Participants = incident.Participants.Select(participant => ToParticipantViewModel(participant)).ToList();
			currentIncident.ParticipantsListing = incident.Participants.ToPersonString();
		}

		public override void Dispose()
		{
			_incidentStore.IncidentDeleted -= OnIncidentDeleted;
			_incidentStore.IncidentAdded -= OnIncidentAdded;
			_incidentStore.IncidentsLoaded -= OnIncidentsLoaded;
			_incidentStore.IncidentUpdated -= OnIncidentUpdated;
			base.Dispose();
		}

		private void OnIncidentAdded(Incident incident)
		{
			Incidents.Add(new IncidentViewModel(_incidentStore, _currentIncidentStore, _incidentRepository, _navigator, _viewModelFactory)
			{
				Id = incident.Id,
				IncidentType = incident.IncidentType.FromIncidentType(),
				Participants = incident.Participants.Select(participant => ToParticipantViewModel(participant)).ToList(),
				RegDate = incident.RegDate,
				ResolutionType = incident.ResolutionType.FromResolutionType()
			}); ;
		}

		private void OnIncidentDeleted(Incident incident)
		{
			RemoveIncident(incident.Id);
		}

		private void RemoveIncident(int id)
		{
			var incidentToRemove = Incidents.FirstOrDefault(incident => incident.Id == id);
			Incidents.Remove(incidentToRemove);
		}

		private void OnIncidentsLoaded(List<Incident> incidents)
		{
			Incidents = new ObservableCollection<IncidentViewModel>(
				incidents
				.Select(incident => ToIncidentViewModel(incident)));
		}

		private IncidentViewModel ToIncidentViewModel(Incident incident)
		{
			return new IncidentViewModel(_incidentStore, _currentIncidentStore, _incidentRepository, _navigator, _viewModelFactory)
			{
				Id = incident.Id,
				IncidentType = incident.IncidentType.FromIncidentType(),
				ResolutionType = incident.ResolutionType.FromResolutionType(),
				RegDate = incident.RegDate,
				Participants = incident.Participants.Select(participant => ToParticipantViewModel(participant)).ToList(),
				ParticipantsListing = incident.Participants.ToPersonString()
			};
		}

		private ParticipantViewModel ToParticipantViewModel(Participant participant)
		{
			return new ParticipantViewModel(_currentIncidentStore)
			{
				Id = participant.Id,
				FirstName = participant.Person.FirstName,
				LastName = participant.Person.LastName,
				MiddleName = participant.Person.MiddleName,
				Address = participant.Person.Address,
				PersonType = participant.PersonType.FromPersonType(),
				ConvictionsCount = participant.Person.ConvictionsCount
			};
		}
	}
}
