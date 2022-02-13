using System.Windows;

namespace IncidentRegistrar.UI
{
	public partial class MainWindow : Window
	{
		public MainWindow(object dataContext)
		{
			InitializeComponent();
			DataContext = dataContext;
		}
	}
}
