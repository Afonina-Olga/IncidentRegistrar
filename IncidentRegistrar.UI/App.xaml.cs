using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IncidentRegistrar.UI.ViewModels;
using IncidentRegistrar.UI.Repositories;

namespace IncidentRegistrar.UI
{
	public partial class App : Application
	{
		private readonly IHost _host;

		public App()
		{
			_host = CreateHostBuilder().Build();
		}

		public static IHostBuilder CreateHostBuilder(string[] args = null)
		{
			return Host
				.CreateDefaultBuilder(args)
				.ConfigureServices((context, services) =>
				{
					void options(DbContextOptionsBuilder o) => o.UseSqlite("Data Source=D://MyDB//IncidentDb.db");
					services.AddDbContext<IncidentRegistrarDbContext>(options);
					services.AddSingleton(new IncidentRegistrarDbContextFactory(options));
					services.AddTransient<MainViewModel>();
					services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
					services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
					services.AddSingleton<IIncidentRepository, IncidentRepository>();
				});
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			_host.Start();

			var contextFactory = _host.Services.GetRequiredService<IncidentRegistrarDbContextFactory>();
			using (var context = contextFactory.CreateDbContext())
			{
				context.Database.Migrate();
			}

			var window = _host.Services.GetRequiredService<MainWindow>();
			window.Show();

			base.OnStartup(e);
		}

		protected override async void OnExit(ExitEventArgs e)
		{
			await _host.StopAsync();
			_host.Dispose();

			base.OnExit(e);
		}
	}
}
