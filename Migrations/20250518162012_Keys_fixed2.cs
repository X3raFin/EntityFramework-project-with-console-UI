using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_lab4.Migrations
{
    /// <inheritdoc />
    public partial class Keys_fixed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Książki_Id_Autora",
                table: "Książki",
                column: "Id_Autora");

            migrationBuilder.CreateIndex(
                name: "IX_Książki_Id_Wyd",
                table: "Książki",
                column: "Id_Wyd");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Książki_Autorzy_Id_Autora",
                table: "Książki");

            migrationBuilder.DropForeignKey(
                name: "FK_Książki_Wydawnictwa_Id_Wyd",
                table: "Książki");

            migrationBuilder.DropIndex(
                name: "IX_Książki_Id_Autora",
                table: "Książki");

            migrationBuilder.DropIndex(
                name: "IX_Książki_Id_Wyd",
                table: "Książki");
        }
    }
}
