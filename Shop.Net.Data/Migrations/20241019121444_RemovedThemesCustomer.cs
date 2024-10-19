using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Net.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedThemesCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DarkModeEnabled",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DarkModeEnabled",
                table: "Customers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
