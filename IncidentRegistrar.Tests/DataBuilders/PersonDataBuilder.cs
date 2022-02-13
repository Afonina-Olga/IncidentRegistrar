using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.Tests.DataBuilders
{
	public class PersonDataBuilder : DataBuilder<Person>
	{
		private string _firstName;
		private string _lastName;
		private string _middleName;
		private int _count;
		private string _address;

		public PersonDataBuilder WithFirstName(string firstName)
		{
			ThrowsArgumentNullExceptionIfStringIsNullOrEmpty(firstName);

			_firstName = firstName;
			return this;
		}

		public PersonDataBuilder WithLastName(string lastName)
		{
			ThrowsArgumentNullExceptionIfStringIsNullOrEmpty(lastName);

			_lastName = lastName;
			return this;
		}

		public PersonDataBuilder WithMiddleName(string middleName)
		{
			ThrowsArgumentNullExceptionIfStringIsNullOrEmpty(middleName);

			_middleName = middleName;
			return this;
		}

		public PersonDataBuilder WithConvictionsCount(int count)
		{
			_count = count;
			return this;
		}

		public PersonDataBuilder WithAddress(string address)
		{
			_address = address;
			return this;
		}

		public override Person Build(int postfix = 1)
		{
			return new Person()
			{
				FirstName = _firstName ?? "FirstName" + postfix,
				LastName = _lastName ?? "LastName" + postfix,
				MiddleName = _middleName ?? "MiddleName" + postfix,
				ConvictionsCount = _count,
				Address = _address ?? "Address" + postfix
			};
		}
	}
}
