﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

public partial class PersonaDbContext : DbContext
{
    public PersonaDbContext()
    {
    }

    public PersonaDbContext(DbContextOptions<PersonaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudio> Estudios { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Profesion> Profesions { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=persona_db;User Id=sa;Password=P@ssword;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Estudios__3213E83FD3E69912");

            entity.HasIndex(e => e.CcPer, "estudio_persona_fk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CcPer).HasColumnName("cc_per");
            entity.Property(e => e.IdProf).HasColumnName("id_prof");
            entity.Property(e => e.Univer)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("univer");

            entity.HasOne(d => d.IdProfNavigation).WithMany(p => p.Estudios)
                .HasForeignKey(d => d.IdProf)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Estudios__id_pro__398D8EEE");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Cc).HasName("PK__Persona__3213666D2F0951D9");

            entity.ToTable("Persona");

            entity.Property(e => e.Cc)
                .ValueGeneratedNever()
                .HasColumnName("cc");
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Profesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profesio__3213E83F77DED9D0");

            entity.ToTable("Profesion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dest)
                .HasMaxLength(90)
                .IsUnicode(false)
                .HasColumnName("dest");
            entity.Property(e => e.Nom)
                .HasMaxLength(90)
                .IsUnicode(false)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasKey(e => e.Num).HasName("PK__Telefono__DF908D65FD82F3A0");

            entity.ToTable("Telefono");

            entity.HasIndex(e => e.Duenio, "telefono_persona_fk");

            entity.Property(e => e.Num)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("num");
            entity.Property(e => e.Duenio).HasColumnName("duenio");
            entity.Property(e => e.Oper)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("oper");

            entity.HasOne(d => d.DuenioNavigation).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.Duenio)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Telefono__duenio__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}