using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SRBD_LB3.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmCompany> FilmCompanies { get; set; }

    public virtual DbSet<FilmsLog> FilmsLogs { get; set; }

    public virtual DbSet<Screening> Screenings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-VV6KNLD;Database=Cinema;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC143CCCCD25");

            entity.Property(e => e.AuthorId)
                .ValueGeneratedNever()
                .HasColumnName("AuthorID");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.FilmId).HasName("PK__Films__6D1D229C7A3A76E7");

            entity.ToTable(tb => tb.HasTrigger("tr_Films_InsertProtection"));

            entity.Property(e => e.FilmId).HasColumnName("FilmID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WatchCount).HasDefaultValue(0);

            entity.HasOne(d => d.Author).WithMany(p => p.Films)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Films_Authors");

            entity.HasOne(d => d.Company).WithMany(p => p.Films)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Films_Companies");
        });

        modelBuilder.Entity<FilmCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__FilmComp__2D971C4C6414201A");

            entity.Property(e => e.CompanyId)
                .ValueGeneratedNever()
                .HasColumnName("CompanyID");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Indormation)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FilmsLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__FilmsLog__9E2397E0155C64F1");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.AttemptDate)
                .HasColumnType("datetime")
                .HasColumnName("attempt_date");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(255)
                .HasColumnName("error_message");
            entity.Property(e => e.FilmName)
                .HasMaxLength(255)
                .HasColumnName("film_name");
        });

        modelBuilder.Entity<Screening>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Screenin__3214EC27552CA1EA");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("tr_Screening_CompanyDescription");
                    tb.HasTrigger("tr_Screenings_InsertDate");
                });

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.FilmId).HasColumnName("FilmID");
            entity.Property(e => e.ScreeningDate).HasColumnType("datetime");

            entity.HasOne(d => d.Film).WithMany(p => p.Screenings)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK_Film");
        });
        modelBuilder.Entity<AuthorPriceCountryCountResult>().HasNoKey().ToTable("AuthorPriceCountryCountResult", t => t.ExcludeFromMigrations());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
