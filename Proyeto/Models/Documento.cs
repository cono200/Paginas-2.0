using System;
using System.Collections.Generic;

namespace Proyeto.Models;

public partial class Documento
{
    public int IdDocumento { get; set; }

    public string Estatus { get; set; } = null!;

    public string? CoAutor { get; set; }

    public int? IdCategoria1 { get; set; }

    public string Urldocumento { get; set; } = null!;

    public virtual ICollection<DocumentoUsuario> DocumentoUsuarios { get; set; } = new List<DocumentoUsuario>();

    public virtual Categorium? IdCategoria1Navigation { get; set; }
}
