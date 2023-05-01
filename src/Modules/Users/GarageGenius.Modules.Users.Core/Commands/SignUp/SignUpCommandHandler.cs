using GarageGenius.Shared.Abstractions.Commands;

namespace GarageGenius.Modules.Users.Core.Commands.SignUp;
internal class SignUpCommandHandler : ICommandHandler<SignUpCommand>
{
    public Task HandleAsync(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
