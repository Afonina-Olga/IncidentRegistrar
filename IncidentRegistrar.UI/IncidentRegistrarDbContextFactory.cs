using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IncidentRegistrar.UI
{
	//public class IncidentRegistrarDbContextFactory : IDesignTimeDbContextFactory<IncidentRegistrarDbContext>
	//{
	//	private readonly Action<DbContextOptionsBuilder> _configureDbContext;

	//	public IncidentRegistrarDbContextFactory() { }

	//	public IncidentRegistrarDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
	//	{
	//		_configureDbContext = configureDbContext;
	//	}

	//	public IncidentRegistrarDbContext CreateDbContext(string[] args = null)
	//	{
	//		var options = new DbContextOptionsBuilder<IncidentRegistrarDbContext>();
	//		options.UseSqlite(@"Data Source=IncidentDb.db");
	//		return new IncidentRegistrarDbContext(options.Options);
	//	}
	//}

	public class IncidentRegistrarDbContextFactory
	{
		private readonly Action<DbContextOptionsBuilder> _configureDbContext;

		public IncidentRegistrarDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
		{
			_configureDbContext = configureDbContext;
		}

		public IncidentRegistrarDbContext CreateDbContext()
		{
			var options = new DbContextOptionsBuilder<IncidentRegistrarDbContext>();
			_configureDbContext(options);
			return new IncidentRegistrarDbContext(options.Options);
		}
	}
}
