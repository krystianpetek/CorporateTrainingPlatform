using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Reservations.Infrastructure.Persistance.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "reservations");

        migrationBuilder.CreateTable(
            name: "Reservations",
            schema: "reservations",
            columns: table => new
            {
                ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                VehicleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ReservationState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                ReservationNote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reservations", x => x.ReservationId);
            });

        migrationBuilder.CreateTable(
            name: "ReservationHistories",
            schema: "reservations",
            columns: table => new
            {
                ReservationHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ReservationHistories", x => x.ReservationHistoryId);
                table.ForeignKey(
                    name: "FK_ReservationHistories_Reservations_ReservationId",
                    column: x => x.ReservationId,
                    principalSchema: "reservations",
                    principalTable: "Reservations",
                    principalColumn: "ReservationId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ReservationHistories_ReservationId",
            schema: "reservations",
            table: "ReservationHistories",
            column: "ReservationId");

        migrationBuilder.CreateIndex(
            name: "IX_Reservations_VehicleId",
            schema: "reservations",
            table: "Reservations",
            column: "VehicleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ReservationHistories",
            schema: "reservations");

        migrationBuilder.DropTable(
            name: "Reservations",
            schema: "reservations");
    }
}
