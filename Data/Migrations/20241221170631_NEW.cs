﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class NEW : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Calisan");

            migrationBuilder.CreateTable(
                name: "Calisanlar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisanlar", x => x.ID);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Calisanlar");

            migrationBuilder.CreateTable(
                name: "Calisan",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calisan", x => x.ID);
                });

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
    }
}
