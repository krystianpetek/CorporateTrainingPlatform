using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Reservations.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class AddedReservationDeleteProperty : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<bool>(
			name: "ReservationDeleted",
			schema: "reservations",
			table: "Reservations",
			type: "bit",
			nullable: false,
			defaultValue: false);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "ReservationDeleted",
			schema: "reservations",
			table: "Reservations");
	}
}
