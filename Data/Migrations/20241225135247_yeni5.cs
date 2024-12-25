using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class yeni5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_AspNetUsers_KullanicilarId",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Musteriler_MusteriID",
                table: "Randevular");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_KullanicilarId",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "KullanicilarId",
                table: "Randevular");

            migrationBuilder.AlterColumn<string>(
                name: "MusteriID",
                table: "Randevular",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_AspNetUsers_MusteriID",
                table: "Randevular",
                column: "MusteriID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_AspNetUsers_MusteriID",
                table: "Randevular");

            migrationBuilder.AlterColumn<int>(
                name: "MusteriID",
                table: "Randevular",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "KullanicilarId",
                table: "Randevular",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.ID);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Musteriler_MusteriID",
                table: "Randevular",
                column: "MusteriID",
                principalTable: "Musteriler",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
