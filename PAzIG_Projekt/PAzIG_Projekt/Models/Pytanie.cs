using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PAzIG_Projekt.Models
{
    public class Pytanie
    {
        public int ID { get; set; }

        public string Treść { get; set; }

        public string A { get; set; }

        public string B { get; set; }

        public string C { get; set; }

        public string D { get; set; }

        public string Odp_pop { get; set; }
    }
}