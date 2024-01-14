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

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=Connection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
