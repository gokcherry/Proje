using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class uzmanlikEkleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UzmanlikAlanlari",
                columns: new[] { "ID", "ad", "fiyat", "sure" },
                values: new object[,]
                {
                    { 1, "Saç Kesimi", 500m, 30 },
                    { 2, "Saç Boyama ", 4000m, 3 },
                    { 3, "Saç Şekillendirme ", 400m, 30 },
                    { 4, "Makyaj ", 600m, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UzmanlikAlanlari",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UzmanlikAlanlari",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UzmanlikAlanlari",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UzmanlikAlanlari",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
