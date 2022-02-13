using Microsoft.EntityFrameworkCore;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.UI
{
	public class IncidentRegistrarDbContext : DbContext
	{
		public DbSet<Participant> Participants { get; set; }

		public DbSet<Incident> Incidents { get; set; }

		public DbSet<Person> Persons { get; set; }

		public DbSet<ParticipantIncident> ParticipantIncident { get; set; }

		public IncidentRegistrarDbContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Participant>()
				.HasMany(incident => incident.Incidents)
				.WithMany(person => person.Participants)
				.UsingEntity<ParticipantIncident>(
					pi => pi
						.HasOne(pi => pi.Incident)
						.WithMany()
						.HasForeignKey(pi => pi.IncidentId),
					pi => pi
						.HasOne(pi => pi.Participant)
						.WithMany()
						.HasForeignKey(pi => pi.ParticipantId))
				.ToTable(nameof(ParticipantIncident))
				.HasKey(pi => new
				{
					pi.ParticipantId,
					pi.IncidentId
				});

			base.OnModelCreating(modelBuilder);
		}
	}
}
