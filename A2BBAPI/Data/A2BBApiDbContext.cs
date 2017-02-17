using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using A2BBAPI.Models;

namespace A2BBAPI.Data
{
    public partial class A2BBApiDbContext : DbContext
    {
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<InOut> InOut { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }

        public A2BBApiDbContext(DbContextOptions<A2BBApiDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("device", "a2bb_api");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Enabled).HasColumnName("enabled");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Device)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("device_subject_fk");
            });

            modelBuilder.Entity<InOut>(entity =>
            {
                entity.ToTable("in_out", "a2bb_api");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeviceId).HasColumnName("device_id");

                entity.Property(e => e.OnDate)
                    .HasColumnName("on_date")
                    .HasColumnType("timestamptz");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject", "a2bb_api");

                entity.Property(e => e.Id).HasColumnName("id");
            });
        }
    }
}