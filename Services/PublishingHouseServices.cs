using EF_lab4.Tables;

namespace EF_lab4.Services
{
    internal class PublishingHouseServices : IPublishingHouseServices
    {
        private readonly MyDbContext dbContext;

        public PublishingHouseServices(MyDbContext cnt)
        {
            dbContext = cnt;
        }

        public IEnumerable<Wydawnictwa> WriteUpStatus()
        {
            return dbContext.Wydawnictwa.Select(b => b);
        }

        public string AddNewUnit(string name, string origin, string city, string address, int yearOfCommencement, bool active)
        {
            try
            {
                var ph = new Wydawnictwa()
                {
                    Nazwa = name,
                    Kraj_poch = origin,
                    Miasto = city,
                    Adres = address,
                    rok_zal = yearOfCommencement,
                    aktywne = active
                };
                dbContext.Wydawnictwa.Add(ph);
                dbContext.SaveChanges();
                return "\nWydawnictwo zostało dodane pomyślnie.";
            }
            catch (Exception ex)
            {
                return $"\nWystąpił błąd podczas dodawania wydawnictwa: {ex.Message}";
            }
        }

        public string DeleteUnit(int _pubhouseID)
        {
            try
            {
                var toRmv = dbContext.Wydawnictwa.FirstOrDefault(a => a.Id_Wyd == _pubhouseID);
                dbContext.Wydawnictwa.Remove(toRmv);
                dbContext.SaveChanges();
                return "\nWydawnictwo zostało usunięte pomyślnie.";
            }
            catch (Exception ex)
            {
                return $"\nWystąpił błąd podczas usuwania wydawnictwa: {ex.Message}";
            }
        }

        public string ModifyUnit(int pubhouseId, PublishingHouseMoifyParams parameters)
        {
            try
            {
                var pubhouseToModify = dbContext.Wydawnictwa.FirstOrDefault(a => a.Id_Wyd == pubhouseId);

                if (pubhouseToModify == null)
                {
                    return "Nie znaleziono wydawnictwa o podanym numerze ID.";
                }

                if (parameters.Nazwa != null)
                {
                    pubhouseToModify.Nazwa = parameters.Nazwa;
                }
                if (parameters.Kraj_poch != null)
                {
                    pubhouseToModify.Kraj_poch = parameters.Kraj_poch;
                }
                if (parameters.Miasto != null)
                {
                    pubhouseToModify.Miasto = parameters.Miasto;
                }
                if (parameters.Adres != null)
                {
                    pubhouseToModify.Adres = parameters.Adres;
                }
                if (parameters.rok_zal.HasValue)
                {
                    pubhouseToModify.rok_zal = parameters.rok_zal.Value;
                }
                if (parameters.aktywne.HasValue)
                {
                    pubhouseToModify.aktywne = parameters.aktywne.Value;
                }

                dbContext.SaveChanges();
                return "Wydawnictwo zostało zmodyfikowane pomyślnie.";
            }
            catch (Exception ex)
            {
                return $"Wystąpił błąd podczas edytowania wydawnictwa: {ex.Message}";
            }
        }

        public IQueryable<Wydawnictwa> SearchingForUnit(string _phrase)
        {
            return dbContext.Wydawnictwa.Where(a => a.Nazwa.Contains(_phrase));
        }

        public Wydawnictwa GetPubHouseById(int id)
        {
            return dbContext.Wydawnictwa.FirstOrDefault(a => a.Id_Wyd == id);
        }

        public bool UnitExist(int _pubhouseID)
        {
            var searching = dbContext.Wydawnictwa.FirstOrDefault(a => a.Id_Wyd == _pubhouseID);
            if (searching != null) return false;
            return true;
        }
    }
}

