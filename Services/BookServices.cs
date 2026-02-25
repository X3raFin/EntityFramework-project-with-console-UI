using EF_lab4.Tables;

namespace EF_lab4.Services
{
    internal class BookServices : IBookServices
    {
        private readonly MyDbContext dbContext;

        public BookServices(MyDbContext cnt)
        {
            dbContext = cnt;
        }

        public IEnumerable<Książki> WriteUpStatus()
        {
            return dbContext.Książki.Select(b => b);
        }

        public string AddNewUnit(int _pub_house, int _author, string _title, decimal _price)
        {
            try
            {
                if (dbContext.Książki.Any(b => b.Tytul == _title))
                {
                    var _pub_houses = dbContext.Książki.Where(b => b.Tytul == _title).Select(b => b.WydawnictwoId).ToList();
                    if (_pub_houses.Any(b => b == _pub_house))
                    {
                        return "Błąd. Książka o podanej nazwie istnieje juz w Bazie danych.";
                    }
                }

                var ks = new Książki()
                {
                    WydawnictwoId = _pub_house,
                    AutorId = _author,
                    Tytul = _title,
                    Cena = _price
                };
                dbContext.Książki.Add(ks);
                dbContext.SaveChanges();
                return "\nKsiążka została dodana pomyślnie.";
            }
            catch (Exception ex)
            {
                return $"\nWystąpił błąd podczas dodawania książki: {ex.Message}";
            }
        }

        public string DeleteUnit(int _bookID)
        {
            try
            {
                var toRmv = dbContext.Książki.FirstOrDefault(k => k.Id_Ks == _bookID);
                dbContext.Książki.Remove(toRmv);
                dbContext.SaveChanges();
                return "\nKsiążka została usunięta pomyślnie.";
            }
            catch (Exception ex)
            {
                return $"\nWystąpił błąd podczas usuwania książki: {ex.Message}";
            }
        }

        public string ModifyUnit(int bookId, BookModifyParams parameters)
        {
            try
            {
                var bookToModify = dbContext.Książki.FirstOrDefault(k => k.Id_Ks == bookId);

                if (bookToModify == null)
                {
                    return "Nie znaleziono książki o podanym numerze ID.";
                }

                if (parameters.WydawnictwoId.HasValue)
                {
                    bookToModify.WydawnictwoId = parameters.WydawnictwoId.Value;
                }
                if (parameters.AutorId.HasValue)
                {
                    bookToModify.AutorId = parameters.AutorId.Value;
                }
                if (parameters.Tytul != null)
                {
                    bookToModify.Tytul = parameters.Tytul;
                }
                if (parameters.Stan_ks.HasValue)
                {
                    bookToModify.Stan_ks = parameters.Stan_ks.Value;
                }
                if (parameters.Cena.HasValue)
                {
                    bookToModify.Cena = parameters.Cena.Value;
                }

                dbContext.SaveChanges();
                return "Książka została zmodyfikowana pomyślnie.";
            }
            catch (Exception ex)
            {
                return $"Wystąpił błąd podczas edytowania książki: {ex.Message}";
            }
        }

        public IQueryable<Książki> SearchingForUnit(string _phrase)
        {
            return dbContext.Książki.Where(k => k.Tytul.Contains(_phrase));
        }

        public Książki GetBookById(int id)
        {
            return dbContext.Książki.FirstOrDefault(b => b.Id_Ks == id);
        }

        public bool UnitExist(int _bookID)
        {
            var searching = dbContext.Książki.FirstOrDefault(k => k.Id_Ks == _bookID);
            if (searching != null) return false;
            return true;
        }
    }
}
