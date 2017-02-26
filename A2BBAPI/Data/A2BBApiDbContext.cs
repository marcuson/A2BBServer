using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using A2BBAPI.Models;

namespace A2BBAPI.Data
{
    /// <summary>
    /// The A2BB API DB context.
    /// </summary>
    public partial class A2BBApiDbContext : DbContext
    {
        #region Public properties
        /// <summary>
        /// The devices in the DB.
        /// </summary>
        public virtual DbSet<Device> Device { get; set; }

        /// <summary>
        /// The in/out records in the DB.
        /// </summary>
        public virtual DbSet<InOut> InOut { get; set; }

        /// <summary>
        /// The subjects (users) in the DB.
        /// </summary>
        public virtual DbSet<Subject> Subject { get; set; }
        #endregion

        #region Private methods
        /// <summary>
        /// Called before model creation.
        /// </summary>
        /// <param name="builder">The model builder.</param>
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

                entity.HasOne(e => e.Device)
                    .WithMany(p => p.InOut)
                    .HasForeignKey(e => e.DeviceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("in_out_device_fk");

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
        #endregion

        #region Public methods
        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        /// <param name="options">The DB context builder options.</param>
        public A2BBApiDbContext(DbContextOptions<A2BBApiDbContext> options) : base(options)
        {
        }
        #endregion
    }
}