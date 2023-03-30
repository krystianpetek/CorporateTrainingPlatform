using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace GarageGenius;

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
                Title = "CorporateTrainingPlatform"
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
                name: "CorporateTrainingPlatform");
        });

        app.MapControllers();

        await app.RunAsync();
    }
}
