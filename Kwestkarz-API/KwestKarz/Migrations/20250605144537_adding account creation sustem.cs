using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KwestKarz.Migrations
{
    /// <inheritdoc />
    public partial class addingaccountcreationsustem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "kwestkarzbusinessdata",
                table: "UserAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "kwestkarzbusinessdata",
                table: "UserAccounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "kwestkarzbusinessdata",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "kwestkarzbusinessdata",
                table: "UserAccounts");
        }
    }
}
