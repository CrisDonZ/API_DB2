using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class JugadorDTO
    {
        [Required]
        public int JugadorId { get; set; }

        public string Nombre { get; set; } = null!;

        public int Edad { get; set; }

        public string Posicion { get; set; } = null!;

        public int EquipoId { get; set; }

    }
}
