using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CalisanUzmanlik_CalisanID",
                table: "CalisanUzmanlik");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Musteriler");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUzmanlik_CalisanID",
                table: "CalisanUzmanlik",
                column: "CalisanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CalisanUzmanlik_CalisanID",
                table: "CalisanUzmanlik");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "Musteriler",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUzmanlik_CalisanID",
                table: "CalisanUzmanlik",
                column: "CalisanID",
                unique: true);
        }
    }
}
