using System;
using System.Collections.Generic;
using System.Linq;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.Tests.DataBuilders
{
	public class IncidentDataBuilder : DataBuilder<Incident>
	{
		private DateTime _regDate;
		private IncidentType _incidentType;
		private ResolutionType _resolutionType;
		private List<Participant> _participants = new List<Participant>();

		public IncidentDataBuilder WithDate(DateTime date)
		{
			_regDate = date;
			return this;
		}

		public IncidentDataBuilder WithIncidentType(IncidentType type)
		{
			_incidentType = type;
			return this;
		}

		public IncidentDataBuilder WithResolutionType(ResolutionType type)
		{
			_resolutionType = type;
			return this;
		}

		public IncidentDataBuilder WithParticipant(Participant participant)
		{
			_participants.Add(participant);
			return this;
		}

		public override Incident Build(int postfix = 1)
		{
			if (!_participants.Any())
				_participants.Add(new ParticipantDataBuilder().Build());

			return new Incident()
			{
				IncidentType = _incidentType,
				RegDate = _regDate,
				ResolutionType = _resolutionType,
				Participants = _participants
			};
		}
	}
}
