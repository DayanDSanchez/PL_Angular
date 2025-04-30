using System;
using System.Collections.Generic;

namespace DL.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Edad { get; set; }

    public byte[]? Imagen { get; set; }

    public DateOnly? FechaNacimiento { get; set; }
}
