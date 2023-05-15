using GarageGenius.Modules.Users.Core.Commands.SignUp;
using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.Repositories;
using GarageGenius.Shared.Abstractions.Authentication.PasswordManager;
using GarageGenius.Shared.Infrastructure.MessageBroker;
using Moq;

namespace GarageGenius.Modules.Users.Tests;

public class UserCreatedCommandHandlerTest
{
    private readonly Mock<Serilog.ILogger> _logger;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IRoleRepository> _roleRepository;
    private readonly Mock<IPasswordManager> _passwordManager;
    private readonly Mock<IMessageBroker> _messageBroker;

    public UserCreatedCommandHandlerTest()
    {
        _logger = new Mock<Serilog.ILogger>();
        _userRepository = new Mock<IUserRepository>();
        _roleRepository = new Mock<IRoleRepository>();
        _passwordManager = new Mock<IPasswordManager>();
        _messageBroker = new Mock<IMessageBroker>();
    }

    [Fact]
    public async Task Handle_Should_ThrowException_WhenEmailIsNotUnique()
    {
        // Arrange
        var command = new SignUpCommand("krystianpetek2@gmail.com", "Password!23", "Administrator");

        var handler = new SignUpCommandHandler(_logger.Object, _userRepository.Object, _roleRepository.Object, _passwordManager.Object, _messageBroker.Object);

        // Act
        var exception = await Assert.ThrowsAsync<EmailAlreadyRegisteredException>(() => handler.HandleAsync(command, default));

        // Assert
    }
}