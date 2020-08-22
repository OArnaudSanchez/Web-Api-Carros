using CarrosData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCarros.DTos
{
    public class CarroDTO
    {
        public int id { get; set; }

        public string modelo { get; set; }

        public int anio { get; set; }

        public string color { get; set; }
        public int Marcaid { get; set; }
        //public Marca Marca { get; set; }

    }
}
