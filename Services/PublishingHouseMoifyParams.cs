using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_lab4.Services
{
    internal class PublishingHouseMoifyParams
    {
        public string? Nazwa {  get; set; }
        public string? Kraj_poch { get; set; }
        public string? Miasto { get; set; }
        public string? Adres {  get; set; }
        public int? rok_zal {  get; set; }
        public bool? aktywne {  get; set; }
    }
}
