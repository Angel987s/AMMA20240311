using System;
using System.Collections.Generic;

namespace MDAMMA20241103.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Email { get; set; }

    public DateOnly? FechaContratacion { get; set; }

    public decimal? Salario { get; set; }

    public virtual ICollection<DetalleEmpleado> DetalleEmpleados { get; set; } = new List<DetalleEmpleado>();
}
