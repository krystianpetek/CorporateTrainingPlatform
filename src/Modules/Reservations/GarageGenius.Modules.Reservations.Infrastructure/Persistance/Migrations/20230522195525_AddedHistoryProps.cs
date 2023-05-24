using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Reservations.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class AddedHistoryProps : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Comment",
            schema: "reservations",
            table: "ReservationHistories",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "ReservationState",
            schema: "reservations",
            table: "ReservationHistories",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Comment",
            schema: "reservations",
            table: "ReservationHistories");

        migrationBuilder.DropColumn(
            name: "ReservationState",
            schema: "reservations",
            table: "ReservationHistories");
    }
}
