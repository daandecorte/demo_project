using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Demo.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "City");

            migrationBuilder.EnsureSchema(
                name: "Country");

            migrationBuilder.CreateTable(
                name: "tblCountries",
                schema: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblCities",
                schema: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Population = table.Column<long>(type: "bigint", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblCities_tblCountries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Country",
                        principalTable: "tblCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Country",
                table: "tblCountries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "België" },
                    { 2, "Nederland" },
                    { 3, "Frankrijk" },
                    { 4, "Duitsland" }
                });

            migrationBuilder.InsertData(
                schema: "City",
                table: "tblCities",
                columns: new[] { "Id", "CountryId", "Name", "Population" },
                values: new object[,]
                {
                    { -4, 1, "Brugge", 2000000L },
                    { -3, 1, "Gent", 5000000L },
                    { -2, 1, "Brussel", 20000000L },
                    { -1, 1, "Antwerpen", 10000000L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCities_CountryId",
                schema: "City",
                table: "tblCities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblCities_Id",
                schema: "City",
                table: "tblCities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblCountries_Id",
                schema: "Country",
                table: "tblCountries",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblCities",
                schema: "City");

            migrationBuilder.DropTable(
                name: "tblCountries",
                schema: "Country");
        }
    }
}
