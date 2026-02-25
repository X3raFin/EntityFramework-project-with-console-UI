using Microsoft.EntityFrameworkCore;
using EF_lab4.Tables;

namespace EF_lab4
{
    public class MyDbContext : DbContext
    {
        public DbSet<Autorzy> Autorzy { get; set; }
        public DbSet<Wydawnictwa> Wydawnictwa { get; set; }
        public DbSet<Książki> Książki { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=KAPITANBOMBA\SQLEXPRESS;Database=Księgarnia;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Autorzy>().HasData(
                new Autorzy { Id_Autora = 1, Imię = "Bolesław", Nazwisko = "Prus", Pochodzenie = "Polska" },
                new Autorzy { Id_Autora = 2, Imię = "Henryk", Nazwisko = "Sienkiewicz", Pochodzenie = "Polska" },
                new Autorzy { Id_Autora = 3, Imię = "Adam", Nazwisko = "Mickiewicz", Pochodzenie = "Polska" }
            );

            modelBuilder.Entity<Wydawnictwa>().HasData(
                new Wydawnictwa
                {
                    Id_Wyd = 1,
                    Nazwa = "Marginesy",
                    rok_zal = 2008,
                    Miasto = "Warszawa",
                    Adres = "Ludwika Mieros�awskiego 11A",
                    Kraj_poch = "Polska",
                    aktywne = true
                },
                new Wydawnictwa
                {
                    Id_Wyd = 2,
                    Nazwa = "Znak",
                    rok_zal = 1990,
                    Miasto = "Krak�w",
                    Adres = "ul. Ko�ciuszki 10",
                    Kraj_poch = "Polska",
                    aktywne = true
                },
                new Wydawnictwa
                {
                    Id_Wyd = 3,
                    Nazwa = "Czytelnik",
                    rok_zal = 1944,
                    Miasto = "Warszawa",
                    Adres = "ul. Mokotowska 3",
                    Kraj_poch = "Polska",
                    aktywne = true
                }
            );

            modelBuilder.Entity<Książki>().HasData(
                new Książki { Id_Ks = 1, WydawnictwoId = 1, AutorId = 1, Tytul = "Lalka", Stan_ks = 0, Cena = 50.75m },
                new Książki { Id_Ks = 2, WydawnictwoId = 2, AutorId = 2, Tytul = "Quo Vadis", Stan_ks = stan.Wypożyczona, Cena = 40.00m },
                new Książki { Id_Ks = 3, WydawnictwoId = 3, AutorId = 3, Tytul = "Pan Tadeusz", Stan_ks = 0, Cena = 45.50m },
                new Książki { Id_Ks = 4, WydawnictwoId = 1, AutorId = 1, Tytul = "Faraon", Stan_ks = stan.Uszkodzona, Cena = 39.99m }
            );
        }


    }
}
