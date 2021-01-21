﻿using FleetManagement.BLL.MotorVehicles.Commands;
using FleetManagement.BLL.MotorVehicles.Components;
using FleetManagement.BLL.MotorVehicles.Components.Interfaces;
using FleetManagement.BLL.MotorVehicles.Validators;
using FleetManagement.DAL.Repositories.Interfaces;
using FleetManagement.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
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

        private LicensePlateValidator _licensePlateValidator = new LicensePlateValidator();
        private MotorVehicleValidator _motorVehicleValidator = new MotorVehicleValidator();
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
            _motorVehicle = new MotorVehicle { Id = 1, LicensePlates = new List<LicensePlate>() };
            _cancellationToken = new CancellationToken();


            _licensePlateRepository
                .Setup(x => x
                .FindByIdAsync(_licensePlate.Id, _cancellationToken))
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
                .FindByIdAsync(_motorVehicle.Id, _cancellationToken))
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
        public async Task AssignLicensePlateToMotorVehicleAsync_WhenCalled_ShoulddReturn200OkResponseAndAssignLicensePlate()
        {
            //Act
            var result = await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                    new AssignLicensePlateCommand 
                    { 
                        LicensePlateId = _licensePlate.Id, 
                        MotorVehicleId = _motorVehicle.Id 
                    }, 
                    _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(200));
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate));
        }

        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_LicensePlateAlreadyAssignedOnSameVehicle_ShouldReturn204NoContentResponseAndHaveAssignedLicensePlate()
        {
            //Arrange
            _motorVehicle.LicensePlates.Add(_licensePlate);
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            //Act
            var result = await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                new AssignLicensePlateCommand 
                { 
                    LicensePlateId = _licensePlate.Id,
                    MotorVehicleId = _motorVehicle.Id
                }, 
                _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(204));
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate));
        }

        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_LicensePlateAlreadyAssignedOnAnotherVehicle_ShouldReturnA400BadRequestResponse()
        {
            //Arrange
            var anotherMotorVehicle = new MotorVehicle { Id = 2 };
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(anotherMotorVehicle);

            //Act
            var result = await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                    new AssignLicensePlateCommand 
                    { 
                        LicensePlateId = _licensePlate.Id, 
                        MotorVehicleId = _motorVehicle.Id 
                    },
                    _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(400));
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.False);
        }

        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_LicensePlateIsInUse_ShouldReturnA400BadRequestResponse()
        {
            //Arrange
            _licensePlate.InUse = true;

            //Act
            var result = await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                    new AssignLicensePlateCommand
                    {
                        LicensePlateId = _licensePlate.Id,
                        MotorVehicleId = _motorVehicle.Id
                    },
                    _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(400));
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.False);
        }

        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_LicensePlateDoesNotExist_ShouldReturnA400BadRequestResponse()
        {
            //Arrange
            _licensePlateRepository
                .Setup(x => x
                .FindByIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(() => null);

            //Act
            var result = await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                    new AssignLicensePlateCommand
                    {
                        LicensePlateId = _licensePlate.Id,
                        MotorVehicleId = _motorVehicle.Id
                    },
                    _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(400));
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.False);
        }

        [Test]
        public async Task AssignLicensePlateToMotorVehicleAsync_MotorVehicleDoesNotExist_ShouldReturnA400BadRequestResponse()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByIdAsync(_motorVehicle.Id, _cancellationToken))
                .ReturnsAsync(() => null);

            //Act
            var result = await _systemUnderTest
                .AssignLicensePlateToMotorVehicleAsync(
                    new AssignLicensePlateCommand
                    {
                        LicensePlateId = _licensePlate.Id,
                        MotorVehicleId = _motorVehicle.Id
                    },
                    _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(400));
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.False);
        }

        [Test]
        public async Task WithdrawLicensePlateFromMotorVehicleAsync_WhenCalled_ShouldWithdrawLicensePlateFromVehicleAndReturn200OkResponse()
        {
            //Arrange
            _motorVehicle.LicensePlates.Add(_licensePlate);
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            //Act
            var result = await _systemUnderTest
                .WithdrawLicensePlateFromMotorVehicleAsync(
                    new WithdrawLicensePlateCommand
                    {
                        LicensePlateId = _licensePlate.Id
                    },
                    _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(200));
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.False);
        }

        [Test]
        public async Task WithdrawLicensePlateFromMotorVehicleAsync_LicensePlateDoesNotExist_ShouldReturn400BadRequest()
        {
            //Arrange
            _licensePlateRepository
                .Setup(x => x
                .FindByIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(() => null);

            //Act
            var result = await _systemUnderTest
                .WithdrawLicensePlateFromMotorVehicleAsync(
                    new WithdrawLicensePlateCommand
                    {
                        LicensePlateId = _licensePlate.Id
                    },
                    _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(400));
        }

        [Test]
        public async Task WithdrawLicensePlateFromMotorVehicleAsync_LicensePlateIsInUse_ShouldReturn400BadRequest()
        {
            //Arrange
            _licensePlate.InUse = true;
            _motorVehicle.LicensePlates.Add(_licensePlate);

            //Act
            var result = await _systemUnderTest
                .WithdrawLicensePlateFromMotorVehicleAsync(
                    new WithdrawLicensePlateCommand
                    {
                        LicensePlateId = _licensePlate.Id
                    },
                    _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(400));
            Assert.That(_motorVehicle.LicensePlates.Contains(_licensePlate), Is.True);
        }


        [Test]
        public async Task DeleteLicensePlateAsync_WhenCalled_ShouldRemoveLicensePlateAndReturn200Ok()
        {
            // Act
            var result = await _systemUnderTest
                .DeleteLicensePlateAsync(
                    new DeleteLicensePlateCommand
                    {
                        LicensePlateId = _licensePlate.Id
                    },
                    _cancellationToken);

            // Assert 
            Assert.That(result.Status, Is.EqualTo(200));
            _licensePlateRepository
                .Verify(x => x
                .Remove(It.Is<LicensePlate>(with => with.Id == _licensePlate.Id)), 
                Times.Once());
        }

        [Test]
        public async Task DeleteLicensePlateAsync_LicensePlateInUse_ShouldReturn400BadRequest()
        {
            //Arrange
            _licensePlate.InUse = true;

            //Act
            var result = await _systemUnderTest
                .DeleteLicensePlateAsync(
                    new DeleteLicensePlateCommand
                    {
                        LicensePlateId = _licensePlate.Id
                    }, 
                    _cancellationToken);

            //Assert
            Assert.That(result.Status, Is.EqualTo(400));
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
        public async Task CreateLicensePlateAsync_IdentifierViolatesValidationRules_ShouldReturn400BadRequest(string identifier)
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
        public async Task ChangeLicensePlateInUseStatusAsync_WhenCalled_ShouldChangeInUseStatusAndReturn200OkResponse()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        InUse = true,
                        LicensePlateId = _licensePlate.Id
                    },
                    _cancellationToken);

            // Assert 
            Assert.That(result.Status, Is.EqualTo(200));
            Assert.That(_licensePlate.InUse, Is.True);
        }

        [Test]
        public async Task ChangeLicensePlateInUseStatusAsync_WhenCalled_ShouldReturn200OkResponseAndBeInUse()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        InUse = true,
                        LicensePlateId = _licensePlate.Id
                    },
                    _cancellationToken);

            // Assert 
            Assert.That(result.Status, Is.EqualTo(200));
            Assert.That(_licensePlate.InUse, Is.True);
        }

        [Test]
        public async Task ChangeLicensePlateInUseStatusAsync_WhenCalled_ShouldCreateSnapshot()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        InUse = true,
                        LicensePlateId = _licensePlate.Id
                    },
                    _cancellationToken);

            // Assert 
            _licensePlateSnapshotRepository
                .Verify(x => x
                .Add(It.Is<LicensePlateSnapshot>(with => with.LicensePlate.Id == _licensePlate.Id)),
                Times.Once());
        }


        [Test]
        public async Task ChangeLicensePlateInUseStatusAsync_InUseIsSameValue_ShouldReturn204NoContentResponse()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        InUse = _licensePlate.InUse,
                        LicensePlateId = _licensePlate.Id
                    },
                    _cancellationToken);

            // Assert 
            Assert.That(result.Status, Is.EqualTo(204));
        }

        [Test]
        public async Task ChangeLicensePlateInUseStatusAsync_InUseIsSameValue_ShouldNotCreateASnapshot()
        {
            //Arrange
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(_motorVehicle);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        InUse = _licensePlate.InUse,
                        LicensePlateId = _licensePlate.Id
                    },
                    _cancellationToken);

            // Assert 
            _licensePlateSnapshotRepository
                .Verify(x => x
                .Add(It.Is<LicensePlateSnapshot>(with => with.LicensePlate.Id == _licensePlate.Id)),
                Times.Never());
        }


        [Test]
        public async Task ChangeLicensePlateInUseStatus_LicensePlateIsNotAssignedToMotorVehicle_ShouldReturn400BadRequest()
        {
            // Arrange 
            _motorVehicleRepository
                .Setup(x => x
                .FindByLicensePlateIdAsync(_licensePlate.Id, _cancellationToken))
                .ReturnsAsync(() => null);

            // Act
            var result = await _systemUnderTest
                .ChangeLicensePlateInUseStatusAsync(
                    new ChangeLicensePlateInUseStatusCommand
                    {
                        InUse = true,
                        LicensePlateId = _licensePlate.Id
                    },
                    _cancellationToken);

            // Assert 
            Assert.That(result.Status, Is.EqualTo(400));
        }
    }
}