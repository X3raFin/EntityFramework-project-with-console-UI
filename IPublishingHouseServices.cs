using EF_lab4.Services;
using EF_lab4.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_lab4
{
    internal interface IPublishingHouseServices
    {
        public IEnumerable<Wydawnictwa> WriteUpStatus();
        public string AddNewUnit(string name, string origin, string city, string address, int yearOfCommencement, bool active);
        public string DeleteUnit(int _pubhouseID);
        public string ModifyUnit(int pubhouseId, PublishingHouseMoifyParams parameters);
        public IQueryable<Wydawnictwa> SearchingForUnit(string _phrase);
        public Wydawnictwa GetPubHouseById(int id);
        public bool UnitExist(int _pubhouseID);
    }
}
