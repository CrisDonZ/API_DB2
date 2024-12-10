using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.EFModels;

public partial class Jugadore
{

    public int JugadorId { get; set; }

    public string Nombre { get; set; } = null!;

    public int Edad { get; set; }

    public string Posicion { get; set; } = null!;

    [ForeignKey("Equipo")]
    public int EquipoId { get; set; }

    public virtual Equipo Equipo { get; set; } = null!;
}
