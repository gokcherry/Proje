using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proje.Data.Migrations
{
    /// <inheritdoc />
    public partial class Yeni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalisanGelir_Calisanlar_calisanID",
                table: "CalisanGelir");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanUzmanlik_Calisanlar_calisanID",
                table: "CalisanUzmanlik");

            migrationBuilder.DropForeignKey(
                name: "FK_CalisanUzmanlik_UzmanlikAlanlari_uzmanlikID",
                table: "CalisanUzmanlik");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_calisanID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Musteriler_musteriID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_UzmanlikAlanlari_uzmanlikID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_CalisanUzmanlik_calisanID",
                table: "CalisanUzmanlik");

            migrationBuilder.DropColumn(
                name: "salon_ad",
                table: "Salon");

            migrationBuilder.DropColumn(
                name: "calisan_ID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "musteri_ID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "uzmanlik_ID",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "kayit_tarih",
                table: "Musteriler");

            migrationBuilder.DropColumn(
                name: "calisan_ID",
                table: "CalisanUzmanlik");

            migrationBuilder.DropColumn(
                name: "uzmanlik_ID",
                table: "CalisanUzmanlik");

            migrationBuilder.DropColumn(
                name: "calisan_ad",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "calisan_soyad",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "katilim_tarih",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "calisan_ID",
                table: "CalisanGelir");

            migrationBuilder.RenameColumn(
                name: "sure",
                table: "UzmanlikAlanlari",
                newName: "Sure");

            migrationBuilder.RenameColumn(
                name: "fiyat",
                table: "UzmanlikAlanlari",
                newName: "Fiyat");

            migrationBuilder.RenameColumn(
                name: "ad",
                table: "UzmanlikAlanlari",
                newName: "Ad");

            migrationBuilder.RenameColumn(
                name: "gun",
                table: "Salon",
                newName: "Gun");

            migrationBuilder.RenameColumn(
                name: "kapanis_saati",
                table: "Salon",
                newName: "KapanisSaati");

            migrationBuilder.RenameColumn(
                name: "acilis_saati",
                table: "Salon",
                newName: "AcilisSaati");

            migrationBuilder.RenameColumn(
                name: "uzmanlikID",
                table: "Randevular",
                newName: "UzmanlikID");

            migrationBuilder.RenameColumn(
                name: "musteriID",
                table: "Randevular",
                newName: "MusteriID");

            migrationBuilder.RenameColumn(
                name: "durum",
                table: "Randevular",
                newName: "Durum");

            migrationBuilder.RenameColumn(
                name: "calisanID",
                table: "Randevular",
                newName: "CalisanID");

            migrationBuilder.RenameColumn(
                name: "toplam_fiyat",
                table: "Randevular",
                newName: "ToplamFiyat");

            migrationBuilder.RenameColumn(
                name: "randevu_tarih",
                table: "Randevular",
                newName: "RandevuTarihi");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_uzmanlikID",
                table: "Randevular",
                newName: "IX_Randevular_UzmanlikID");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_musteriID",
                table: "Randevular",
                newName: "IX_Randevular_MusteriID");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_calisanID",
                table: "Randevular",
                newName: "IX_Randevular_CalisanID");

            migrationBuilder.RenameColumn(
                name: "telefon",
                table: "Musteriler",
                newName: "Telefon");

            migrationBuilder.RenameColumn(
                name: "soyad",
                table: "Musteriler",
                newName: "Soyad");

            migrationBuilder.RenameColumn(
                name: "sifre",
                table: "Musteriler",
                newName: "Sifre");

            migrationBuilder.RenameColumn(
                name: "rol",
                table: "Musteriler",
                newName: "Rol");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Musteriler",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "ad",
                table: "Musteriler",
                newName: "Ad");

            migrationBuilder.RenameColumn(
                name: "uzmanlikID",
                table: "CalisanUzmanlik",
                newName: "UzmanlikID");

            migrationBuilder.RenameColumn(
                name: "calisanID",
                table: "CalisanUzmanlik",
                newName: "CalisanID");

            migrationBuilder.RenameIndex(
                name: "IX_CalisanUzmanlik_uzmanlikID",
                table: "CalisanUzmanlik",
                newName: "IX_CalisanUzmanlik_UzmanlikID");

            migrationBuilder.RenameColumn(
                name: "telefon",
                table: "Calisanlar",
                newName: "Telefon");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Calisanlar",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "tarih",
                table: "CalisanGelir",
                newName: "Tarih");

            migrationBuilder.RenameColumn(
                name: "calisanID",
                table: "CalisanGelir",
                newName: "CalisanID");

            migrationBuilder.RenameColumn(
                name: "toplam_gelir",
                table: "CalisanGelir",
                newName: "ToplamGelir");

            migrationBuilder.RenameIndex(
                name: "IX_CalisanGelir_calisanID",
                table: "CalisanGelir",
                newName: "IX_CalisanGelir_CalisanID");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "UzmanlikAlanlari",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Gun",
                table: "Salon",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "Salon",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Durum",
                table: "Randevular",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Soyad",
                table: "Musteriler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Sifre",
                table: "Musteriler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Musteriler",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ad",
                table: "Musteriler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Ad",
                table: "Calisanlar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Soyad",
                table: "Calisanlar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUzmanlik_CalisanID",
                table: "CalisanUzmanlik",
                column: "CalisanID",
                unique: true);

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
                name: "FK_CalisanUzmanlik_UzmanlikAlanlari_UzmanlikID",
                table: "CalisanUzmanlik",
                column: "UzmanlikID",
                principalTable: "UzmanlikAlanlari",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanID",
                table: "Randevular",
                column: "CalisanID",
                principalTable: "Calisanlar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Musteriler_MusteriID",
                table: "Randevular",
                column: "MusteriID",
                principalTable: "Musteriler",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_UzmanlikAlanlari_UzmanlikID",
                table: "Randevular",
                column: "UzmanlikID",
                principalTable: "UzmanlikAlanlari",
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
                name: "FK_CalisanUzmanlik_UzmanlikAlanlari_UzmanlikID",
                table: "CalisanUzmanlik");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Calisanlar_CalisanID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_Musteriler_MusteriID",
                table: "Randevular");

            migrationBuilder.DropForeignKey(
                name: "FK_Randevular_UzmanlikAlanlari_UzmanlikID",
                table: "Randevular");

            migrationBuilder.DropIndex(
                name: "IX_CalisanUzmanlik_CalisanID",
                table: "CalisanUzmanlik");

            migrationBuilder.DropColumn(
                name: "Ad",
                table: "Salon");

            migrationBuilder.DropColumn(
                name: "Ad",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "Soyad",
                table: "Calisanlar");

            migrationBuilder.RenameColumn(
                name: "Sure",
                table: "UzmanlikAlanlari",
                newName: "sure");

            migrationBuilder.RenameColumn(
                name: "Fiyat",
                table: "UzmanlikAlanlari",
                newName: "fiyat");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "UzmanlikAlanlari",
                newName: "ad");

            migrationBuilder.RenameColumn(
                name: "Gun",
                table: "Salon",
                newName: "gun");

            migrationBuilder.RenameColumn(
                name: "KapanisSaati",
                table: "Salon",
                newName: "kapanis_saati");

            migrationBuilder.RenameColumn(
                name: "AcilisSaati",
                table: "Salon",
                newName: "acilis_saati");

            migrationBuilder.RenameColumn(
                name: "UzmanlikID",
                table: "Randevular",
                newName: "uzmanlikID");

            migrationBuilder.RenameColumn(
                name: "MusteriID",
                table: "Randevular",
                newName: "musteriID");

            migrationBuilder.RenameColumn(
                name: "Durum",
                table: "Randevular",
                newName: "durum");

            migrationBuilder.RenameColumn(
                name: "CalisanID",
                table: "Randevular",
                newName: "calisanID");

            migrationBuilder.RenameColumn(
                name: "ToplamFiyat",
                table: "Randevular",
                newName: "toplam_fiyat");

            migrationBuilder.RenameColumn(
                name: "RandevuTarihi",
                table: "Randevular",
                newName: "randevu_tarih");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_UzmanlikID",
                table: "Randevular",
                newName: "IX_Randevular_uzmanlikID");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_MusteriID",
                table: "Randevular",
                newName: "IX_Randevular_musteriID");

            migrationBuilder.RenameIndex(
                name: "IX_Randevular_CalisanID",
                table: "Randevular",
                newName: "IX_Randevular_calisanID");

            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "Musteriler",
                newName: "telefon");

            migrationBuilder.RenameColumn(
                name: "Soyad",
                table: "Musteriler",
                newName: "soyad");

            migrationBuilder.RenameColumn(
                name: "Sifre",
                table: "Musteriler",
                newName: "sifre");

            migrationBuilder.RenameColumn(
                name: "Rol",
                table: "Musteriler",
                newName: "rol");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Musteriler",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Musteriler",
                newName: "ad");

            migrationBuilder.RenameColumn(
                name: "UzmanlikID",
                table: "CalisanUzmanlik",
                newName: "uzmanlikID");

            migrationBuilder.RenameColumn(
                name: "CalisanID",
                table: "CalisanUzmanlik",
                newName: "calisanID");

            migrationBuilder.RenameIndex(
                name: "IX_CalisanUzmanlik_UzmanlikID",
                table: "CalisanUzmanlik",
                newName: "IX_CalisanUzmanlik_uzmanlikID");

            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "Calisanlar",
                newName: "telefon");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Calisanlar",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Tarih",
                table: "CalisanGelir",
                newName: "tarih");

            migrationBuilder.RenameColumn(
                name: "CalisanID",
                table: "CalisanGelir",
                newName: "calisanID");

            migrationBuilder.RenameColumn(
                name: "ToplamGelir",
                table: "CalisanGelir",
                newName: "toplam_gelir");

            migrationBuilder.RenameIndex(
                name: "IX_CalisanGelir_CalisanID",
                table: "CalisanGelir",
                newName: "IX_CalisanGelir_calisanID");

            migrationBuilder.AlterColumn<string>(
                name: "ad",
                table: "UzmanlikAlanlari",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "gun",
                table: "Salon",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "salon_ad",
                table: "Salon",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "durum",
                table: "Randevular",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "calisan_ID",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "musteri_ID",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "uzmanlik_ID",
                table: "Randevular",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "soyad",
                table: "Musteriler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "sifre",
                table: "Musteriler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "rol",
                table: "Musteriler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ad",
                table: "Musteriler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "kayit_tarih",
                table: "Musteriler",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "calisan_ID",
                table: "CalisanUzmanlik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "uzmanlik_ID",
                table: "CalisanUzmanlik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "calisan_ad",
                table: "Calisanlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "calisan_soyad",
                table: "Calisanlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "katilim_tarih",
                table: "Calisanlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "calisan_ID",
                table: "CalisanGelir",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CalisanUzmanlik_calisanID",
                table: "CalisanUzmanlik",
                column: "calisanID");

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanGelir_Calisanlar_calisanID",
                table: "CalisanGelir",
                column: "calisanID",
                principalTable: "Calisanlar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanUzmanlik_Calisanlar_calisanID",
                table: "CalisanUzmanlik",
                column: "calisanID",
                principalTable: "Calisanlar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CalisanUzmanlik_UzmanlikAlanlari_uzmanlikID",
                table: "CalisanUzmanlik",
                column: "uzmanlikID",
                principalTable: "UzmanlikAlanlari",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Calisanlar_calisanID",
                table: "Randevular",
                column: "calisanID",
                principalTable: "Calisanlar",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_Musteriler_musteriID",
                table: "Randevular",
                column: "musteriID",
                principalTable: "Musteriler",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Randevular_UzmanlikAlanlari_uzmanlikID",
                table: "Randevular",
                column: "uzmanlikID",
                principalTable: "UzmanlikAlanlari",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
