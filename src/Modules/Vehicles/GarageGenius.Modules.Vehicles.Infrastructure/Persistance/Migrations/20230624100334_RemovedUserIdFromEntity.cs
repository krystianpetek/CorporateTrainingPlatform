using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Vehicles.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class RemovedUserIdFromEntity : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "UserId",
			schema: "vehicles",
			table: "Vehicles");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<Guid>(
			name: "UserId",
			schema: "vehicles",
			table: "Vehicles",
			type: "uniqueidentifier",
			nullable: true);
	}
}
