using System;
using System.Collections.Generic;

namespace Core.EFModels;

public partial class Equipo
{
    public int EquipoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Dt { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public virtual ICollection<Jugadore> Jugadores { get; set; } = new List<Jugadore>();

    public virtual ICollection<Partido> PartidoEquipoLocalNavigations { get; set; } = new List<Partido>();

    public virtual ICollection<Partido> PartidoEquipoVisitanteNavigations { get; set; } = new List<Partido>();
}
