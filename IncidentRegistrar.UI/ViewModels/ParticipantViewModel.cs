using System.Collections.Generic;

namespace IncidentRegistrar.UI.ViewModels
{
	public class ParticipantViewModel : ViewModelBase
	{
		private string _lastName;
		public string LastName
		{
			get { return _lastName; }
			set
			{
				_lastName = value;
				OnPropertyChanged(nameof(LastName));
			}
		}

		private string _firstName;
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				_firstName = value;
				OnPropertyChanged(nameof(FirstName));
			}
		}

		private string _middleName;
		public string MiddleName
		{
			get { return _middleName; }
			set
			{
				_middleName = value;
				OnPropertyChanged(nameof(MiddleName));
			}
		}

		private string _address;
		public string Address
		{
			get { return _address; }
			set
			{
				_address = value;
				OnPropertyChanged(nameof(Address));
			}
		}

		private string _presonType;
		public string PersonType
		{
			get { return _presonType; }
			set
			{
				_presonType = value;
				OnPropertyChanged(nameof(PersonType));
			}
		}

		private int _convictionsCount;
		public int ConvictionsCount
		{
			get { return _convictionsCount; }
			set
			{
				_convictionsCount = value;
				OnPropertyChanged(nameof(ConvictionsCount));
			}
		}

		private string _selectedPersonType;
		public string SelectedPersonType
		{
			get { return _selectedPersonType; }
			set
			{
				_selectedPersonType = value;
				OnPropertyChanged(nameof(SelectedPersonType));
			}
		}

		public List<string> PersonTypes { get; set; } = new List<string>() { "Потерпевший", "Виновник", "Подозреваемый", "Свидетель" };
	}
}