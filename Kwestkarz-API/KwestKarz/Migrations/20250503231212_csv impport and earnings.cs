using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwestKarz.Migrations
{
    /// <inheritdoc />
    public partial class csvimpportandearnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripEarnings",
                schema: "kwestkarzbusinessdata",
                columns: table => new
                {
                    ReservationID = table.Column<string>(type: "text", nullable: false),
                    Guest = table.Column<string>(type: "text", nullable: false),
                    Vehicle = table.Column<string>(type: "text", nullable: false),
                    VehicleName = table.Column<string>(type: "text", nullable: false),
                    TripStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TripEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PickupLocation = table.Column<string>(type: "text", nullable: false),
                    ReturnLocation = table.Column<string>(type: "text", nullable: false),
                    TripStatus = table.Column<string>(type: "text", nullable: false),
                    TotalEarnings = table.Column<decimal>(type: "numeric", nullable: true),
                    ImportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RowHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripEarnings", x => x.ReservationID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripEarnings",
                schema: "kwestkarzbusinessdata");
        }
    }
}
