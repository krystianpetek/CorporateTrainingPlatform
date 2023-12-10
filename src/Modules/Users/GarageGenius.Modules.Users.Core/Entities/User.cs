using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Users.Core.Entities;
internal sealed class User : AuditableEntity
{
    internal Guid UserId { get; } // TODO userid as Types
    internal Guid CustomerId { get; } // TODO customerId as Types
    public string RoleName { get; } // TODO ValueObject
    public EmailAddress Email { get; }
    public string Password { get; private set; } // TODO maybe ValueObject
	public UserState State { get; private set; }
    public Role Role { get; set; }

    public User(EmailAddress email, string password, string role)
	{
		UserId = Guid.NewGuid();
		CustomerId = Guid.NewGuid();
		Email = email;
		Password = password;
		RoleName = role;
		Activate();
	}

	private User() { }

	internal User Deactivate()
	{
		State = UserState.Unactive;
		return this;
	}

	private User Activate()
	{
		State = UserState.Active;
		return this;
	}

	internal void VerifyUserState()
	{
		if (State != UserState.Active.Value)
			throw new UserInactiveStateException(this.UserId); // TODO - domain entity should throw exception ?
	}

	internal void ChangePassword(string newPassword)
	{
		Password = newPassword;
	}
}