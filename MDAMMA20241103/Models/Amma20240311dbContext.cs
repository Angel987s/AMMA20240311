using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MDAMMA20241103.Models;

public partial class Amma20240311dbContext : DbContext
{
    public Amma20240311dbContext()
    {
    }

    public Amma20240311dbContext(DbContextOptions<Amma20240311dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleEmpleado> DetalleEmpleados { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleEmpleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DetalleE__3214EC279C04EDDD");

            entity.ToTable("DetalleEmpleado");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Empleado).WithMany(p => p.DetalleEmpleados)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DetalleEm__Emple__267ABA7A");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3214EC2733334DC5");

            entity.ToTable("Empleado");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Salario).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
