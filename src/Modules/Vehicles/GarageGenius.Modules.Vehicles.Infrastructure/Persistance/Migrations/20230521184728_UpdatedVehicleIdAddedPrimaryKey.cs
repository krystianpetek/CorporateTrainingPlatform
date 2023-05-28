using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Vehicles.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class UpdatedVehicleIdAddedPrimaryKey : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			name: "Id",
			schema: "vehicles",
			table: "Vehicles",
			newName: "VehicleId");

		migrationBuilder.RenameIndex(
			name: "IX_Vehicles_Id",
			schema: "vehicles",
			table: "Vehicles",
			newName: "IX_Vehicles_VehicleId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.RenameColumn(
			name: "VehicleId",
			schema: "vehicles",
			table: "Vehicles",
			newName: "Id");

		migrationBuilder.RenameIndex(
			name: "IX_Vehicles_VehicleId",
			schema: "vehicles",
			table: "Vehicles",
			newName: "IX_Vehicles_Id");
	}
}
