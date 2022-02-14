using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IncidentRegistrar.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace IncidentRegistrar.UI.Repositories
{
	public class IncidentRepository : Repository<Incident>, IIncidentRepository
	{
		private readonly IncidentRegistrarDbContextFactory _contextFactory;

		public IncidentRepository(IncidentRegistrarDbContextFactory contextFactory)
			: base(contextFactory)
		{
			_contextFactory = contextFactory;
		}

		public override async Task<Incident> Create(Incident entity)
		{
			using var context = _contextFactory.CreateDbContext();

			var createdIncident = await base.Create(new Incident()
			{
				IncidentType = entity.IncidentType,
				RegDate = entity.RegDate,
				ResolutionType = entity.ResolutionType
			});

			await AddParticipants(createdIncident.Id, entity.Participants);

			await context.SaveChangesAsync();
			return await Get(createdIncident.Id);
		}

		public override async Task<bool> Delete(int id)
		{
			using var context = _contextFactory.CreateDbContext();

			var incident = await Get(id);
			if (incident != null)
			{
				context.Remove(incident);
				await context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public override async Task<Incident> Update(int id, Incident entity)
		{
			using var context = _contextFactory.CreateDbContext();

			var incidentInDb = await context.Incidents.FirstOrDefaultAsync(incident => incident.Id == id);

			incidentInDb.IncidentType = entity.IncidentType;
			incidentInDb.ResolutionType = entity.ResolutionType;
			incidentInDb.RegDate = entity.RegDate;

			context.ParticipantIncident.RemoveRange(context.ParticipantIncident.Where(x => x.IncidentId == incidentInDb.Id));
			context.SaveChanges();

			await AddParticipants(id, entity.Participants);

			return await Get(id);
		}

		public override async Task<IEnumerable<Incident>> Get()
		{
			using var context = _contextFactory.CreateDbContext();

			var entities = await context.Incidents
				.Include(incident => incident.Participants)
					.ThenInclude(x => x.Person)
				.ToListAsync();
			return entities;
		}

		public override async Task<Incident> Get(int id)
		{
			using var context = _contextFactory.CreateDbContext();

			var entity = await context.Incidents
				.Include(incident => incident.Participants)
					.ThenInclude(x => x.Person)
				.FirstOrDefaultAsync(incident => incident.Id == id);

			return entity;
		}

		public async Task<List<Incident>> GetByParticipantLastName(string lastName)
		{
			using var context = _contextFactory.CreateDbContext();

			return await context.Incidents
				.Include(incident => incident.Participants)
					.ThenInclude(x => x.Person)
				.Where(incident => incident.Participants.Any(x => x.Person.LastName.ToUpper().StartsWith(lastName.ToUpper())))
				.ToListAsync();
		}

		private async Task AddParticipants(int id, List<Participant> participants)
		{
			using var context = _contextFactory.CreateDbContext();

			foreach (var participant in participants)
			{
				var participantInDb = context.Participants.FirstOrDefault(item =>
					item.Person.LastName == participant.Person.LastName &&
					item.Person.FirstName == participant.Person.FirstName &&
					item.Person.MiddleName == participant.Person.MiddleName &&
					item.Person.Address == participant.Person.Address);

				if (participantInDb != null)
				{
					await context.Set<ParticipantIncident>().AddAsync(new ParticipantIncident()
					{
						IncidentId = id,
						ParticipantId = participantInDb.Id
					});
				}
				else
				{
					var addedEntity = await context.Participants.AddAsync(participant);
					await context.SaveChangesAsync();
					await context.Set<ParticipantIncident>().AddAsync(new ParticipantIncident()
					{
						IncidentId = id,
						ParticipantId = addedEntity.Entity.Id
					});
				}
			}
			context.SaveChanges();
		}
	}
}
