using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_lab4.Migrations
{
    /// <inheritdoc />
    public partial class Initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autorzy",
                columns: table => new
                {
                    Id_Autora = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imię = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Pochodzenie = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autorzy", x => x.Id_Autora);
                });

            migrationBuilder.CreateTable(
                name: "Wydawnictwa",
                columns: table => new
                {
                    Id_Wyd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Kraj_poch = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Miasto = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rok_zal = table.Column<int>(type: "int", nullable: false),
                    aktywne = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wydawnictwa", x => x.Id_Wyd);
                });

            migrationBuilder.CreateTable(
                name: "Książki",
                columns: table => new
                {
                    Id_Ks = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Wyd = table.Column<int>(type: "int", nullable: false),
                    Id_Autora = table.Column<int>(type: "int", nullable: false),
                    WydawnictwaId_Wyd = table.Column<int>(type: "int", nullable: false),
                    AutorzyId_Autora = table.Column<int>(type: "int", nullable: false),
                    Stan_ks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Książki", x => x.Id_Ks);
                    table.ForeignKey(
                        name: "FK_Książki_Autorzy_AutorzyId_Autora",
                        column: x => x.AutorzyId_Autora,
                        principalTable: "Autorzy",
                        principalColumn: "Id_Autora",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Książki_Wydawnictwa_WydawnictwaId_Wyd",
                        column: x => x.WydawnictwaId_Wyd,
                        principalTable: "Wydawnictwa",
                        principalColumn: "Id_Wyd",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Książki_AutorzyId_Autora",
                table: "Książki",
                column: "AutorzyId_Autora");

            migrationBuilder.CreateIndex(
                name: "IX_Książki_WydawnictwaId_Wyd",
                table: "Książki",
                column: "WydawnictwaId_Wyd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Książki");

            migrationBuilder.DropTable(
                name: "Autorzy");

            migrationBuilder.DropTable(
                name: "Wydawnictwa");
        }
    }
}
