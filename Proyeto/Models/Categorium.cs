using System;
using System.Collections.Generic;

namespace Proyeto.Models;

public partial class Categorium
{
    public int IdCategoria { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
}
