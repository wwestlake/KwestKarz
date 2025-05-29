using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KwestKarz.Migrations
{
    /// <inheritdoc />
    public partial class AddCrmEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guests",
                schema: "kwestkarzbusinessdata",
                columns: table => new
                {
                    GuestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    TuroUsername = table.Column<string>(type: "text", nullable: false),
                    InternalRating = table.Column<int>(type: "integer", nullable: true),
                    IsVIP = table.Column<bool>(type: "boolean", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.GuestId);
                });

            migrationBuilder.CreateTable(
                name: "ContactLogs",
                schema: "kwestkarzbusinessdata",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuestId = table.Column<int>(type: "integer", nullable: false),
                    ContactType = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_ContactLogs_Guests_GuestId",
                        column: x => x.GuestId,
                        principalSchema: "kwestkarzbusinessdata",
                        principalTable: "Guests",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OutstandingCharges",
                schema: "kwestkarzbusinessdata",
                columns: table => new
                {
                    ChargeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuestId = table.Column<int>(type: "integer", nullable: false),
                    TripId = table.Column<int>(type: "integer", nullable: true),
                    ChargeType = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DateIncurred = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateResolved = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutstandingCharges", x => x.ChargeId);
                    table.ForeignKey(
                        name: "FK_OutstandingCharges_Guests_GuestId",
                        column: x => x.GuestId,
                        principalSchema: "kwestkarzbusinessdata",
                        principalTable: "Guests",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                schema: "kwestkarzbusinessdata",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuestId = table.Column<int>(type: "integer", nullable: false),
                    VehicleId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Earnings = table.Column<decimal>(type: "numeric", nullable: false),
                    MilesDriven = table.Column<int>(type: "integer", nullable: false),
                    LateReturn = table.Column<bool>(type: "boolean", nullable: false),
                    DamageReported = table.Column<bool>(type: "boolean", nullable: false),
                    TollFlag = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.ForeignKey(
                        name: "FK_Trips_Guests_GuestId",
                        column: x => x.GuestId,
                        principalSchema: "kwestkarzbusinessdata",
                        principalTable: "Guests",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactLogs_GuestId",
                schema: "kwestkarzbusinessdata",
                table: "ContactLogs",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_OutstandingCharges_GuestId",
                schema: "kwestkarzbusinessdata",
                table: "OutstandingCharges",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_GuestId",
                schema: "kwestkarzbusinessdata",
                table: "Trips",
                column: "GuestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactLogs",
                schema: "kwestkarzbusinessdata");

            migrationBuilder.DropTable(
                name: "OutstandingCharges",
                schema: "kwestkarzbusinessdata");

            migrationBuilder.DropTable(
                name: "Trips",
                schema: "kwestkarzbusinessdata");

            migrationBuilder.DropTable(
                name: "Guests",
                schema: "kwestkarzbusinessdata");
        }
    }
}
