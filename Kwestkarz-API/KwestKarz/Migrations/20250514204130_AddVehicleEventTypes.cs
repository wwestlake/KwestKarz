using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwestKarz.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleEventTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClaimId",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComponentAffected",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaintenanceEntry_Cost",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents",
                type: "numeric(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepairType",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReportedToTuro",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Severity",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShopName",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClaimId",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "ComponentAffected",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "MaintenanceEntry_Cost",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "RepairType",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "ReportedToTuro",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "Severity",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents");

            migrationBuilder.DropColumn(
                name: "ShopName",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents");
        }
    }
}
