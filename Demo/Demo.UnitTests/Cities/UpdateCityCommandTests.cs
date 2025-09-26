using Demo.Application.CQRS.Cities;
using Demo.Application.Interfaces;
using FluentValidation.TestHelper;
using Moq;

namespace Demo.UnitTests.Cities
{
    [TestClass]
    public class UpdateCityCommandTests
    {
        private Mock<IUnitofWork> uowMock;
        private Mock<ICityRepository> cityRepoMock;

        [TestInitialize]
        public void Setup()
        {
            uowMock = new Mock<IUnitofWork>();
            cityRepoMock = new Mock<ICityRepository>();
            uowMock.Setup(u => u.CityRepository).Returns(cityRepoMock.Object);
        }

        [TestMethod]
        public async Task TestNameEmpty()
        {
            // Arrange
            var city = new UpdateCityDTO { Name = null };
            var unit = new UpdateCommand { City = city };

            // Act
            var result = await new UpdateCommandValidator(uowMock.Object).TestValidateAsync(unit);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.City.Name)
                .WithErrorMessage("De naam mag niet leeg zijn.");
        }

        [TestMethod]
        public async Task TestPopulationTooHigh()
        {
            // Arrange
            var city = new UpdateCityDTO { Population = 10000000001 };
            var command = new UpdateCommand { City = city };


            // Act
            var result = await new UpdateCommandValidator(uowMock.Object).TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.City.Population)
                .WithErrorMessage("Het aantal inwoners mag niet groter zijn dan 10.000.000.000.");
        }

        [TestMethod]
        public async Task TestCountryIdZero()
        {
            // Arrange
            var city = new UpdateCityDTO { CountryId = 0 };
            var command = new UpdateCommand { City = city };

            // Act
            var result = await new UpdateCommandValidator(uowMock.Object).TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.City.CountryId)
                .WithErrorMessage("Er moet een land gekozen worden.");
        }
    }
}