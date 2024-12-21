using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class Yeni2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalisanGelir_Calisanlar_CalisanID",
                table: "CalisanGelir");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanUzmanlik_Calisanlar_CalisanID",
                table: "CalisanUzmanlik");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanID",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calisanlar",
                table: "Calisanlar");

            migrationBuilder.RenameTable(
                name: "Calisanlar",
                newName: "Calisan");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calisan",
                table: "Calisan",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanGelir_Calisan_CalisanID",
                table: "CalisanGelir",
                column: "CalisanID",
                principalTable: "Calisan",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanUzmanlik_Calisan_CalisanID",
                table: "CalisanUzmanlik",
                column: "CalisanID",
                principalTable: "Calisan",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisan_CalisanID",
                table: "Randevular",
                column: "CalisanID",
                principalTable: "Calisan",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalisanGelir_Calisan_CalisanID",
                table: "CalisanGelir");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanUzmanlik_Calisan_CalisanID",
                table: "CalisanUzmanlik");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisan_CalisanID",
                table: "Randevular");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Calisan",
                table: "Calisan");

            migrationBuilder.RenameTable(
                name: "Calisan",
                newName: "Calisanlar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Calisanlar",
                table: "Calisanlar",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanGelir_Calisanlar_CalisanID",
                table: "CalisanGelir",
                column: "CalisanID",
                principalTable: "Calisanlar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanUzmanlik_Calisanlar_CalisanID",
                table: "CalisanUzmanlik",
                column: "CalisanID",
                principalTable: "Calisanlar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanID",
                table: "Randevular",
                column: "CalisanID",
                principalTable: "Calisanlar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
