namespace GarageGenius.Shared.Abstractions.Authentication.PasswordManager;
public interface IPasswordManager
{
    string Generate(string password);
    bool IsValid(string password, string securedPassword);
}
