using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MDAMMA20241103.Models;

namespace MDAMMA20241103.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly Amma20240311dbContext _context;

        public EmpleadosController(Amma20240311dbContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empleados.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(s => s.DetalleEmpleados)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewBag.Accion = "Details";
            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            var empleado = new Empleado();
            empleado.DetalleEmpleados = new List<DetalleEmpleado>();
            empleado.DetalleEmpleados.Add(new DetalleEmpleado
            {
                
            });
            ViewBag.Accion = "Create";
            return View(empleado);
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,FechaContratacion,Salario,DetalleEmpleados")] Empleado empleado)
        {
            
            _context.Add(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult AgregarDetalles([Bind("Id,Nombre,Apellido,Email,FechaContratacion,Salario,DetalleEmpleados")] Empleado empleado, string accion)
        {
            empleado.DetalleEmpleados.Add(new DetalleEmpleado {});
            ViewBag.Accion = accion;
            return View(accion, empleado);
        }

        public ActionResult EliminarDetalles([Bind("Id,Nombre,Apellido,Email,FechaContratacion,Salario,DetalleEmpleados")] Empleado empleado,
        int index, string accion)
        {
            var det = empleado.DetalleEmpleados.ElementAtOrDefault(index);
            if (accion == "Edit" && det.Id > 0)
            {
                det.Id = det.Id * -1;
            }
            else
            {
                empleado.DetalleEmpleados.Remove(det);
            }

            ViewBag.Accion = accion;
            return View(accion, empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(s => s.DetalleEmpleados)
                .FirstAsync(s => s.Id == id);
               
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Email,FechaContratacion,Salario,DetalleEmpleados")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            try
            {
                // Obtener los datos de la base de datos que van a ser modificados
                var facturaUpdate = await _context.Empleados
                        .Include(s => s.DetalleEmpleados)
                        .FirstAsync(s => s.Id == empleado.Id);
                facturaUpdate.Nombre = empleado.Nombre;
                facturaUpdate.Apellido = empleado.Apellido;
                facturaUpdate.Email = empleado.Email;
                facturaUpdate.FechaContratacion = empleado.FechaContratacion;
                facturaUpdate.Salario = empleado.Salario;
                // Obtener todos los detalles que seran nuevos y agregarlos a la base de datos
                var detNew = empleado.DetalleEmpleados.Where(s => s.Id == 0);
                foreach (var d in detNew)
                {
                    facturaUpdate.DetalleEmpleados.Add(d);
                }
                // Obtener todos los detalles que seran modificados y actualizar a la base de datos
                var detUpdate = empleado.DetalleEmpleados.Where(s => s.Id > 0);
                foreach (var d in detUpdate)
                {
                    var det = facturaUpdate.DetalleEmpleados.FirstOrDefault(s => s.Id == d.Id);
                    det.Nombre = d.Nombre;
                    det.Apellido = d.Apellido;
                    det.Telefono = d.Telefono;
                    det.Email = d.Email;
                }
                // Obtener todos los detalles que seran eliminados y actualizar a la base de datos
                var delDet = empleado.DetalleEmpleados.Where(s => s.Id < 0).ToList();
                if (delDet != null && delDet.Count > 0)
                {
                    foreach (var d in delDet)
                    {
                        d.Id = d.Id * -1;
                        var det = facturaUpdate.DetalleEmpleados.FirstOrDefault(s => s.Id == d.Id);
                        _context.Remove(det);
                        // facturaUpdate.DetFacturaVenta.Remove(det);
                    }
                }
                // Aplicar esos cambios a la base de datos
                _context.Update(facturaUpdate);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmpleadoExists(empleado.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
