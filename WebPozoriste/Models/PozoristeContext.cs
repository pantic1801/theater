using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebPozoriste.Models
{
    public partial class PozoristeContext : DbContext
    {
      

        public PozoristeContext()
        {
        }

        public PozoristeContext(DbContextOptions<PozoristeContext> opcije)
            : base(opcije)
        {
        }

        public virtual DbSet<Karta> Karta { get; set; }
        public virtual DbSet<Korisnik> Korisnik { get; set; }
        public virtual DbSet<Porudzbina> Porudzbina { get; set; }
        public virtual DbSet<Predstava> Predstava { get; set; }
        public virtual DbSet<Slike> Slike { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Karta>(entity =>
            {
                entity.HasOne(d => d.Porudzbina)
                    .WithMany(p => p.Karta)
                    .HasForeignKey(d => d.PorudzbinaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Karta__Porudzbin__0C85DE4D");

                entity.HasOne(d => d.Predstava)
                    .WithMany(p => p.Karta)
                    .HasForeignKey(d => d.PredstavaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Karta__Predstava__0D7A0286");
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.Property(e => e.KorisnikId).ValueGeneratedNever();

                entity.Property(e => e.Drzava).HasDefaultValueSql("('Srbija')");

                entity.Property(e => e.Grad).HasDefaultValueSql("('Beograd')");
            });

            modelBuilder.Entity<Porudzbina>(entity =>
            {
                entity.Property(e => e.DatumPorucivanja).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Porudzbina)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Porudzbin__Koris__06CD04F7");
            });

            modelBuilder.Entity<Predstava>(entity =>
            {
                entity.Property(e => e.Opis).IsUnicode(false);
            });

            modelBuilder.Entity<Slike>(entity =>
            {
                entity.HasOne(d => d.Predstava)
                    .WithMany(p => p.SlikeNavigation)
                    .HasForeignKey(d => d.PredstavaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Slike__Predstava__10566F31");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
