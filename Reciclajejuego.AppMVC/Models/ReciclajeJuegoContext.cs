using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Reciclajejuego.AppMVC.Models;

public partial class ReciclajeJuegoContext : DbContext
{
    internal object contenedor;

    public ReciclajeJuegoContext()
    {
    }

    public ReciclajeJuegoContext(DbContextOptions<ReciclajeJuegoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ajuste> Ajustes { get; set; }

    public virtual DbSet<Contenedor> Contenedors { get; set; }

    public virtual DbSet<DetallesModo> DetallesModos { get; set; }

    public virtual DbSet<Juego> Juegos { get; set; }

    public virtual DbSet<ModoJuegos> ModoJuegos { get; set; }

    public virtual DbSet<Residuo> Residuos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ajuste>(entity =>
        {
            entity.HasKey(e => e.AjustesId).HasName("PK__ajustes__A16CB44DF02AB26F");

            entity.ToTable("ajustes");

            entity.HasIndex(e => e.UsuarioId, "UQ__ajustes__A5B1ABAF6893CD46").IsUnique();

            entity.Property(e => e.AjustesId).HasColumnName("ajustesID");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioID");
            entity.Property(e => e.VolumenEfectos).HasColumnName("volumenEfectos");
            entity.Property(e => e.VolumenGeneral).HasColumnName("volumenGeneral");
            entity.Property(e => e.VolumenMusica).HasColumnName("volumenMusica");

            entity.HasOne(d => d.Usuario).WithOne(p => p.Ajuste)
                .HasForeignKey<Ajuste>(d => d.UsuarioId)
                .HasConstraintName("FK__ajustes__usuario__534D60F1");
        });

        modelBuilder.Entity<Contenedor>(entity =>
        {
            entity.HasKey(e => e.ContenedorId).HasName("PK__contened__2FA199F7FB5D0EFE");

            entity.ToTable("contenedor");

            entity.HasIndex(e => e.TipoReciclaje, "UQ__contened__98E9429C8370BF4B").IsUnique();

            entity.Property(e => e.ContenedorId).HasColumnName("contenedorID");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.TipoReciclaje)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoReciclaje");
        });

        modelBuilder.Entity<DetallesModo>(entity =>
        {
            entity.HasKey(e => e.ModoJuegoId).HasName("PK__detalles__751590887E8576D2");

            entity.ToTable("detalles_modo");

            entity.Property(e => e.ModoJuegoId)
                .ValueGeneratedNever()
                .HasColumnName("modoJuegoID");
            entity.Property(e => e.ComboMaximo).HasColumnName("comboMaximo");
            entity.Property(e => e.TiempoLimite).HasColumnName("tiempoLimite");
            entity.Property(e => e.Vidas).HasColumnName("vidas");

            entity.HasOne(d => d.ModoJuego).WithOne(p => p.DetallesModo)
                .HasForeignKey<DetallesModo>(d => d.ModoJuegoId)
                .HasConstraintName("FK__detalles___modoJ__59063A47");
        });

        modelBuilder.Entity<Juego>(entity =>
        {
            entity.HasKey(e => e.JuegoId).HasName("PK__juego__B68069B5CDD02967");

            entity.ToTable("juego");

            entity.Property(e => e.JuegoId).HasColumnName("juegoID");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Activo")
                .HasColumnName("estado");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaInicio");
            entity.Property(e => e.ModoJuegoId).HasColumnName("modoJuegoID");
            entity.Property(e => e.PuntuacionActual).HasColumnName("puntuacionActual");
            entity.Property(e => e.UsuarioId).HasColumnName("usuarioID");

            entity.HasOne(d => d.ModoJuego).WithMany(p => p.Juegos)
                .HasForeignKey(d => d.ModoJuegoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__juego__modoJuego__6754599E");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Juegos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__juego__usuarioID__66603565");
        });

        modelBuilder.Entity<ModoJuegos>(entity =>
        {
            entity.HasKey(e => e.ModoJuegoId).HasName("PK__modo_jue__751590880755731C");

            entity.ToTable("modo_juego");

            entity.HasIndex(e => e.Nombre, "UQ__modo_jue__72AFBCC62F4FD23B").IsUnique();

            entity.Property(e => e.ModoJuegoId).HasColumnName("modoJuegoID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Residuo>(entity =>
        {
            entity.HasKey(e => e.ResiduoId).HasName("PK__residuo__B9516E472974259E");

            entity.ToTable("residuo");

            entity.Property(e => e.ResiduoId).HasColumnName("residuoID");
            entity.Property(e => e.ContenedorId).HasColumnName("contenedorID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Imagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagen");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Puntos)
                .HasDefaultValue(10)
                .HasColumnName("puntos");

            entity.HasOne(d => d.Contenedor).WithMany(p => p.Residuos)
                .HasForeignKey(d => d.ContenedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__residuo__contene__5FB337D6");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__usuario__A5B1ABAEB259978B");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "UQ__usuario__2A586E0BE80BD6F9").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuarioID");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.CuentaGoogle).HasColumnName("cuentaGoogle");
            entity.Property(e => e.MejorPuntaje).HasColumnName("mejorPuntaje");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
