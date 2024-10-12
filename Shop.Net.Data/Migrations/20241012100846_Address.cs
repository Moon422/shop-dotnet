using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Net.Data.Migrations
{
    /// <inheritdoc />
    public partial class Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_StateProvinces_StateProvinceId",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "StateProvinceId",
                table: "Cities",
                newName: "DivisionId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_StateProvinceId",
                table: "Cities",
                newName: "IX_Cities_DivisionId");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fullname = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FlatNumber = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HouseNumber = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoadNumber = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address1 = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    StateProvinceId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_StateProvinces_StateProvinceId",
                        column: x => x.StateProvinceId,
                        principalTable: "StateProvinces",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DistrictId",
                table: "Addresses",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StateProvinceId",
                table: "Addresses",
                column: "StateProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_StateProvinces_DivisionId",
                table: "Cities",
                column: "DivisionId",
                principalTable: "StateProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_StateProvinces_DivisionId",
                table: "Cities");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.RenameColumn(
                name: "DivisionId",
                table: "Cities",
                newName: "StateProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_DivisionId",
                table: "Cities",
                newName: "IX_Cities_StateProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_StateProvinces_StateProvinceId",
                table: "Cities",
                column: "StateProvinceId",
                principalTable: "StateProvinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
