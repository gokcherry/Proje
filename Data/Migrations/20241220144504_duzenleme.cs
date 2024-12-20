using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class duzenleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { 

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalisanGelir_Calisanlar_calisanID",
                table: "CalisanGelir");

            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_UzmanlikAlanlari_uzmanlikID",
                table: "Calisanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Musteriler_musteriID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_UzmanlikAlanlari_uzmanlikID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_musteriID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_uzmanlikID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_uzmanlikID",
                table: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_CalisanGelir_calisanID",
                table: "CalisanGelir");

            migrationBuilder.DropColumn(
                name: "musteriID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "uzmanlikID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "uzmanlikID",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "calisanID",
                table: "CalisanGelir");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_musteri_ID",
                table: "Randevular",
                column: "musteri_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_uzmanlik_ID",
                table: "Randevular",
                column: "uzmanlik_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_uzmanlik_ID",
                table: "Calisanlar",
                column: "uzmanlik_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanGelir_calisan_ID",
                table: "CalisanGelir",
                column: "calisan_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanGelir_Calisanlar_calisan_ID",
                table: "CalisanGelir",
                column: "calisan_ID",
                principalTable: "Calisanlar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_UzmanlikAlanlari_uzmanlik_ID",
                table: "Calisanlar",
                column: "uzmanlik_ID",
                principalTable: "UzmanlikAlanlari",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Musteriler_musteri_ID",
                table: "Randevular",
                column: "musteri_ID",
                principalTable: "Musteriler",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_UzmanlikAlanlari_uzmanlik_ID",
                table: "Randevular",
                column: "uzmanlik_ID",
                principalTable: "UzmanlikAlanlari",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
