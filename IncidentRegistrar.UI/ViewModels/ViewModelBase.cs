using System.ComponentModel;

namespace IncidentRegistrar.UI.ViewModels
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public virtual void Dispose() { }

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
