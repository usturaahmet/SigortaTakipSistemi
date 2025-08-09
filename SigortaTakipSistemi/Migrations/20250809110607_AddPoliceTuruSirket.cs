using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigortaTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddPoliceTuruSirket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policeler_Kullanicilar_PersonelId",
                table: "Policeler");

            migrationBuilder.DropForeignKey(
                name: "FK_Policeler_PoliceTurleri_PoliceTuruId",
                table: "Policeler");

            migrationBuilder.DropForeignKey(
                name: "FK_Policeler_SigortaSirketleri_SigortaSirketiId",
                table: "Policeler");

            migrationBuilder.DropIndex(
                name: "IX_Kullanicilar_KullaniciAdi",
                table: "Kullanicilar");

            migrationBuilder.AlterColumn<decimal>(
                name: "Prim",
                table: "PoliceTurleri",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "AcentaPrimi",
                table: "PoliceTurleri",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "TelefonNo",
                table: "Policeler",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PoliceNo",
                table: "Policeler",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Kisi",
                table: "Policeler",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "KullaniciAdi",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "PoliceTuruSirketler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SigortaSirketiId = table.Column<int>(type: "int", nullable: false),
                    PoliceTuruId = table.Column<int>(type: "int", nullable: false),
                    PrimYuzde = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AcentaPrimiYuzde = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Aktif = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceTuruSirketler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceTuruSirketler_PoliceTurleri_PoliceTuruId",
                        column: x => x.PoliceTuruId,
                        principalTable: "PoliceTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoliceTuruSirketler_SigortaSirketleri_SigortaSirketiId",
                        column: x => x.SigortaSirketiId,
                        principalTable: "SigortaSirketleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoliceTuruSirketler_PoliceTuruId",
                table: "PoliceTuruSirketler",
                column: "PoliceTuruId");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceTuruSirketler_SigortaSirketiId_PoliceTuruId",
                table: "PoliceTuruSirketler",
                columns: new[] { "SigortaSirketiId", "PoliceTuruId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Policeler_Kullanicilar_PersonelId",
                table: "Policeler",
                column: "PersonelId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policeler_PoliceTurleri_PoliceTuruId",
                table: "Policeler",
                column: "PoliceTuruId",
                principalTable: "PoliceTurleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policeler_SigortaSirketleri_SigortaSirketiId",
                table: "Policeler",
                column: "SigortaSirketiId",
                principalTable: "SigortaSirketleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policeler_Kullanicilar_PersonelId",
                table: "Policeler");

            migrationBuilder.DropForeignKey(
                name: "FK_Policeler_PoliceTurleri_PoliceTuruId",
                table: "Policeler");

            migrationBuilder.DropForeignKey(
                name: "FK_Policeler_SigortaSirketleri_SigortaSirketiId",
                table: "Policeler");

            migrationBuilder.DropTable(
                name: "PoliceTuruSirketler");

            migrationBuilder.AlterColumn<decimal>(
                name: "Prim",
                table: "PoliceTurleri",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AcentaPrimi",
                table: "PoliceTurleri",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "TelefonNo",
                table: "Policeler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PoliceNo",
                table: "Policeler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Kisi",
                table: "Policeler",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "KullaniciAdi",
                table: "Kullanicilar",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicilar_KullaniciAdi",
                table: "Kullanicilar",
                column: "KullaniciAdi",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Policeler_Kullanicilar_PersonelId",
                table: "Policeler",
                column: "PersonelId",
                principalTable: "Kullanicilar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Policeler_PoliceTurleri_PoliceTuruId",
                table: "Policeler",
                column: "PoliceTuruId",
                principalTable: "PoliceTurleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Policeler_SigortaSirketleri_SigortaSirketiId",
                table: "Policeler",
                column: "SigortaSirketiId",
                principalTable: "SigortaSirketleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
