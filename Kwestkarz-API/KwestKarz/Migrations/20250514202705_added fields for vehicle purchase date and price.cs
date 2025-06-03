using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwestKarz.Migrations
{
    /// <inheritdoc />
    public partial class addedfieldsforvehiclepurchasedateandprice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                schema: "kwestkarzbusinessdata",
                table: "Vehicles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                schema: "kwestkarzbusinessdata",
                table: "Vehicles",
                type: "numeric(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                schema: "kwestkarzbusinessdata",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                schema: "kwestkarzbusinessdata",
                table: "Vehicles");
        }
    }
}
