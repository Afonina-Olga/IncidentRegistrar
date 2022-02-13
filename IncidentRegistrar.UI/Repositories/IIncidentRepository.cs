using System.Collections.Generic;
using System.Threading.Tasks;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.UI.Repositories
{
	public interface IIncidentRepository : IRepository<Incident>
	{
		Task<List<Incident>> GetByParticipantLastName(string lastName);
	}
}
