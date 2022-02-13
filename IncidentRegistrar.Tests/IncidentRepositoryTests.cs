using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;

using IncidentRegistrar.UI.Models;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.Tests.DataBuilders;

namespace IncidentRegistrar.UI.Tests
{
	[TestFixture]
	public class IncidentRepositoryTests
	{
		/// <summary>
		/// Простой тест для метода Create (без дополнительных проверок и сложностей)
		/// </summary>
		[Test]
		public async Task Create_NewIncident()
		{
			// Arrange
			var repo = CreateIncidentRepository("IncidentDbCreate1");

			var incident = new IncidentDataBuilder()
				.WithDate(new DateTime(2022, 1, 1))
				.WithIncidentType(IncidentType.Accident)
				.WithResolutionType(ResolutionType.Refused)
				.WithParticipant(
					new ParticipantDataBuilder()
						.WithPerson(new PersonDataBuilder().Build(1))
						.WithPersonType(PersonType.Victim)
						.Build())
				.WithParticipant(
					new ParticipantDataBuilder()
						.WithPerson(new PersonDataBuilder().Build(2))
						.WithPersonType(PersonType.Culprit)
						.Build())
				.WithParticipant(
					new ParticipantDataBuilder()
						.WithPerson(new PersonDataBuilder().Build(3))
						.WithPersonType(PersonType.Suspect)
						.Build())
				.WithParticipant(
					new ParticipantDataBuilder()
						.WithPerson(new PersonDataBuilder().Build(4))
						.WithPersonType(PersonType.Witness)
						.Build())
				.Build();

			// Act
			var createdIncident = await repo.Create(incident);
			var actualIncident = await repo.Get(createdIncident.Id);

			// Assert
			Assert.AreEqual(incident.Id, actualIncident.Id);
			Assert.AreEqual(incident.IncidentType, actualIncident.IncidentType);
			Assert.AreEqual(incident.ResolutionType, actualIncident.ResolutionType);
			Assert.AreEqual(incident.RegDate, actualIncident.RegDate);
			CollectionAssert.AreEqual(incident.Participants, actualIncident.Participants);
		}

		/// <summary>
		/// Два человека участвуют в происшествии, проживающие по одному адресу
		/// </summary>
		[Test]
		public async Task Create_NewIncident_WithTwoPeopleLiveAtTheSameAddress()
		{
			// Arrange
			var repo = CreateIncidentRepository("IncidentDbCreate2");

			var incident = new IncidentDataBuilder()
				.WithDate(new DateTime(2022, 1, 1))
				.WithIncidentType(IncidentType.Accident)
				.WithResolutionType(ResolutionType.Refused)
				.WithParticipant(
					new ParticipantDataBuilder()
						.WithPerson(
							new PersonDataBuilder()
							.WithAddress("Address").Build(1))
						.WithPersonType(PersonType.Victim)
						.Build())
				.WithParticipant(
					new ParticipantDataBuilder()
						.WithPerson(
							new PersonDataBuilder()
							.WithAddress("Address").Build(2))
						.WithPersonType(PersonType.Culprit)
						.Build())
				.WithParticipant(
					new ParticipantDataBuilder()
						.WithPerson(
							new PersonDataBuilder()
							.WithAddress("Address").Build(3))
						.WithPersonType(PersonType.Suspect)
						.Build())
				.WithParticipant(
					new ParticipantDataBuilder()
						.WithPerson(
							new PersonDataBuilder()
							.WithAddress("Address").Build(4))
						.WithPersonType(PersonType.Witness)
						.Build())
				.Build();

			// Act
			var createdIncident = await repo.Create(incident);
			var actualIncident = await repo.Get(createdIncident.Id);

			// Assert
			Assert.AreEqual(incident.Id, actualIncident.Id);
			Assert.AreEqual(incident.IncidentType, actualIncident.IncidentType);
			Assert.AreEqual(incident.ResolutionType, actualIncident.ResolutionType);
			Assert.AreEqual(incident.RegDate, actualIncident.RegDate);
			CollectionAssert.AreEqual(incident.Participants, actualIncident.Participants);
		}

		/// <summary>
		/// Один человек участвует в нескольких инцидентах
		/// </summary>
		[Test]
		public async Task Create_WithOnePersonTakesPartInTwoIncidents()
		{
			// Arrange

			// Act

			// Assert
		}

		/// <summary>
		/// Один человек может участвовать в инциденте только один раз в любом статусе
		/// Мы не можем добавлять одинаковых людей к одному инциденту
		/// </summary>
		[Test]
		public async Task Create_WithPersonCanBeMentionedOnceInTheSameIncident()
		{
			// Arrange

			// Act

			// Assert
		}

		/// <summary>
		/// Удаление происшествия по существующему Id
		/// </summary>
		/// <returns>true</returns>
		[Test]
		public async Task Delete_WithExistingId_ReturnsTrue()
		{
			// Arrange

			// Act

			// Assert
		}

		/// <summary>
		/// Удаление происшествия, если id не существует
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		[Test]
		public async Task Delete_WithNonExistingId_ThrowsArgumentNullExeption()
		{
			// Arrange

			// Act

			// Assert
		}

		/// <summary>
		/// При удалении происшествия должны быть каскадно удалены все связанные данные (адреса, люди),
		/// если они больше нигде не используются
		/// </summary>
		[Test]
		public async Task Delete_AllLinkedDataMustBeDeletedIfNoLongerUsed()
		{
			// Arrange

			// Act

			// Assert
		}

		[Test]
		public async Task Get_() { }

		private IncidentRepository CreateIncidentRepository(string databaseName)
		{
			var contextFactory = new IncidentRegistrarDbContextFactory(options => options.UseInMemoryDatabase(databaseName));
			return new IncidentRepository(contextFactory);
		}
	}
}
