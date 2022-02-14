using Moq;
using System.Threading.Tasks;
using NUnit.Framework;
using IncidentRegistrar.UI.Repositories;
using IncidentRegistrar.UI.Services;
using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.Tests
{
	[TestFixture]
	public class AuthenticationServiceTests
	{
		[Test]
		public async Task Register_WithDifferentPasswords_ReturnsPasswordsDoNotMatchResult()
		{
			// Arrange
			var userRepository = new Mock<IUserRepository>();
			var authenticationService = new AuthenticationService(userRepository.Object);

			// Act
			var result = await authenticationService.Register("login", "pass1", "pass2");

			// Assert
			Assert.AreEqual(RegistrationResult.PasswordsDoNotMatch, result);
		}

		[Test]
		public async Task Register_WithExistingLogin_ReturnsLoginAlreadyExists()
		{
			// Arrange
			var repo = new Mock<IUserRepository>();
			repo.Setup(repo => repo.Get("login")).ReturnsAsync(new User()
			{
				Login = "login",
				Password = "123"
			});
			var authenticationService = new AuthenticationService(repo.Object);

			// Act
			var result = await authenticationService.Register("login", "pass1", "pass1");

			// Assert
			Assert.AreEqual(RegistrationResult.LoginAlreadyExists, result);
		}

		[Test]
		public async Task Register_WithNonExistringLogin_RetirnsSuccessResult()
		{
			// Arrange
			var repo = new Mock<IUserRepository>();
			User user = null;

			repo.Setup(repo => repo.Get("login")).ReturnsAsync(user);

			var authenticationService = new AuthenticationService(repo.Object);

			// Act
			var result = await authenticationService.Register("login", "pass1", "pass1");

			// Assert
			Assert.AreEqual(RegistrationResult.Success, result);
		}
	}
}
