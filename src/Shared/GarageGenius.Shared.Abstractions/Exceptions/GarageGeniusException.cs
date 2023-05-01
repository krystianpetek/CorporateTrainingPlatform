namespace GarageGenius.Shared.Abstractions.Exceptions;
public abstract class GarageGeniusException : Exception
{
    protected GarageGeniusException(string message) : base(message)
    {
    }
}