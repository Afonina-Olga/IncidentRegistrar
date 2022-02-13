using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.Tests.DataBuilders
{
	public class ParticipantDataBuilder : DataBuilder<Participant>
	{
		private Person _person;
		private PersonType _personType;

		public ParticipantDataBuilder WithPersonType(PersonType personType)
		{
			_personType = personType;
			return this;
		}

		public ParticipantDataBuilder WithPerson(Person person)
		{
			_person = person;
			return this;
		}

		public override Participant Build(int postfix = 1)
		{
			return new Participant()
			{
				Person = _person ?? new PersonDataBuilder().Build(postfix),
				PersonType = _personType
			};
		}
	}
}
