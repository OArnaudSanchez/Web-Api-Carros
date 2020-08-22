using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarrosData.Models
{
    public class Marca
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido"), StringLength(50)]
        public string nombre { get; set; }
        
        //Una marca puede pertenecer a varios carros
        public List<Carro> Carros { get; set; }
    }
}
