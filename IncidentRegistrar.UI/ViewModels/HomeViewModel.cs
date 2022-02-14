using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		private readonly IIncidentRepository _incidentRepository;

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

		#endregion

		public HomeViewModel(
			IIncidentRepository incidentRepository,
			INavigator navigator,
			IViewModelFactory viewModelFactory,
			IIncidentStore incidentStore)
		{
			_incidentStore = incidentStore;
			_incidentRepository = incidentRepository;

			LoadIncidentsCommand = new LoadIncidentsCommand(this, incidentRepository, incidentStore);
			LoadIncidentsCommand.Execute(null);

			UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);

			_incidentStore.IncidentDeleted += OnIncidentDeleted;
			_incidentStore.IncidentAdded += OnIncidentAdded;
		}

		public override void Dispose()
		{
			_incidentStore.IncidentDeleted -= OnIncidentDeleted;
			_incidentStore.IncidentAdded -= OnIncidentAdded;
			base.Dispose();
		}

		private void OnIncidentAdded(Incident incident)
		{
			Incidents.Add(new IncidentViewModel(_incidentStore, _incidentRepository)
			{
				Id = incident.Id,
				IncidentType = incident.IncidentType.FromIncidentType(),
				Participants = Convert(incident.Participants),
				RegDate = incident.RegDate,
				ResolutionType = incident.ResolutionType.FromResolutionType()
			});
		}

		private void OnIncidentDeleted(Incident incident)
		{
			var incidentToRemove = Incidents.FirstOrDefault(incident => incident.Id == incident.Id);
			Incidents.Remove(incidentToRemove);
		}

		private string Convert(List<Participant> participants)
		{
			var names = participants.Select(participant =>
			{
				return $"{participant.Person.LastName} {participant.Person.FirstName} {participant.Person.MiddleName}";
			});

			return string.Join(Environment.NewLine, names);
		}
	}
}
