using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageGenius.Modules.Users.Core.Migrations
{
    /// <inheritdoc />
    public partial class next : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                schema: "users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "users",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "users",
                table: "Roles");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                schema: "users",
                table: "Users",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "users",
                table: "Roles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "users",
                table: "Roles",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleName",
                schema: "users",
                table: "Users",
                column: "RoleName");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleName",
                schema: "users",
                table: "Users",
                column: "RoleName",
                principalSchema: "users",
                principalTable: "Roles",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleName",
                schema: "users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleName",
                schema: "users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "users",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleName",
                schema: "users",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "users",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                schema: "users",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "users",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "users",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                schema: "users",
                table: "Users",
                column: "RoleId",
                principalSchema: "users",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
