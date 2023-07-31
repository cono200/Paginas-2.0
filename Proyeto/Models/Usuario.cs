using System;
using System.Collections.Generic;

namespace Proyeto.Models;

public partial class Usuario
{
    public string NombreUsuario { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int? IdAutor1 { get; set; }

    public virtual ICollection<DocumentoUsuario> DocumentoUsuarios { get; set; } = new List<DocumentoUsuario>();

    public virtual Autor? IdAutor1Navigation { get; set; }
}
