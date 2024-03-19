namespace GarageGenius.Modules.Notifications.Core.Services;

public interface IPdfGeneratorService
{
    Task<byte[]> GeneratePdfAsync(string content);
}