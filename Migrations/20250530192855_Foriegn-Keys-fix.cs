using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_lab4.Migrations
{
    /// <inheritdoc />
    public partial class ForiegnKeysfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Książki_Autorzy_Id_Autora",
                table: "Książki");

            migrationBuilder.DropForeignKey(
                name: "FK_Książki_Wydawnictwa_Id_Wyd",
                table: "Książki");

            migrationBuilder.RenameColumn(
                name: "Id_Wyd",
                table: "Książki",
                newName: "WydawnictwoId");

            migrationBuilder.RenameColumn(
                name: "Id_Autora",
                table: "Książki",
                newName: "AutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Książki_Id_Wyd",
                table: "Książki",
                newName: "IX_Książki_WydawnictwoId");

            migrationBuilder.RenameIndex(
                name: "IX_Książki_Id_Autora",
                table: "Książki",
                newName: "IX_Książki_AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Książki_Autorzy_AutorId",
                table: "Książki",
                column: "AutorId",
                principalTable: "Autorzy",
                principalColumn: "Id_Autora",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Książki_Wydawnictwa_WydawnictwoId",
                table: "Książki",
                column: "WydawnictwoId",
                principalTable: "Wydawnictwa",
                principalColumn: "Id_Wyd",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Książki_Autorzy_AutorId",
                table: "Książki");

            migrationBuilder.DropForeignKey(
                name: "FK_Książki_Wydawnictwa_WydawnictwoId",
                table: "Książki");

            migrationBuilder.RenameColumn(
                name: "WydawnictwoId",
                table: "Książki",
                newName: "Id_Wyd");

            migrationBuilder.RenameColumn(
                name: "AutorId",
                table: "Książki",
                newName: "Id_Autora");

            migrationBuilder.RenameIndex(
                name: "IX_Książki_WydawnictwoId",
                table: "Książki",
                newName: "IX_Książki_Id_Wyd");

            migrationBuilder.RenameIndex(
                name: "IX_Książki_AutorId",
                table: "Książki",
                newName: "IX_Książki_Id_Autora");

            migrationBuilder.AddForeignKey(
                name: "FK_Książki_Autorzy_Id_Autora",
                table: "Książki",
                column: "Id_Autora",
                principalTable: "Autorzy",
                principalColumn: "Id_Autora",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Książki_Wydawnictwa_Id_Wyd",
                table: "Książki",
                column: "Id_Wyd",
                principalTable: "Wydawnictwa",
                principalColumn: "Id_Wyd",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
