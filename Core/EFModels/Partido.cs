using System;
using System.Collections.Generic;

namespace Core.EFModels;
public partial class Partido
{
    public int PartidoId { get; set; }

    public int EquipoLocal { get; set; }

    public int EquipoVisitante { get; set; }

    public DateOnly Fecha { get; set; }

    public string Resultado { get; set; } = null!;

    public virtual Equipo EquipoLocalNavigation { get; set; } = null!;

    public virtual Equipo EquipoVisitanteNavigation { get; set; } = null!;
}
