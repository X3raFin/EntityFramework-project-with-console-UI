using EF_lab4.Tables;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_lab4.Services
{
    internal class AuthorsServices : IAuthorsServices
    {
        private readonly MyDbContext dbContext;

        public AuthorsServices(MyDbContext cnt)
        {
            dbContext = cnt;
        }

        public IEnumerable<Autorzy> WriteUpStatus()
        {
            return dbContext.Autorzy.Select(b => b);
        }

        public string AddNewUnit(string name, string secondName, string origin)
        {
            try
            {
                var au = new Autorzy()
                {
                    Imię = name,
                    Nazwisko = secondName,
                    Pochodzenie = origin
                };
                dbContext.Autorzy.Add(au);
                dbContext.SaveChanges();
                return "\nAutor został dodaya pomyślnie.";
            }
            catch (Exception ex)
            {
                return $"\nWystąpił błąd podczas dodawania autora: {ex.Message}";
            }
        }

        public string DeleteUnit(int _authorID)
        {
            try
            {
                var toRmv = dbContext.Autorzy.FirstOrDefault(a => a.Id_Autora == _authorID);
                dbContext.Autorzy.Remove(toRmv);
                dbContext.SaveChanges();
                return "\nAutor został usunięty pomyślnie.";
            }
            catch (Exception ex)
            {
                return $"\nWystąpił błąd podczas usuwania autora: {ex.Message}";
            }
        }

        public string ModifyUnit(int authorId, AuthorsModifyParams parameters)
        {
            try
            {
                var authorToModify = dbContext.Autorzy.FirstOrDefault(a => a.Id_Autora == authorId);

                if (authorToModify == null)
                {
                    return "Nie znaleziono książki o podanym numerze ID.";
                }

                if (parameters.Imię != null)
                {
                    authorToModify.Imię = parameters.Imię;
                }
                if (parameters.Nazwisko != null)
                {
                    authorToModify.Nazwisko = parameters.Nazwisko;
                }
                if (parameters.Pochodzenie != null)
                {
                    authorToModify.Pochodzenie = parameters.Pochodzenie;
                }

                dbContext.SaveChanges();
                return "Autor został zmodyfikowany pomyślnie.";
            }
            catch (Exception ex)
            {
                return $"Wystąpił błąd podczas edytowania autora: {ex.Message}";
            }
        }

        public IQueryable<Autorzy> SearchingForUnit(string _phrase)
        {
            return dbContext.Autorzy.Where(a => a.Nazwisko.Contains(_phrase));
        }

        public Autorzy GetAuthorById(int id)
        {
            return dbContext.Autorzy.FirstOrDefault(a => a.Id_Autora == id);
        }

        public bool UnitExist(int _authorID)
        {
            var searching = dbContext.Autorzy.FirstOrDefault(a => a.Id_Autora == _authorID);
            if (searching != null) return false;
            return true;
        }
    }
}
