using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_lab4.Services
{
    internal class AuthorsModifyParams
    {
        public string? Imię {  get; set; }
        public string? Nazwisko { get; set; }
        public string? Pochodzenie { get; set; }
    }
}
