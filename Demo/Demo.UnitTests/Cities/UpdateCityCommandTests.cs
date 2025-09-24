using Demo.Application.CQRS.Cities;
using Demo.Application.Interfaces;
using Demo.Domain;
using FluentValidation.TestHelper;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Demo.UnitTests.Cities
{
    [TestClass]
    public class UpdateCityCommandTests
    {
        private Mock<ICityRepository> cityRepo;

        [TestInitialize]
        public void Setup()
        {
            cityRepo = new Mock<ICityRepository>();
        }

        [TestMethod]
        public async Task TestNameEmpty()
        {
            // Arrange
            var city = new CityDTO { Name = null, Population = 1000, CountryId = 1 };
            var command = new UpdateCommand { City = city };

            var validator = new UpdateCommandValidator(cityRepo.Object);

            // Act
            var result = await validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.City.Name)
                .WithErrorMessage("De naam mag niet leeg zijn.");
        }

        [TestMethod]
        public async Task TestPopulationTooHigh()
        {
            // Arrange
            var city = new CityDTO { Name = "Test", Population = 10000000001, CountryId = 1 };
            var command = new UpdateCommand { City = city };

            var validator = new UpdateCommandValidator(countryRepoMock.Object);

            // Act
            var result = await validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.City.Population)
                .WithErrorMessage("Het aantal inwoners mag niet groter zijn dan 10.000.000.000.");
        }

        [TestMethod]
        public async Task TestCountryIdZero()
        {
            // Arrange
            var city = new CityDTO { Name = "Test", Population = 1000, CountryId = 0 };
            var command = new UpdateCommand { City = city };

            var validator = new UpdateCommandValidator(countryRepoMock.Object);

            // Act
            var result = await validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.City.CountryId)
                .WithErrorMessage("Er moet een land gekozen worden.");
        }

        [TestMethod]
        public async Task TestCountryDoesNotExist()
        {
            // Arrange
            var city = new CityDTO { Name = "Test", Population = 1000, CountryId = 99 };
            var command = new UpdateCommand { City = city };


            cityRepo.Setup(r => r.GetById(99)).ReturnsAsync((Country)null);

            var validator = new UpdateCommandValidator(countryRepoMock.Object);

            // Act
            var result = await validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.City.CountryId)
                .WithErrorCode("AsyncPredicateValidator");
        }

        [TestMethod]
        public async Task TestSuccessfulUpdateValidation()
        {
            // Arrange
            var city = new CityDTO { Name = "Valid", Population = 1000, CountryId = 1 };
            var command = new UpdateCommand { City = city };

            cityRepo.Setup(r => r.GetById(1)).ReturnsAsync(new Country());

            var validator = new UpdateCommandValidator(cityRepo.Object);

            // Act
            var result = await validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}