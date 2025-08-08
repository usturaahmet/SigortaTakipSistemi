using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SigortaTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class AddAdToPoliceTuru : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PoliceTurleri_SigortaSirketleri_SigortaSirketiId",
                table: "PoliceTurleri");

            migrationBuilder.DropIndex(
                name: "IX_PoliceTurleri_SigortaSirketiId",
                table: "PoliceTurleri");

            migrationBuilder.DropColumn(
                name: "SigortaSirketiId",
                table: "PoliceTurleri");

            migrationBuilder.RenameColumn(
                name: "Turu",
                table: "PoliceTurleri",
                newName: "Ad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "PoliceTurleri",
                newName: "Turu");

            migrationBuilder.AddColumn<int>(
                name: "SigortaSirketiId",
                table: "PoliceTurleri",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PoliceTurleri_SigortaSirketiId",
                table: "PoliceTurleri",
                column: "SigortaSirketiId");

            migrationBuilder.AddForeignKey(
                name: "FK_PoliceTurleri_SigortaSirketleri_SigortaSirketiId",
                table: "PoliceTurleri",
                column: "SigortaSirketiId",
                principalTable: "SigortaSirketleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
