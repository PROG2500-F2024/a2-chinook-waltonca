﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Models;

namespace PROG2500_A2_Chinook.Data;

public partial class ChinookContext : DbContext
{
    public ChinookContext()
    {
    }

    public ChinookContext(DbContextOptions<ChinookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Chinook;Integrated Security=True;Trust Server Certificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.ToTable("Album");

            entity.HasIndex(e => e.ArtistId, "IFK_AlbumArtistId");

            entity.Property(e => e.AlbumId).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(160);

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Album__ArtistId__267ABA7A");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.ToTable("Artist");

            entity.Property(e => e.ArtistId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.HasIndex(e => e.SupportRepId, "IFK_CustomerSupportRepId");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(70);
            entity.Property(e => e.City).HasMaxLength(40);
            entity.Property(e => e.Company).HasMaxLength(80);
            entity.Property(e => e.Country).HasMaxLength(40);
            entity.Property(e => e.Email).HasMaxLength(60);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.FirstName).HasMaxLength(40);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.State).HasMaxLength(40);

            entity.HasOne(d => d.SupportRep).WithMany(p => p.Customers)
                .HasForeignKey(d => d.SupportRepId)
                .HasConstraintName("FK__Customer__Suppor__2C3393D0");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.HasIndex(e => e.ReportsTo, "IFK_EmployeeReportsTo");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(70);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.City).HasMaxLength(40);
            entity.Property(e => e.Country).HasMaxLength(40);
            entity.Property(e => e.Email).HasMaxLength(60);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.HireDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.State).HasMaxLength(40);
            entity.Property(e => e.Title).HasMaxLength(30);

            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo)
                .HasConstraintName("FK__Employee__Report__29572725");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.GenreId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoice");

            entity.HasIndex(e => e.CustomerId, "IFK_InvoiceCustomerId");

            entity.Property(e => e.InvoiceId).ValueGeneratedNever();
            entity.Property(e => e.BillingAddress).HasMaxLength(70);
            entity.Property(e => e.BillingCity).HasMaxLength(40);
            entity.Property(e => e.BillingCountry).HasMaxLength(40);
            entity.Property(e => e.BillingPostalCode).HasMaxLength(10);
            entity.Property(e => e.BillingState).HasMaxLength(40);
            entity.Property(e => e.InvoiceDate).HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("numeric(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Custome__30F848ED");
        });

        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.ToTable("InvoiceLine");

            entity.HasIndex(e => e.InvoiceId, "IFK_InvoiceLineInvoiceId");

            entity.HasIndex(e => e.TrackId, "IFK_InvoiceLineTrackId");

            entity.Property(e => e.InvoiceLineId).ValueGeneratedNever();
            entity.Property(e => e.UnitPrice).HasColumnType("numeric(10, 2)");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceLi__Invoi__403A8C7D");

            entity.HasOne(d => d.Track).WithMany(p => p.InvoiceLines)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceLi__Track__412EB0B6");
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.ToTable("MediaType");

            entity.Property(e => e.MediaTypeId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.ToTable("Playlist");

            entity.Property(e => e.PlaylistId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);

            entity.HasMany(d => d.Tracks).WithMany(p => p.Playlists)
                .UsingEntity<Dictionary<string, object>>(
                    "PlaylistTrack",
                    r => r.HasOne<Track>().WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlaylistT__Track__3D5E1FD2"),
                    l => l.HasOne<Playlist>().WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlaylistT__Playl__3C69FB99"),
                    j =>
                    {
                        j.HasKey("PlaylistId", "TrackId");
                        j.ToTable("PlaylistTrack");
                        j.HasIndex(new[] { "TrackId" }, "IFK_PlaylistTrackTrackId");
                    });
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.ToTable("Track");

            entity.HasIndex(e => e.AlbumId, "IFK_TrackAlbumId");

            entity.HasIndex(e => e.GenreId, "IFK_TrackGenreId");

            entity.HasIndex(e => e.MediaTypeId, "IFK_TrackMediaTypeId");

            entity.Property(e => e.TrackId).ValueGeneratedNever();
            entity.Property(e => e.Composer).HasMaxLength(220);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UnitPrice).HasColumnType("numeric(10, 2)");

            entity.HasOne(d => d.Album).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK__Track__AlbumId__37A5467C");

            entity.HasOne(d => d.Genre).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Track__GenreId__38996AB5");

            entity.HasOne(d => d.MediaType).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.MediaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Track__MediaType__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
