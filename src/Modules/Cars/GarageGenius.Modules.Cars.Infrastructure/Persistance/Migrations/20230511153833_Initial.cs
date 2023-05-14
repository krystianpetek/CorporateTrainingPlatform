using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Cars.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "cars");

        migrationBuilder.CreateTable(
            name: "Cars",
            schema: "cars",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Manufacturer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Year = table.Column<int>(type: "int", maxLength: 8, nullable: true),
                Vin = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true),
                LicensePlate = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cars", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Cars_Id",
            schema: "cars",
            table: "Cars",
            column: "Id");

        migrationBuilder.CreateIndex(
            name: "IX_Cars_Vin",
            schema: "cars",
            table: "Cars",
            column: "Vin",
            unique: true,
            filter: "[Vin] IS NOT NULL");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Cars",
            schema: "cars");
    }
}
