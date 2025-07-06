using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_lab4.Tables
{
    public class Książki
    {
        [Key]
        public int Id_Ks { get; set; }

        [Required]
        public int WydawnictwoId { get; set; }
        public Wydawnictwa Wydawnictwo { get; set; } = null!;

        [Required]
        public int AutorId { get; set;}
        public Autorzy Autor { get; set; } = null!;

        [Required]
        public string Tytul { get; set; } = null!;

        public stan Stan_ks { get; set; } = stan.Dostępna;

        [Required]
        public decimal Cena { get; set; }
    }
        public enum stan { 
            Dostępna, 
            Wypożyczona, 
            Uszkodzona 
        };
}
