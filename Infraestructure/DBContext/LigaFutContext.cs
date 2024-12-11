using System;
using System.Collections.Generic;
using Core.EFModels;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.DBContext;

public partial class LigaFutContext : DbContext
{
    public LigaFutContext()
    {
    }

    public LigaFutContext(DbContextOptions<LigaFutContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Jugadore> Jugadores { get; set; }

    public virtual DbSet<Partido> Partidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-8GBGS8F\\SQLEXPRESS;Database=LigaFut;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.EquipoId).HasName("PK__Equipo__DE8A0BFF40DD46A6");

            entity.ToTable("Equipo");

            entity.Property(e => e.EquipoId)
                .ValueGeneratedNever()
                .HasColumnName("EquipoID");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.Dt)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("DT");
            entity.Property(e => e.Nombre)
                .HasMaxLength(55)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Jugadore>(entity =>
        {
            entity.HasKey(e => e.JugadorId).HasName("PK__Jugadore__4B57524252717494");

            entity.Property(e => e.JugadorId)
                .ValueGeneratedNever()
                .HasColumnName("JugadorID");
            entity.Property(e => e.EquipoId).HasColumnName("EquipoID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.Posicion)
                .HasMaxLength(55)
                .IsUnicode(false);

            entity.HasOne(d => d.Equipo).WithMany(p => p.Jugadores)
                .HasForeignKey(d => d.EquipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Jugadores__Equip__3B75D760");
        });

        modelBuilder.Entity<Partido>(entity =>
        {
            entity.HasKey(e => e.PartidoId).HasName("PK__Partidos__DBC2E8D607BADD3F");

            entity.Property(e => e.PartidoId)
                .ValueGeneratedNever()
                .HasColumnName("PartidoID");
            entity.Property(e => e.Resultado)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.EquipoLocalNavigation).WithMany(p => p.PartidoEquipoLocalNavigations)
                .HasForeignKey(d => d.EquipoLocal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Partidos__Equipo__3C69FB99");

            entity.HasOne(d => d.EquipoVisitanteNavigation).WithMany(p => p.PartidoEquipoVisitanteNavigations)
                .HasForeignKey(d => d.EquipoVisitante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Partidos__Equipo__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
