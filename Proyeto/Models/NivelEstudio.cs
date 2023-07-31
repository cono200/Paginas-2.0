using System;
using System.Collections.Generic;

namespace Proyeto.Models;

public partial class NivelEstudio
{
    public int NivelEstudiosId { get; set; }

    public string? NombreNivel { get; set; }

    public virtual ICollection<Autor> Autors { get; set; } = new List<Autor>();
}
