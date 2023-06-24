using GarageGenius.Modules.Users.Core.Exceptions;
using GarageGenius.Modules.Users.Core.ValueObjects;
using GarageGenius.Shared.Abstractions.Common;

namespace GarageGenius.Modules.Users.Core.Entities;
internal sealed class User : AuditableEntity
{
	internal Guid UserId { get; private set; } // TODO userid as Types
	internal Guid CustomerId { get; private set; } // TODO customerId as Types
	public string RoleName { get; private set; } // TODO ValueObject
	public EmailAddress Email { get; private set; }
	public string Password { get; private set; } // TODO maybe ValueObject
	public UserState State { get; private set; }
	public Role Role { get; private set; }

	public User(EmailAddress email, string password, string role)
	{
		UserId = Guid.NewGuid();
		CustomerId = Guid.NewGuid();
		Email = email;
		Password = password;
		RoleName = role;
		this.Activate();
	}

	private User() { }

	internal void Deactivate()
	{
		State = UserState.Unactive;
	}

	internal void Activate()
	{
		State = UserState.Active;
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