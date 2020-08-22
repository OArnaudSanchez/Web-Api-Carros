using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarrosData.Models
{
    public class Carro
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido"), StringLength(50)]
        public string modelo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int anio { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido"), StringLength(50)]
        public string color { get; set; }

        //Un carro tiene una marca
        [ForeignKey("Marcaid")]
        public int Marcaid { get; set; }
        public Marca Marca { get; set; }
    }
}
