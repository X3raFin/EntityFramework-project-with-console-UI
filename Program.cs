using EF_lab4.Services;
using EF_lab4.Tables;
using Ninject;
using Microsoft.Extensions.Configuration;

namespace EF_lab4
{
    internal class Program
    {
        static void ShowBooks(IEnumerable<Książki> books)
        {

            Console.WriteLine("Książki");
            Console.WriteLine();
            Console.WriteLine("====================================");
            foreach (var b in books)
            {
                Console.WriteLine($"Id książki = {b.Id_Ks}");
                Console.WriteLine($"Id wydawnictwa = {b.WydawnictwoId}");
                Console.WriteLine($"Id autora = {b.AutorId}");
                Console.WriteLine($"Tytul = {b.Tytul}");
                Console.WriteLine($"Stan = {b.Stan_ks}");
                Console.WriteLine($"Cena = {b.Cena}");
                Console.WriteLine("====================================");
            }
        }

        static void ShowAuthors(IEnumerable<Autorzy> authors)
        {
            Console.WriteLine("Autorzy");
            Console.WriteLine();
            Console.WriteLine("====================================");
            foreach (var a in authors)
            {
                Console.WriteLine($"Id autora = {a.Id_Autora}");
                Console.WriteLine($"Imię = {a.Imię}");
                Console.WriteLine($"Nazwisko = {a.Nazwisko}");
                Console.WriteLine($"Pochodzenie = {a.Pochodzenie}");
                Console.WriteLine("====================================");
            }
        }

        static void ShowPubHouse(IEnumerable<Wydawnictwa> pubhouses)
        {
            Console.WriteLine("Wydawnictwa");
            Console.WriteLine();
            Console.WriteLine("====================================");
            foreach (var p in pubhouses)
            {
                Console.WriteLine($"Id wydawnictwa = {p.Id_Wyd}");
                Console.WriteLine($"Nazwa = {p.Nazwa}");
                Console.WriteLine($"Kraj pochodzenia = {p.Kraj_poch}");
                Console.WriteLine($"Miasto = {p.Miasto}");
                Console.WriteLine($"Adres = {p.Adres}");
                Console.WriteLine($"Rok założenia = {p.rok_zal}");
                Console.WriteLine($"Aktywne = {p.aktywne}");
                Console.WriteLine("====================================");
            }
        }

        static void ClearTerminal()
        {
            Console.Write("\x1b[2J");
            Console.Write("\x1b[3J");
            Console.Write("\x1b[H");
        }

        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            using (var kernel = new StandardKernel(new ServiceModule(configuration.GetConnectionString("DefaultConnection"))))
            {
                var books = kernel.Get<IBookServices>();
                var authors = kernel.Get<IAuthorsServices>();
                var pub_houses = kernel.Get<IPublishingHouseServices>();
                while (true)
                {
                    ClearTerminal();
                    Console.WriteLine("---------Witamy w Bazie Danych księgarni!---------");
                    Console.WriteLine();
                    Console.WriteLine("Co chciałbyś zrobić?");
                    Console.WriteLine("Wyświetl Bazę Danych: a");
                    Console.WriteLine("Dodać, usunąć lub zmodyfikować dane: b");
                    Console.WriteLine("Zapytania do Bazy Danych: c");
                    Console.WriteLine("Zamknij program: q");

                    ConsoleKeyInfo? answer = Console.ReadKey(intercept: true);
                    string ans = answer?.KeyChar.ToString().ToLower();
                    if (ans == "q") break;
                    switch (ans)
                    {
                        case "a":
                            ClearTerminal();
                            ShowBooks(books.WriteUpStatus());
                            Console.WriteLine();
                            ShowAuthors(authors.WriteUpStatus());
                            Console.WriteLine();
                            ShowPubHouse(pub_houses.WriteUpStatus());
                            Console.ReadKey(intercept: true);
                            break;
                        case "b":
                            ClearTerminal();
                            Console.WriteLine("Którą tabele chcesz modyfikować?");
                            Console.WriteLine("Książki: a\nAutorzy: b\nWydawnictwa: c");
                            answer = Console.ReadKey();
                            ans = answer?.KeyChar.ToString().ToLower();
                            switch (ans)
                            {
                                case "a":
                                    ClearTerminal();
                                    ShowBooks(books.WriteUpStatus());
                                    Console.WriteLine("\nJaką operacje dokładnie chcesz wykonać?");
                                    Console.WriteLine("Dodanie rekordu: a");
                                    Console.WriteLine("Usunięcie rekordu: b");
                                    Console.WriteLine("Modyfikacja rekordu: c");
                                    answer = Console.ReadKey();
                                    ans = answer?.KeyChar.ToString().ToLower();
                                    switch (ans)
                                    {
                                        case "a":
                                            ClearTerminal();
                                            try
                                            {
                                                Console.WriteLine("\nId Wydawnictwa:");
                                                int wyd = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Id Autora:");
                                                int aut = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Tytuł:");
                                                string tyt = Console.ReadLine();
                                                Console.WriteLine("Cena:");
                                                decimal cn = decimal.Parse(Console.ReadLine());

                                                Console.WriteLine(books.AddNewUnit(wyd, aut, tyt, cn));
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("\nBłąd: Wprowadzono niepoprawny format danych (oczekiwano liczby).");
                                            }
                                            Console.ReadKey(intercept: true);
                                            break;
                                        case "b":
                                            ClearTerminal();
                                            ShowBooks(books.WriteUpStatus());
                                            try
                                            {
                                                Console.WriteLine("\nPodaj Id książki która chcesz usunąć:");
                                                int rmvId = int.Parse(Console.ReadLine());
                                                if (books.UnitExist(rmvId))
                                                {
                                                    Console.WriteLine("\nNie znaleziono książki o tym numerze ID.");
                                                    Console.ReadKey(intercept: true);
                                                    break;
                                                }
                                                Console.WriteLine("\nCzy na pewno chcesz usunąć tę książkę? tak/nie");
                                                if (Console.ReadLine().Trim().ToLower() == "nie")
                                                {
                                                    Console.WriteLine("Procedura przerwana.");
                                                    Console.ReadKey(intercept: true);
                                                    break;
                                                }
                                                Console.WriteLine(books.DeleteUnit(rmvId));
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("\nBłąd: Wprowadzono niepoprawny format Id (oczekiwano liczby całkowitej).");
                                            }
                                            Console.ReadKey(intercept: true);
                                            break;
                                        case "c":
                                            try
                                            {
                                                ClearTerminal();
                                                ShowBooks(books.WriteUpStatus());
                                                Console.WriteLine("Podaj Id rekordu do modyfikacji:");
                                                var id = int.Parse(Console.ReadLine());
                                                ClearTerminal();
                                                if (books.UnitExist(id))
                                                {
                                                    Console.WriteLine("\nNie znaleziono książki o tym numerze ID.");
                                                    Console.ReadKey(intercept: true);
                                                    break;
                                                }
                                                var currentBook = books.GetBookById(id);
                                                Console.WriteLine($"Id książki = {currentBook.Id_Ks}\nId Wydawnictwa = {currentBook.WydawnictwoId}\nId Autora = {currentBook.AutorId}\n" +
                                                                    $"Tytuł = {currentBook.Tytul}\nStan = {currentBook.Stan_ks}\nCena = {currentBook.Cena}");
                                                var modifications = new BookModifyParams();
                                                Console.WriteLine("\nProszę wypełnić tylko pola które mają zostać zmienione (pozostaw puste, aby nie zmieniać):");

                                                Console.WriteLine("\nId Wydawnictwa (obecnie: " + currentBook.WydawnictwoId + "):");
                                                string inputWyd = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputWyd))
                                                {
                                                    modifications.WydawnictwoId = int.Parse(inputWyd);
                                                }

                                                Console.WriteLine("Id Autora (obecnie: " + currentBook.AutorId + "):");
                                                string inputAut = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputAut))
                                                {
                                                    modifications.AutorId = int.Parse(inputAut);
                                                }

                                                Console.WriteLine("Tytuł (obecnie: " + currentBook.Tytul + "):");
                                                string inputTyt = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputTyt))
                                                {
                                                    modifications.Tytul = inputTyt;
                                                }

                                                Console.WriteLine("Stan (0-Dostępna, 1-Wypożyczona, 2-Zarezerwowana, 3-Uszkodzona) (obecnie: " + currentBook.Stan_ks + "):");
                                                string inputStan = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputStan))
                                                {
                                                    if (Enum.TryParse(inputStan, out stan parsedStan))
                                                    {
                                                        modifications.Stan_ks = parsedStan;
                                                    }
                                                    else if (int.TryParse(inputStan, out int stanInt) && Enum.IsDefined(typeof(stan), stanInt))
                                                    {
                                                        modifications.Stan_ks = (stan)stanInt;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Ostrzeżenie: Niepoprawna wartość stanu. Stan nie zostanie zmieniony.");
                                                    }
                                                }

                                                Console.WriteLine("Cena (obecnie: " + currentBook.Cena + "):");
                                                string inputCena = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputCena))
                                                {
                                                    modifications.Cena = decimal.Parse(inputCena);
                                                }

                                                Console.WriteLine(books.ModifyUnit(id, modifications));

                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("\nBłąd: Wprowadzono niepoprawny format danych (oczekiwano liczby).");
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"\nWystąpił błąd podczas modyfikowania książki: {ex.Message}");
                                            }
                                            Console.ReadKey(intercept: true);
                                            break;
                                    }
                                    Console.Clear();
                                    break;
                                case "b":
                                    ClearTerminal();
                                    ShowAuthors(authors.WriteUpStatus());
                                    Console.WriteLine("\nJaką operację dokładnie chcesz wykonać?");
                                    Console.WriteLine("Dodanie rekordu: a");
                                    Console.WriteLine("Usunięcie rekordu: b");
                                    Console.WriteLine("Modyfikacja rekordu: c");
                                    ConsoleKeyInfo authorActionAnswer = Console.ReadKey(intercept: true);
                                    string authorAction = authorActionAnswer.KeyChar.ToString().ToLower();
                                    switch (authorAction)
                                    {
                                        case "a":
                                            ClearTerminal();
                                            try
                                            {
                                                Console.WriteLine("\nImię autora:");
                                                string imie = Console.ReadLine();
                                                Console.WriteLine("Nazwisko autora:");
                                                string nazwisko = Console.ReadLine();
                                                Console.WriteLine("Pochodzenie autora:");
                                                string pochodzenie = Console.ReadLine();

                                                Console.WriteLine(authors.AddNewUnit(imie, nazwisko, pochodzenie));
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Niepoprawny format danych wejśćiowych(Dopuszczalny tylko string)");
                                            }
                                            Console.ReadKey(intercept: true);
                                            break;

                                        case "b":
                                            ClearTerminal();
                                            ShowAuthors(authors.WriteUpStatus());
                                            try
                                            {
                                                Console.WriteLine("\nPodaj Id autora, którego chcesz usunąć:");
                                                int rmvId = int.Parse(Console.ReadLine());
                                                if (authors.UnitExist(rmvId))
                                                {
                                                    Console.WriteLine("\nNie znaleziono autora o tym numerze ID.");
                                                    Console.ReadKey(intercept: true);
                                                    break;
                                                }
                                                Console.WriteLine("\nCzy na pewno chcesz usunąć tego autora? tak/nie");
                                                if (Console.ReadLine().Trim().ToLower() == "nie")
                                                {
                                                    Console.WriteLine("Procedura przerwana.");
                                                    Console.ReadKey(intercept: true);
                                                    break;
                                                }
                                                Console.WriteLine(authors.DeleteUnit(rmvId));
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("\nBłąd: Wprowadzono niepoprawny format Id (oczekiwano liczby całkowitej).");
                                            }
                                            Console.ReadKey(intercept: true);
                                            break;

                                        case "c":
                                            ClearTerminal();
                                            ShowAuthors(authors.WriteUpStatus());
                                            try
                                            {
                                                Console.WriteLine("Podaj Id rekordu do modyfikacji:");
                                                int modifyId = int.Parse(Console.ReadLine());
                                                ClearTerminal();
                                                if (authors.UnitExist(modifyId))
                                                {
                                                    Console.WriteLine("\nNie znaleziono autora o tym numerze ID.");
                                                    Console.ReadKey(intercept: true);
                                                    break;
                                                }
                                                var currentAuthor = authors.GetAuthorById(modifyId);
                                                Console.WriteLine($"Id autora = {currentAuthor.Id_Autora}\nImię = {currentAuthor.Imię}\nNazwisko = {currentAuthor.Nazwisko}\n" +
                                                    $"Pochodzenie = {currentAuthor.Pochodzenie}");
                                                var modifications = new AuthorsModifyParams();
                                                Console.WriteLine("\nProszę wypełnić tylko pola które mają zostać zmienione (pozostaw puste, aby nie zmieniać):");

                                                Console.WriteLine("\nImię (obecnie: " + currentAuthor.Imię + "):");
                                                string inputImie = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputImie))
                                                {
                                                    modifications.Imię = inputImie;
                                                }

                                                Console.WriteLine("Nazwisko (obecnie: " + currentAuthor.Nazwisko + "):");
                                                string inputNazwisko = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputNazwisko))
                                                {
                                                    modifications.Nazwisko = inputNazwisko;
                                                }

                                                Console.WriteLine("Pochodzenie (obecnie: " + currentAuthor.Pochodzenie + "):");
                                                string inputPochodzenie = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputPochodzenie))
                                                {
                                                    modifications.Pochodzenie = inputPochodzenie;
                                                }

                                                Console.WriteLine(authors.ModifyUnit(modifyId, modifications));
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("\nBłąd: Wprowadzono niepoprawny format danych (oczekiwano liczby dla Id).");
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"\nWystąpił błąd podczas modyfikowania autora: {ex.Message}");
                                            }
                                            Console.ReadKey(intercept: true);
                                            break;
                                    }
                                    Console.Clear();
                                    break;
                                case "c":
                                    ClearTerminal();
                                    ShowPubHouse(pub_houses.WriteUpStatus());
                                    Console.WriteLine("\nJaką operację dokładnie chcesz wykonać?");
                                    Console.WriteLine("Dodanie rekordu: a");
                                    Console.WriteLine("Usunięcie rekordu: b");
                                    Console.WriteLine("Modyfikacja rekordu: c");
                                    ConsoleKeyInfo pubHouseActionAnswer = Console.ReadKey(intercept: true);
                                    string pubHouseAction = pubHouseActionAnswer.KeyChar.ToString().ToLower();

                                    switch (pubHouseAction)
                                    {
                                        case "a":
                                            ClearTerminal();
                                            try
                                            {
                                                Console.WriteLine("\nNazwa wydawnictwa:");
                                                string nazwa = Console.ReadLine();
                                                Console.WriteLine("Kraj pochodzenia:");
                                                string krajPoch = Console.ReadLine();
                                                Console.WriteLine("Miasto:");
                                                string miasto = Console.ReadLine();
                                                Console.WriteLine("Adres:");
                                                string adres = Console.ReadLine();
                                                Console.WriteLine("Rok założenia:");
                                                int rokZal = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Aktywne (true/false):");
                                                bool aktywne = bool.Parse(Console.ReadLine());

                                                Console.WriteLine(pub_houses.AddNewUnit(nazwa, krajPoch, miasto, adres, rokZal, aktywne));
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("\nBłąd: Wprowadzono niepoprawny format danych (oczekiwano liczby dla roku założenia lub true/false dla 'Aktywne').");
                                            }
                                            Console.ReadKey(intercept: true);
                                            break;

                                        case "b":
                                            ClearTerminal();
                                            ShowPubHouse(pub_houses.WriteUpStatus());
                                            try
                                            {
                                                Console.WriteLine("\nPodaj Id wydawnictwa, które chcesz usunąć:");
                                                int rmvId = int.Parse(Console.ReadLine());
                                                if (pub_houses.UnitExist(rmvId))
                                                {
                                                    Console.WriteLine("\nNie znaleziono wydawnictwa o tym numerze ID.");
                                                    Console.ReadKey(intercept: true);
                                                    break;
                                                }
                                                Console.WriteLine("\nCzy na pewno chcesz usunąć te wydawnictwo? tak/nie");
                                                if (Console.ReadLine().Trim().ToLower() == "nie")
                                                {
                                                    Console.WriteLine("Procedura przerwana.");
                                                    Console.ReadKey(intercept: true);
                                                    break;
                                                }
                                                Console.WriteLine(pub_houses.DeleteUnit(rmvId));
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("\nBłąd: Wprowadzono niepoprawny format Id (oczekiwano liczby całkowitej).");
                                            }
                                            Console.ReadKey(intercept: true);
                                            break;

                                        case "c":
                                            ClearTerminal();
                                            ShowPubHouse(pub_houses.WriteUpStatus());
                                            try
                                            {
                                                Console.WriteLine("Podaj Id rekordu do modyfikacji:");
                                                int modifyId = int.Parse(Console.ReadLine());
                                                ClearTerminal();
                                                if (pub_houses.UnitExist(modifyId))
                                                {
                                                    Console.WriteLine("\nNie znaleziono wydawnictwa o tym numerze ID.");
                                                    Console.ReadKey(intercept: true);
                                                    break;
                                                }
                                                var currentPubHouse = pub_houses.GetPubHouseById(modifyId);
                                                Console.WriteLine($"Id wydawnictwa = {currentPubHouse.Id_Wyd}\nNazwa = {currentPubHouse.Nazwa}\nKraj pochodzenia = {currentPubHouse.Kraj_poch}\n" +
                                                    $"Miasto = {currentPubHouse.Miasto}\nAdres = {currentPubHouse.Adres}\nRok założenia = {currentPubHouse.rok_zal}\nAktywne = {currentPubHouse.aktywne}");
                                                var modifications = new PublishingHouseMoifyParams();
                                                Console.WriteLine("\nProszę wypełnić tylko pola które mają zostać zmienione (pozostaw puste, aby nie zmieniać):");

                                                Console.WriteLine("\nNazwa (obecnie: " + currentPubHouse.Nazwa + "):");
                                                string inputNazwa = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputNazwa))
                                                {
                                                    modifications.Nazwa = inputNazwa;
                                                }

                                                Console.WriteLine("Kraj pochodzenia (obecnie: " + currentPubHouse.Kraj_poch + "):");
                                                string inputKrajPoch = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputKrajPoch))
                                                {
                                                    modifications.Kraj_poch = inputKrajPoch;
                                                }

                                                Console.WriteLine("Miasto (obecnie: " + currentPubHouse.Miasto + "):");
                                                string inputMiasto = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputMiasto))
                                                {
                                                    modifications.Miasto = inputMiasto;
                                                }

                                                Console.WriteLine("Adres (obecnie: " + currentPubHouse.Adres + "):");
                                                string inputAdres = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputAdres))
                                                {
                                                    modifications.Adres = inputAdres;
                                                }

                                                Console.WriteLine("Rok założenia (obecnie: " + currentPubHouse.rok_zal + "):");
                                                string inputRokZal = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputRokZal))
                                                {
                                                    if (int.TryParse(inputRokZal, out int rokZalParsed))
                                                    {
                                                        modifications.rok_zal = rokZalParsed;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Ostrzeżenie: Niepoprawny format roku założenia. Wartość nie zostanie zmieniona.");
                                                    }
                                                }

                                                Console.WriteLine("Aktywne (true/false) (obecnie: " + currentPubHouse.aktywne + "):");
                                                string inputAktywne = Console.ReadLine();
                                                if (!string.IsNullOrWhiteSpace(inputAktywne))
                                                {
                                                    if (bool.TryParse(inputAktywne, out bool aktywneParsed))
                                                    {
                                                        modifications.aktywne = aktywneParsed;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Ostrzeżenie: Niepoprawny format dla 'Aktywne'. Wartość nie zostanie zmieniona.");
                                                    }
                                                }

                                                Console.WriteLine(pub_houses.ModifyUnit(modifyId, modifications));

                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("\nBłąd: Wprowadzono niepoprawny format danych (oczekiwano liczby dla Id, roku założenia lub true/false dla 'Aktywne').");
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"\nWystąpił błąd podczas modyfikowania wydawnictwa: {ex.Message}");
                                            }
                                            Console.ReadKey(intercept: true);
                                            break;
                                    }
                                    Console.Clear();
                                    break;
                            }
                            break;
                        case "c":
                            ClearTerminal();
                            Console.WriteLine("Jakiej informacji szukasz:");
                            Console.WriteLine("Autora: a\nKsiążki: b\nWydawnictwa: c");
                            answer = Console.ReadKey(intercept: true);
                            ans = answer?.KeyChar.ToString().ToLower();
                            switch (ans)
                            {
                                case "a":
                                    Console.WriteLine("Wpisz fraze której szukasz:");
                                    ans = Console.ReadLine();
                                    ClearTerminal();
                                    var anonymous = authors.SearchingForUnit(ans);
                                    if (!anonymous.Any()) Console.WriteLine("Nie znaleziono podanej frazy.");
                                    else
                                    {
                                        foreach (var a in anonymous)
                                        {
                                            Console.WriteLine($"Id autora = {a.Id_Autora}");
                                            Console.WriteLine($"Imię = {a.Imię}");
                                            Console.WriteLine($"Nazwisko = {a.Nazwisko}");
                                            Console.WriteLine($"Pochodzenie = {a.Pochodzenie}");
                                            Console.WriteLine();
                                        }
                                        Console.WriteLine("O to znalezione wystąpienia wyrażenia...");
                                    }
                                    Console.ReadKey();
                                    break;
                                case "b":
                                    Console.WriteLine("Wpisz fraze której szukasz:");
                                    ans = Console.ReadLine();
                                    ClearTerminal();
                                    var tresure = books.SearchingForUnit(ans);
                                    if (!tresure.Any()) Console.WriteLine("Nie znaleziono podanej frazy.");
                                    else
                                    {
                                        foreach (var b in tresure)
                                        {
                                            Console.WriteLine($"Id książki = {b.Id_Ks}");
                                            Console.WriteLine($"Id wydawnictwa = {b.WydawnictwoId}");
                                            Console.WriteLine($"Id autora = {b.AutorId}");
                                            Console.WriteLine($"Tytul = {b.Tytul}");
                                            Console.WriteLine($"Stan = {b.Stan_ks}");
                                            Console.WriteLine($"Cena = {b.Cena}");
                                            Console.WriteLine("");
                                        }
                                        Console.WriteLine("O to znalezione wystąpienia wyrażenia...");
                                    }
                                    Console.ReadKey();
                                    break;
                                case "c":
                                    Console.WriteLine("Wpisz fraze której szukasz:");
                                    ans = Console.ReadLine();
                                    ClearTerminal();
                                    var idk = pub_houses.SearchingForUnit(ans);
                                    if (!idk.Any()) Console.WriteLine("Nie znaleziono podanej frazy.");
                                    else
                                    {
                                        foreach (var p in idk)
                                        {
                                            Console.WriteLine($"Id wydawnictwa = {p.Id_Wyd}");
                                            Console.WriteLine($"Nazwa = {p.Nazwa}");
                                            Console.WriteLine($"Kraj pochodzenia = {p.Kraj_poch}");
                                            Console.WriteLine($"Miasto = {p.Miasto}");
                                            Console.WriteLine($"Adres = {p.Adres}");
                                            Console.WriteLine($"Rok założenia = {p.rok_zal}");
                                            Console.WriteLine($"Aktywne = {p.aktywne}");
                                            Console.WriteLine("");
                                        }
                                        Console.WriteLine("O to znalezione wystąpienia wyrażenia...");
                                    }
                                    Console.ReadKey();
                                    break;
                            }
                            break;
                    }
                }
            }
        }
    }
}
