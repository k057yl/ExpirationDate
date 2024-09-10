using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpirationDate.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyCodeToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Items");
        }
    }
}
