using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KwestKarz.Migrations
{
    /// <inheritdoc />
    public partial class apikeystore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiClientKeys",
                schema: "kwestkarzbusinessdata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    KeyHash = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DateIssued = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LastUsed = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiClientKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApiClientClaims",
                schema: "kwestkarzbusinessdata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    ApiClientKeyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiClientClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiClientClaims_ApiClientKeys_ApiClientKeyId",
                        column: x => x.ApiClientKeyId,
                        principalSchema: "kwestkarzbusinessdata",
                        principalTable: "ApiClientKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiClientClaims_ApiClientKeyId",
                schema: "kwestkarzbusinessdata",
                table: "ApiClientClaims",
                column: "ApiClientKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiClientClaims",
                schema: "kwestkarzbusinessdata");

            migrationBuilder.DropTable(
                name: "ApiClientKeys",
                schema: "kwestkarzbusinessdata");
        }
    }
}
