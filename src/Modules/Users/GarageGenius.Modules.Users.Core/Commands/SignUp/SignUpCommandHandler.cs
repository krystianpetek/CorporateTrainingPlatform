using GarageGenius.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageGenius.Modules.Users.Core.Commands.SignUp;
internal class SignUpCommandHandler : ICommandHandler<SignUpCommand>
{
    public Task HandleAsync(SignUpCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
