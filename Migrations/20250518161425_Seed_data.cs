using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EF_lab4.Migrations
{
    /// <inheritdoc />
    public partial class Seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Książki_Autorzy_AutorzyId_Autora",
                table: "Książki");

            migrationBuilder.DropForeignKey(
                name: "FK_Książki_Wydawnictwa_WydawnictwaId_Wyd",
                table: "Książki");

            migrationBuilder.DropIndex(
                name: "IX_Książki_AutorzyId_Autora",
                table: "Książki");

            migrationBuilder.DropIndex(
                name: "IX_Książki_WydawnictwaId_Wyd",
                table: "Książki");

            migrationBuilder.DropColumn(
                name: "AutorzyId_Autora",
                table: "Książki");

            migrationBuilder.DropColumn(
                name: "WydawnictwaId_Wyd",
                table: "Książki");

            migrationBuilder.AddColumn<decimal>(
                name: "Cena",
                table: "Książki",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Tytul",
                table: "Książki",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Autorzy",
                columns: new[] { "Id_Autora", "Imię", "Nazwisko", "Pochodzenie" },
                values: new object[,]
                {
                    { 1, "Bolesław", "Prus", "Polska" },
                    { 2, "Henryk", "Sienkiewicz", "Polska" },
                    { 3, "Adam", "Mickiewicz", "Polska" }
                });

            migrationBuilder.InsertData(
                table: "Książki",
                columns: new[] { "Id_Ks", "Cena", "Id_Autora", "Id_Wyd", "Stan_ks", "Tytul" },
                values: new object[,]
                {
                    { 1, 50.75m, 1, 1, 0, "Lalka" },
                    { 2, 40.00m, 2, 2, 1, "Quo Vadis" },
                    { 3, 45.50m, 3, 3, 0, "Pan Tadeusz" },
                    { 4, 39.99m, 1, 1, 2, "Faraon" }
                });

            migrationBuilder.InsertData(
                table: "Wydawnictwa",
                columns: new[] { "Id_Wyd", "Adres", "Kraj_poch", "Miasto", "Nazwa", "aktywne", "rok_zal" },
                values: new object[,]
                {
                    { 1, "Ludwika Mierosławskiego 11A", "Polska", "Warszawa", "Marginesy", true, 2008 },
                    { 2, "ul. Kościuszki 10", "Polska", "Kraków", "Znak", true, 1990 },
                    { 3, "ul. Mokotowska 3", "Polska", "Warszawa", "Czytelnik", true, 1944 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Autorzy",
                keyColumn: "Id_Autora",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Autorzy",
                keyColumn: "Id_Autora",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Autorzy",
                keyColumn: "Id_Autora",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Książki",
                keyColumn: "Id_Ks",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Książki",
                keyColumn: "Id_Ks",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Książki",
                keyColumn: "Id_Ks",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Książki",
                keyColumn: "Id_Ks",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Wydawnictwa",
                keyColumn: "Id_Wyd",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Wydawnictwa",
                keyColumn: "Id_Wyd",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Wydawnictwa",
                keyColumn: "Id_Wyd",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Cena",
                table: "Książki");

            migrationBuilder.DropColumn(
                name: "Tytul",
                table: "Książki");

            migrationBuilder.AddColumn<int>(
                name: "AutorzyId_Autora",
                table: "Książki",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WydawnictwaId_Wyd",
                table: "Książki",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Książki_AutorzyId_Autora",
                table: "Książki",
                column: "AutorzyId_Autora");

            migrationBuilder.CreateIndex(
                name: "IX_Książki_WydawnictwaId_Wyd",
                table: "Książki",
                column: "WydawnictwaId_Wyd");

            migrationBuilder.AddForeignKey(
                name: "FK_Książki_Autorzy_AutorzyId_Autora",
                table: "Książki",
                column: "AutorzyId_Autora",
                principalTable: "Autorzy",
                principalColumn: "Id_Autora",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Książki_Wydawnictwa_WydawnictwaId_Wyd",
                table: "Książki",
                column: "WydawnictwaId_Wyd",
                principalTable: "Wydawnictwa",
                principalColumn: "Id_Wyd",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
