using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Reciclajejuego.AppMVC.Models;

public partial class ReciclajeJuegoContext : DbContext
{
    public ReciclajeJuegoContext()
    {
    }

    public ReciclajeJuegoContext(DbContextOptions<ReciclajeJuegoContext> options)
        : base(options)
    {
    }

    // ✅ DbSets corregidos
    public virtual DbSet<Ajuste> Ajustes { get; set; }
    public virtual DbSet<Contenedor> Contenedores { get; set; }
    public virtual DbSet<DetallesModo> DetallesModoJuego { get; set; }
    public virtual DbSet<Juego> Juegos { get; set; }
    public virtual DbSet<ModoJuego> ModosJuego { get; set; }
    public virtual DbSet<Residuo> Residuos { get; set; }
    public virtual DbSet<Usuarios> Usuarios { get; set; }
    public virtual DbSet<Rol> Rol { get; set; }
    public virtual DbSet<MejorPuntaje> MejorPuntaje { get; set; }
    public virtual DbSet<RecuperacionContrasenas> RecuperacionContrasenas { get; set; }
    public IEnumerable Roles { get; internal set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ===== ROLES =====
        modelBuilder.Entity<Rol>(entity =>
        {
            entity.ToTable("Roles");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(e => e.Nombre).IsUnique();
        });

        // ===== USUARIOS =====
        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Correo).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Contrasena).HasMaxLength(255).IsRequired();

            entity.Property(e => e.EsCuentaGoogle).HasDefaultValue(false);
            entity.Property(e => e.Estado).HasDefaultValue((byte)1);

            entity.HasIndex(e => e.Correo).IsUnique();

            entity.HasOne(d => d.Rol)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId);
        });

        // ===== CONTENEDORES =====
        modelBuilder.Entity<Contenedor>(entity =>
        {
            entity.ToTable("Contenedores");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.TipoReciclaje)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsRequired();

            entity.HasIndex(e => e.TipoReciclaje).IsUnique();
        });

        // ===== MODOS DE JUEGO =====
        modelBuilder.Entity<ModoJuego>(entity =>
        {
            entity.ToTable("ModosJuego");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasIndex(e => e.Nombre).IsUnique();
        });

        // ===== DETALLES MODO JUEGO =====
        modelBuilder.Entity<DetallesModo>(entity =>
        {
            entity.ToTable("DetallesModoJuego");
            entity.HasKey(e => e.Id);

            entity.HasOne(d => d.ModoJuego)
                .WithMany(p => p.DetallesModo)
                .HasForeignKey(d => d.ModoJuegoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ===== RESIDUOS =====
        modelBuilder.Entity<Residuo>(entity =>
        {
            entity.ToTable("Residuos");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Descripcion).HasMaxLength(500).IsRequired();
            entity.Property(e => e.ImagenUrl).HasMaxLength(255);
            entity.Property(e => e.Puntos).HasDefaultValue(10);

            entity.HasOne(d => d.Contenedor)
                .WithMany(p => p.Residuos)
                .HasForeignKey(d => d.ContenedorId);
        });

        // ===== JUEGOS =====
        modelBuilder.Entity<Juego>(entity =>
        {
            entity.ToTable("Juegos");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.PuntuacionFinal).HasDefaultValue(0);
            entity.Property(e => e.FechaInicio).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Activo");

            entity.HasOne(d => d.Usuarios)
                .WithMany(p => p.Juegos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.ModoJuego)
                .WithMany(p => p.Juegos)
                .HasForeignKey(d => d.ModoJuegoId);
        });

        // ===== AJUSTES =====
        modelBuilder.Entity<Ajuste>(entity =>
        {
            entity.ToTable("Ajustes");
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.UsuarioId).IsUnique();

            entity.HasOne(d => d.Usuario)
                .WithOne(p => p.Ajuste)
                .HasForeignKey<Ajuste>(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ===== MEJORES PUNTAJES =====
        modelBuilder.Entity<MejorPuntaje>(entity =>
        {
            entity.ToTable("MejoresPuntajes");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Puntaje).HasDefaultValue(0);
            entity.Property(e => e.FechaAlcanzado).HasDefaultValueSql("GETDATE()");

            entity.HasIndex(e => new { e.UsuarioId, e.ModoJuegoId }).IsUnique();

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.MejoresPuntajes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.ModoJuego)
                .WithMany(p => p.MejoresPuntajes)
                .HasForeignKey(d => d.ModoJuegoId);
        });

        // ===== RECUPERACION CONTRASEÑA =====
        modelBuilder.Entity<RecuperacionContrasenas>(entity =>
        {
            entity.ToTable("RecuperacionContrasena");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsRequired();

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("GETDATE()");

            entity.Property(e => e.Usado)
                .HasDefaultValue(false);

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.RecuperacionContrasenas)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
