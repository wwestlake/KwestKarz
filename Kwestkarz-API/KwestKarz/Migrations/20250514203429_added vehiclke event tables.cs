using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwestKarz.Migrations
{
    /// <inheritdoc />
    public partial class addedvehiclkeeventtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleEvents",
                schema: "kwestkarzbusinessdata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Odometer = table.Column<int>(type: "integer", nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EventType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Inspector = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Result = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    InspectionType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ServiceType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    PerformedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleEvents_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "kwestkarzbusinessdata",
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEvents_VehicleId",
                schema: "kwestkarzbusinessdata",
                table: "VehicleEvents",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleEvents",
                schema: "kwestkarzbusinessdata");
        }
    }
}
