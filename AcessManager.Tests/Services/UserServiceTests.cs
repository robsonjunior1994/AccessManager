using AccessManager.Api.Common;
using AccessManager.Api.DTOs;
using AccessManager.Api.Models;
using AccessManager.Api.Repository.Interface;
using AccessManager.Api.Services.Interface;
using Moq;

namespace AccessManager.Api.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IEncryptionPasswordService> _encryptionPasswordServiceMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _encryptionPasswordServiceMock = new Mock<IEncryptionPasswordService>();
            _userService = new UserService(_userRepositoryMock.Object, _encryptionPasswordServiceMock.Object);
        }

        // Arrange comum
        UserDTO userDto = new UserDTO
        {
            Email = "new@email.com",
            Password = "password123",
            Name = "New User",
            CompanyDTO = new CompanyDTO { Name = "New Company", Number = "654321" }
        };

        [Fact]
        public async Task Create_UserAlreadyExists_ReturnsFailure()
        {
            // Arrange
            var userDto = new UserDTO
            {
                Email = "existing@email.com",
                Password = "password123",
                Name = "Existing User",
                CompanyDTO = new CompanyDTO { Name = "Company", Number = "123456" }
            };

            var existingUser = new User { Email = "existing@email.com" };

            _userRepositoryMock
                .Setup(repo => repo.GetByEmail(userDto.Email))
                .ReturnsAsync(existingUser);

            // Act
            var result = await _userService.Create(userDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("User already exist", result.ErrorMessage);
            Assert.Equal(ErrorCode.USER_ALREADY_EXISTS, result.ErrorCode);
        }

        [Fact]
        public async Task Create_ValidUser_ReturnsSuccess()
        {
            // Arrange
            _userRepositoryMock
                .Setup(repo => repo.GetByEmail(userDto.Email))
                .ReturnsAsync((User?)null);

            _encryptionPasswordServiceMock
                .Setup(service => service.EncryptPassword(userDto.Password))
                .Returns("encrypted_password");

            _userRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userService.Create(userDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Equal(userDto.Email, result.Data.Email);
            Assert.Equal("encrypted_password", result.Data.Password);
            Assert.Equal(userDto.Name, result.Data.Name);
            Assert.Contains(Roles.admin.ToString(), result.Data.Roles);
            Assert.NotNull(result.Data.Company);
            Assert.Equal(userDto.CompanyDTO.Name, result.Data.Company.Name);
        }

        [Fact]
        public async Task Create_RepositoryThrowsException_ReturnsDatabaseError()
        {
            // Arrange
            _userRepositoryMock
                .Setup(repo => repo.GetByEmail(userDto.Email))
                .ReturnsAsync((User?)null);

            _encryptionPasswordServiceMock
                .Setup(service => service.EncryptPassword(userDto.Password))
                .Returns("encrypted_password");

            _userRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<User>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _userService.Create(userDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("An error occurred while creating the user.", result.ErrorMessage);
            Assert.Equal(ErrorCode.DATABASE_ERROR, result.ErrorCode);
        }

        [Fact]
        public async Task Create_PasswordIsEncrypted_CallsEncryptionService()
        {
            // Arrange
            _userRepositoryMock
                .Setup(repo => repo.GetByEmail(userDto.Email))
                .ReturnsAsync((User?)null);

            _encryptionPasswordServiceMock
                .Setup(service => service.EncryptPassword(userDto.Password))
                .Returns("encrypted_password")
                .Verifiable();

            _userRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userService.Create(userDto);

            // Assert
            _encryptionPasswordServiceMock.Verify(
                service => service.EncryptPassword(userDto.Password),
                Times.Once);
        }

        [Fact]
        public async Task Create_UserHasAdminRole_ByDefault()
        {
            // Arrange
            _userRepositoryMock
                .Setup(repo => repo.GetByEmail(userDto.Email))
                .ReturnsAsync((User?)null);

            _encryptionPasswordServiceMock
                .Setup(service => service.EncryptPassword(userDto.Password))
                .Returns("encrypted_password");

            _userRepositoryMock
                .Setup(repo => repo.Create(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userService.Create(userDto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Contains(Roles.admin.ToString(), result.Data.Roles);
            Assert.Single(result.Data.Roles);
        }
    }
}