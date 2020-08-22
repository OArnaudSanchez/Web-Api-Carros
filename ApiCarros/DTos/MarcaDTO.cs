using CarrosData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCarros.DTos
{
    public class MarcaDTO
    {
        public int id { get; set; }

        public string nombre { get; set; }

        public virtual List<Carro> Carros { get; set; }
    }
}
