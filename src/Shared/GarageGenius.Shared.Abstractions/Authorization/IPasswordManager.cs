namespace GarageGenius.Shared.Abstractions.Authorization;
public interface IPasswordManager
{
    string Generate(string password);
    bool IsValid(string password, string securedPassword);
}
