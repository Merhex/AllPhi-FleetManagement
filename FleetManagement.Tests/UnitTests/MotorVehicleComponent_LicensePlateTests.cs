using FleetManagement.API.Write.Commands;
using FleetManagement.BLL.MotorVehicles.Components;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FleetManagement.Tests.UnitTests
{
    /// <summary>
    /// Testing the following business in methods from the IMotorVehicleComponent Interface.
    /// 
    /// Task<ICommandResponse> AssignLicensePlateToMotorVehicleAsync(AssignLicensePlateCommand command, CancellationToken cancellationToken);
    /// Task<ICommandResponse> WithdrawLicensePlateFromMotorVehicleAsync(WithdrawLicensePlateCommand command, CancellationToken cancellationToken);
    /// Task<ICommandResponse> DeleteLicensePlateAsync(DeleteLicensePlateCommand command, CancellationToken cancellationToken);
    /// Task<ICommandResponse> CreateLicensePlateAsync(CreateLicensePlateCommand command, CancellationToken cancellationToken);
    /// Task<ICommandResponse> ChangeLicensePlateInUseStatusAsync(ChangeLicensePlateInUseStatusCommand command, CancellationToken cancellationToken);
    /// </summary>
    [TestFixture]
    public class MotorVehicleComponent_LicensePlateTests
    {
        private IMotorVehicleComponent _systemUnderTest;

        private readonly LicensePlateValidator _licensePlateValidator = new LicensePlateValidator();
        private readonly MotorVehicleValidator _motorVehicleValidator = new MotorVehicleValidator();
        private Mock<ILicensePlateRepository> _licensePlateRepository;
        private Mock<ILicensePlateSnapshotRepository> _licensePlateSnapshotRepository;
        private Mock<IMotorVehicleRepository> _motorVehicleRepository;
        private CancellationToken _cancellationToken;
        private LicensePlate _licensePlate;
        private MotorVehicle _motorVehicle;


    [SetUp]
        public void SetUp()
        {
            _licensePlateRepository = new Mock<ILicensePlateRepository>();
            _licensePlateSnapshotRepository = new Mock<ILicensePlateSnapshotRepository>();
            _motorVehicleRepository = new Mock<IMotorVehicleRepository>();


            _licensePlate = new LicensePlate { Id = 1, Identifier = "1", InUse = false };
            _motorVehicle = new MotorVehicle { Id = 1, ChassisNumber = "1", LicensePlates = new List<LicensePlate>() };
            _cancellationToken = new CancellationToken();


            _licensePlateRepository
                .Setup(x => x
                .FindByIdentifierAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(_licensePlate);
            _licensePlateRepository
                .Setup(x => x
                .Remove(It.IsAny<LicensePlate>()))
                .Verifiable();
            _licensePlateRepository
                .Setup(x => x
                .SaveAsync())
                .ReturnsAsync(true);


            _motorVehicleRepository
                .Setup(x => x
                .FindByChassisNumberIncludeLicensePlatesAsync(_motorVehicle.ChassisNumber, _cancellationToken))
                .ReturnsAsync(_motorVehicle);
            _motorVehicleRepository
                .Setup(x => x
                .SaveAsync())
                .ReturnsAsync(true);


            _licensePlateSnapshotRepository
                .Setup(x => x
                .Add(It.IsAny<LicensePlateSnapshot>()))
                .Verifiable();
            _licensePlateSnapshotRepository
                .Setup(x => x
                .SaveAsync())
                .ReturnsAsync(true);


            _systemUnderTest = new MotorVehicleComponent(
                licensePlateRepository: _licensePlateRepository.Object,
                motorVehicleRepository: _motorVehicleRepository.Object,
                licensePlateSnaphotRepository: _licensePlateSnapshotRepository.Object,
                licensePlateValidator: _licensePlateValidator,
                motorVehicleValidator: _motorVehicleValidator);
        }
        
        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_WhenCalled_ShouldAssignLicensePlate()
        {
            //Act
            await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                    new AssignLicensePlateCommand 
                    {
                        ChassisNumber = _motorVehicle.ChassisNumber,
                        LicensePlateIdentifier = _licensePlate.Identifier
                    }, 
                    _cancellationToken);

            //Assert
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate));
        }

        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_LicensePlateAlreadyAssignedOnSameVehicle_ShouldDoNothing()
        {
            //Arrange
            _motorVehicle.LicensePlates.Add(_licensePlate);
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdentifierIncludeLicensePlatesAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            //Act
            var result = await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                new AssignLicensePlateCommand
                {
                    ChassisNumber = _motorVehicle.ChassisNumber,
                    LicensePlateIdentifier = _licensePlate.Identifier
                },
                _cancellationToken);

            //Assert
            Assert.That(_motorVehicle.LicensePlates
                .Where(x => x.Identifier == _licensePlate.Identifier)
                .Count(), 
                Is.EqualTo(1));
        }

        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_LicensePlateAlreadyAssignedOnAnotherVehicle_ShouldNotContainAssignedLicensePlate()
        {
            //Arrange
            var anotherMotorVehicle = new MotorVehicle { ChassisNumber = "2" };
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdentifierIncludeLicensePlatesAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(anotherMotorVehicle);

            //Act
            var result = await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                    new AssignLicensePlateCommand
                    {
                        LicensePlateIdentifier = _licensePlate.Identifier,
                        ChassisNumber = _motorVehicle.ChassisNumber
                    },
                    _cancellationToken);

            //Assert
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.False);
        }

        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_LicensePlateIsInUse_ShouldNotContainLicensePlate()
        {
            //Arrange
            _licensePlate.InUse = true;

            //Act
            await _systemUnderTest
            .AssignLicensePlateToMotorVehicleAsync(
                new AssignLicensePlateCommand
                {
                    ChassisNumber = _motorVehicle.ChassisNumber,
                    LicensePlateIdentifier = _licensePlate.Identifier
                },
                _cancellationToken);

            //Assert
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.False);
        }

        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_LicensePlateDoesNotExist_ShouldNotContainAnyLicensePlate()
        {
            //Arrange
            _licensePlateRepository
                .Setup(x => x
                .FindByIdentifierAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(() => null);

            //Act
            var result = await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                    new AssignLicensePlateCommand
                    {
                        ChassisNumber = _motorVehicle.ChassisNumber,
                        LicensePlateIdentifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            //Assert
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.False);
        }

        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_MotorVehicleDoesNotExist_ShouldNotContainLicensePlate()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByChassisNumberIncludeLicensePlatesAsync(_motorVehicle.ChassisNumber, _cancellationToken))
                .ReturnsAsync(() => null);

            //Act
            var result = await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                    new AssignLicensePlateCommand
                    {
                        ChassisNumber = _motorVehicle.ChassisNumber,
                        LicensePlateIdentifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            //Assert
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.False);
        }

        [Test]
        public async Task WithdrawLicensePlateFromMotorVehicleAsync_WhenCalled_ShouldWithdrawLicensePlateFromVehicle()
        {
            //Arrange
            _motorVehicle.LicensePlates.Add(_licensePlate);
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdentifierIncludeLicensePlatesAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            //Act
            var result = await _systemUnderTest
                .WithdrawLicensePlateFromMotorVehicleAsync(
                    new WithdrawLicensePlateCommand
                    {
                        Identifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            //Assert
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.False);
        }

        [Test]
        public async Task WithdrawLicensePlateFromMotorVehicleAsync_LicensePlateDoesNotExist_ShouldNotRemoveLicensePlate()
        {
            //Arrange
            _motorVehicle.LicensePlates.Add(_licensePlate);
            _licensePlateRepository
                .Setup(x => x
                .FindByIdentifierAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(() => null);

            //Act
            var result = await _systemUnderTest
                .WithdrawLicensePlateFromMotorVehicleAsync(
                    new WithdrawLicensePlateCommand
                    {
                        Identifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            //Assert
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.True);
        }

        [Test]
        public async Task WithdrawLicensePlateFromMotorVehicleAsync_LicensePlateIsInUse_ShouldNotWithdrawLicensePlate()
        {
            //Arrange
            _licensePlate.InUse = true;
            _motorVehicle.LicensePlates.Add(_licensePlate);

            //Act
            await _systemUnderTest
                .WithdrawLicensePlateFromMotorVehicleAsync(
                    new WithdrawLicensePlateCommand
                    {
                        Identifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            //Assert
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.True);
        }


        [Test]
        public async Task DeleteLicensePlateAsync_WhenCalled_ShouldRemoveLicensePlate()
        {
            // Act
            var result = await _systemUnderTest
                .DeleteLicensePlateAsync(
                    new DeleteLicensePlateCommand
                    {
                        Identifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            // Assert
            _licensePlateRepository
                .Verify(x => x
                .Remove(It.Is<LicensePlate>(with => with.Identifier == _licensePlate.Identifier)),
                Times.Once());
        }

        [Test]
        public async Task DeleteLicensePlateAsync_LicensePlateInUse_ShouldNotRemoveLicensePlate()
        {
            //Arrange
            _licensePlate.InUse = true;

            //Act
            var result = await _systemUnderTest
                .DeleteLicensePlateAsync(
                    new DeleteLicensePlateCommand
                    {
                        Identifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            //Assert
            _licensePlateRepository
                .Verify(x => x
                .Remove(It.Is<LicensePlate>(with => with.Identifier == _licensePlate.Identifier)),
                Times.Never());
        }

        /// <summary>
        /// The identifier can only be 9 characters long. And can not be empty, whitespaced or null.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        [TestCase("1234567890")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("")]
        public async Task CreateLicensePlateAsync_IdentifierViolatesValidationRules_ShouldNotCreateLicensePlate(string identifier)
        {
            //Act
            var result = await _systemUnderTest
                .CreateLicensePlateAsync(
                    new CreateLicensePlateCommand
                    {
                        Identifier = identifier
                    },
                    _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(400));
        }


        [Test]
        public async Task ChangeLicensePlateInUseStatusAsync_WhenCalled_ShouldChangeInUseStatus()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdentifierIncludeLicensePlatesAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        Status = true,
                        Identifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            // Assert
            Assert.That(_licensePlate.InUse, Is.True);
        }

        [Test]
        public async Task ChangeLicensePlateInUseStatusAsync_WhenCalled_ShouldCreateSnapshot()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdentifierIncludeLicensePlatesAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        Status = true,
                        Identifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            // Assert
            _licensePlateSnapshotRepository
                .Verify(x => x
                .Add(It.Is<LicensePlateSnapshot>(with => with.LicensePlate.Identifier == _licensePlate.Identifier)), 
                Times.Once());
        }


        [Test]
        public async Task ChangeLicensePlateInUseStatusAsync_InUseIsSameValue_ShouldDoNothing()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdentifierIncludeLicensePlatesAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        Status = _licensePlate.InUse,
                        Identifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            // Assert 
            Assert.That(_licensePlate.InUse, Is.EqualTo(_licensePlate.InUse));
        }

        [Test]
        public async Task ChangeLicensePlateInUseStatusAsync_InUseIsSameValue_ShouldNotCreateASnapshot()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdentifierIncludeLicensePlatesAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        Status = _licensePlate.InUse,
                        Identifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            // Assert 
            _licensePlateSnapshotRepository
                .Verify(x => x
                .Add(It.Is<LicensePlateSnapshot>(with => with.LicensePlate.Identifier == _licensePlate.Identifier)),
                Times.Never());
        }


        [Test]
        public async Task ChangeLicensePlateInUseStatus_LicensePlateIsNotAssignedToMotorVehicle_LicensePlateShouldNotBeInUse()
        {
            // Arrange 
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdentifierIncludeLicensePlatesAsync(_licensePlate.Identifier, _cancellationToken))
                .ReturnsAsync(() => null);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        Status = true,
                        Identifier = _licensePlate.Identifier
                    },
                    _cancellationToken);

            // Assert 
            Assert.That(_licensePlate.InUse, Is.False);
        }
    }
}
