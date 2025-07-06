using EF_lab4.Services;
using EF_lab4.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_lab4
{
    internal interface IAuthorsServices
    {
        public IEnumerable<Autorzy> WriteUpStatus();
        public string AddNewUnit(string name, string secondName, string origin);
        public string DeleteUnit(int _authorID);
        public string ModifyUnit(int authorId, AuthorsModifyParams parameters);
        public IQueryable<Autorzy> SearchingForUnit(string _phrase);
        public Autorzy GetAuthorById(int id);
        public bool UnitExist(int _authorID);
    }
}
