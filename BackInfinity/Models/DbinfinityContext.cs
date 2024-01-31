using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackInfinity.Models;

public partial class DbinfinityContext : DbContext
{
    public DbinfinityContext()
    {
    }

    public DbinfinityContext(DbContextOptions<DbinfinityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => e.IdAcces).HasName("PK__Access__7ACD6400AB63889D");

            entity.ToTable("Access");

            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.IdAppointment).HasName("PK__Appointm__44E34BD43D8206EB");

            entity.ToTable("Appointment");

            entity.Property(e => e.IdAppointment).HasColumnName("idAppointment");
            entity.Property(e => e.EstatePay)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HorAppoint).HasColumnType("datetime");
            entity.Property(e => e.HorCreate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NameUs)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdServiceNavigation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.IdService)
                .HasConstraintName("FK__Appointme__IdSer__5DCAEF64");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Service__3214EC07962B46AC");

            entity.ToTable("Service");

            entity.Property(e => e.Description1).IsUnicode(false);
            entity.Property(e => e.Description2).IsUnicode(false);
            entity.Property(e => e.Image1).IsUnicode(false);
            entity.Property(e => e.Image2).IsUnicode(false);
            entity.Property(e => e.Image3).IsUnicode(false);
            entity.Property(e => e.NameService)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
