using System;
using System.Collections.Generic;

namespace MDAMMA20241103.Models;

public partial class DetalleEmpleado
{
    public int Id { get; set; }

    public int? EmpleadoId { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public virtual Empleado? Empleado { get; set; }
}
