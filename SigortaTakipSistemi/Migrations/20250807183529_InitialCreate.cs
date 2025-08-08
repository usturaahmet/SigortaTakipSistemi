using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigortaTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsimSoyisim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Eposta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SigortaSirketleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SigortaSirketleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliceTurleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SigortaSirketiId = table.Column<int>(type: "int", nullable: false),
                    Turu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcentaPrimi = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Prim = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceTurleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceTurleri_SigortaSirketleri_SigortaSirketiId",
                        column: x => x.SigortaSirketiId,
                        principalTable: "SigortaSirketleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Policeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoliceNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kisi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TcNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    TelefonNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SigortaSirketiId = table.Column<int>(type: "int", nullable: false),
                    PoliceTuruId = table.Column<int>(type: "int", nullable: false),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    TanzimTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PoliceSuresi = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policeler_Kullanicilar_PersonelId",
                        column: x => x.PersonelId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policeler_PoliceTurleri_PoliceTuruId",
                        column: x => x.PoliceTuruId,
                        principalTable: "PoliceTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policeler_SigortaSirketleri_SigortaSirketiId",
                        column: x => x.SigortaSirketiId,
                        principalTable: "SigortaSirketleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_KullaniciAdi",
                table: "Kullanicilar",
                column: "KullaniciAdi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Policeler_PersonelId",
                table: "Policeler",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_Policeler_PoliceTuruId",
                table: "Policeler",
                column: "PoliceTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_Policeler_SigortaSirketiId",
                table: "Policeler",
                column: "SigortaSirketiId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceTurleri_SigortaSirketiId",
                table: "PoliceTurleri",
                column: "SigortaSirketiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Policeler");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "PoliceTurleri");

            migrationBuilder.DropTable(
                name: "SigortaSirketleri");
        }
    }
}
