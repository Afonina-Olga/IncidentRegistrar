using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IncidentRegistrar.UI.ViewModels;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.State;
using IncidentRegistrar.UI.ViewModels.Factories;
using IncidentRegistrar.UI.Services;

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
					services.AddSingleton<IUserRepository, UserRepository>();
					services.AddSingleton<IAuthenticationService, AuthenticationService>();
					services.AddSingleton<INavigator, Navigator>();
					services.AddSingleton<IUserStore, UserStore>();
					services.AddSingleton<IIncidentStore, IncidentStore>();
					services.AddSingleton<ICurrentIncidentStore, CurrentIncidentStore>();

					services.AddTransient(CreateHomeViewModel);
					services.AddTransient<MainViewModel>();

					services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
					services.AddSingleton<CreateViewModel<LoginViewModel>>(services => () => CreateLoginViewModel(services));
					services.AddSingleton<CreateViewModel<RegisterViewModel>>(services => () => CreateRegisterViewModel(services));
					services.AddSingleton<CreateViewModel<CreateIncidentViewModel>>(services => () => CreateCreateIncidentViewModel(services));
					services.AddSingleton<CreateViewModel<ReadIncidentViewModel>>(services => () => CreateReadIncidentViewModel(services));
					services.AddSingleton<CreateViewModel<EditIncidentViewModel>>(services => () => CreateEditIncidentViewModel(services));

					services.AddSingleton<IViewModelFactory, ViewModelFactory>();

					services.AddSingleton<ViewModelRenavigator<HomeViewModel>>();
					services.AddSingleton<ViewModelRenavigator<LoginViewModel>>();
					services.AddSingleton<ViewModelRenavigator<RegisterViewModel>>();
					services.AddSingleton<ViewModelRenavigator<CreateIncidentViewModel>>();
					services.AddSingleton<ViewModelRenavigator<ReadIncidentViewModel>>();
					services.AddSingleton<ViewModelRenavigator<EditIncidentViewModel>>();
				});
		}

		private static CreateIncidentViewModel CreateCreateIncidentViewModel(IServiceProvider services)
		{
			return new CreateIncidentViewModel(
				services.GetRequiredService<IIncidentStore>(),
				services.GetRequiredService<IIncidentRepository>(),
				services.GetRequiredService<ViewModelRenavigator<HomeViewModel>>());
		}

		private static ReadIncidentViewModel CreateReadIncidentViewModel(IServiceProvider services)
		{
			return new ReadIncidentViewModel(
				services.GetRequiredService<ViewModelRenavigator<HomeViewModel>>(),
				services.GetRequiredService<ICurrentIncidentStore>());
		}

		private static EditIncidentViewModel CreateEditIncidentViewModel(IServiceProvider services)
		{
			return new EditIncidentViewModel();
		}

		private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
		{
			return new HomeViewModel(
				services.GetRequiredService<IIncidentRepository>(),
				services.GetRequiredService<INavigator>(),
				services.GetRequiredService<IViewModelFactory>(),
				services.GetRequiredService<IIncidentStore>(),
				services.GetRequiredService<ICurrentIncidentStore>());
		}

		private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
		{
			return new LoginViewModel(
				services.GetRequiredService<ViewModelRenavigator<HomeViewModel>>(),
				services.GetRequiredService<ViewModelRenavigator<RegisterViewModel>>(),
				services.GetRequiredService<IUserStore>(),
				services.GetRequiredService<IAuthenticationService>());
		}

		private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
		{
			return new RegisterViewModel(
				services.GetRequiredService<ViewModelRenavigator<LoginViewModel>>(),
				services.GetRequiredService<ViewModelRenavigator<LoginViewModel>>(),
				services.GetRequiredService<IAuthenticationService>());
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
