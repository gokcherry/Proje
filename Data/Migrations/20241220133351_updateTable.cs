using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_calisanID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_calisanID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "calisanID",
                table: "Randevular");

            migrationBuilder.UpdateData(
                table: "UzmanlikAlanlari",
                keyColumn: "ID",
                keyValue: 2,
                column: "ad",
                value: "Saç Boyama");

            migrationBuilder.UpdateData(
                table: "UzmanlikAlanlari",
                keyColumn: "ID",
                keyValue: 3,
                column: "ad",
                value: "Saç Şekillendirme");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_calisanID",
                table: "Randevular",
                column: "calisan_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_calisanID",
                table: "Randevular",
                column: "calisan_ID",
                principalTable: "Calisanlar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_calisanID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_Randevular_calisan_ID",
                table: "Randevular");

            migrationBuilder.AddColumn<int>(
                name: "calisanID",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "UzmanlikAlanlari",
                keyColumn: "ID",
                keyValue: 2,
                column: "ad",
                value: "Saç Boyama ");

            migrationBuilder.UpdateData(
                table: "UzmanlikAlanlari",
                keyColumn: "ID",
                keyValue: 3,
                column: "ad",
                value: "Saç Şekillendirme ");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_calisanID",
                table: "Randevular",
                column: "calisanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_calisanID",
                table: "Randevular",
                column: "calisanID",
                principalTable: "Calisanlar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
