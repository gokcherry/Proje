using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class Register : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KullanicilarId",
                table: "Randevular",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_KullanicilarId",
                table: "Randevular",
                column: "KullanicilarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_AspNetUsers_KullanicilarId",
                table: "Randevular",
                column: "KullanicilarId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_AspNetUsers_KullanicilarId",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_KullanicilarId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "KullanicilarId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "AspNetUsers");
        }
    }
}
