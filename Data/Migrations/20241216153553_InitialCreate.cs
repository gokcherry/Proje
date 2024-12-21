using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    calisan_ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    calisan_soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    katilim_tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kayit_tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Salon",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salon_ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gun = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acilis_saati = table.Column<TimeSpan>(type: "time", nullable: false),
                    kapanis_saati = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UzmanlikAlanlari",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sure = table.Column<int>(type: "int", nullable: false),
                    fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UzmanlikAlanlari", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CalisanGelir",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    calisan_ID = table.Column<int>(type: "int", nullable: false),
                    tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    toplam_gelir = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    calisanID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanGelir", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalisanGelir_Calisanlar_calisanID",
                        column: x => x.calisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalisanUzmanlik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    calisan_ID = table.Column<int>(type: "int", nullable: false),
                    uzmanlik_ID = table.Column<int>(type: "int", nullable: false),
                    calisanID = table.Column<int>(type: "int", nullable: false),
                    uzmanlikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalisanUzmanlik", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalisanUzmanlik_Calisanlar_calisanID",
                        column: x => x.calisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalisanUzmanlik_UzmanlikAlanlari_uzmanlikID",
                        column: x => x.uzmanlikID,
                        principalTable: "UzmanlikAlanlari",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Randevular",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    musteri_ID = table.Column<int>(type: "int", nullable: false),
                    calisan_ID = table.Column<int>(type: "int", nullable: false),
                    uzmanlik_ID = table.Column<int>(type: "int", nullable: false),
                    randevu_tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    toplam_fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    durum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    musteriID = table.Column<int>(type: "int", nullable: false),
                    calisanID = table.Column<int>(type: "int", nullable: false),
                    uzmanlikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Randevular", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Randevular_Calisanlar_calisanID",
                        column: x => x.calisanID,
                        principalTable: "Calisanlar",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_Musteriler_musteriID",
                        column: x => x.musteriID,
                        principalTable: "Musteriler",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Randevular_UzmanlikAlanlari_uzmanlikID",
                        column: x => x.uzmanlikID,
                        principalTable: "UzmanlikAlanlari",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalisanGelir_calisanID",
                table: "CalisanGelir",
                column: "calisanID");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUzmanlik_calisanID",
                table: "CalisanUzmanlik",
                column: "calisanID");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUzmanlik_uzmanlikID",
                table: "CalisanUzmanlik",
                column: "uzmanlikID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_calisanID",
                table: "Randevular",
                column: "calisanID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_musteriID",
                table: "Randevular",
                column: "musteriID");

            migrationBuilder.CreateIndex(
                name: "IX_Randevular_uzmanlikID",
                table: "Randevular",
                column: "uzmanlikID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalisanGelir");

            migrationBuilder.DropTable(
                name: "CalisanUzmanlik");

            migrationBuilder.DropTable(
                name: "Randevular");

            migrationBuilder.DropTable(
                name: "Salon");

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropTable(
                name: "UzmanlikAlanlari");
        }
    }
}
