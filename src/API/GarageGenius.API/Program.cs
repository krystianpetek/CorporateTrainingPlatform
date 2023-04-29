using Microsoft.OpenApi.Models;

namespace GarageGenius.API;

public static class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen((swagger) =>
        {
            swagger.SwaggerDoc(name: "v1", info: new OpenApiInfo
            {
                Version = "v1",
                Title = "GeniusGarage"
            });
        });
        builder.Services.AddControllers();

        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI((swagger) =>
        {
            swagger.SwaggerEndpoint(
                url: "/swagger/v1/swagger.json",
                name: "GeniusGarage");
        });

        app.MapControllers();

        await app.RunAsync();
    }
}
