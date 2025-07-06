using EF_lab4.Services;
using EF_lab4.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_lab4
{
    internal interface IBookServices
    {
        public IEnumerable<Książki> WriteUpStatus();
        public string AddNewUnit(int _pub_house, int _author, string _title, decimal _price);
        public string DeleteUnit(int _bookID);
        public string ModifyUnit(int bookId, BookModifyParams parameters);
        public IQueryable<Książki> SearchingForUnit(string _phrase);
        public Książki GetBookById(int id);
        public bool UnitExist(int _bookID);
    }
}
