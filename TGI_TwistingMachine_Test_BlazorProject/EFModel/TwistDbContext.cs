using Microsoft.EntityFrameworkCore;

namespace TGI_TwistingMachine_Test_BlazorProject.EFModel
{
    public class TwistDbContext : DbContext
    {
        public TwistDbContext(DbContextOptions<TwistDbContext> options) : base(options)
        {

        }

        public virtual DbSet<TwistingMachineTable> TwistingMachineTables { get; set; }

        public virtual DbSet<CakeTable> CakeTables { get; set; }

        public virtual DbSet<BobbinTable> BobbinTables { get; set; }

        public virtual DbSet<LogTable> LogTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TwistingMachineTable>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Machine).HasConversion<int>().IsRequired();
                entity.HasIndex(e => e.Machine).IsUnique();

                entity.HasMany(e => e.CakeTables).WithOne(e => e.MachineTable).HasForeignKey(e => e.MachineId);

                entity.HasMany(e => e.BobbinTables).WithOne(e => e.MachineTable).HasForeignKey(e => e.MachineId);

            });

            modelBuilder.Entity<CakeTable>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CakeRFID).HasMaxLength(500);

                entity.Property(e => e.Side).HasConversion<int>().IsRequired();

                entity.Property(e => e.AmrCellName).HasMaxLength(500);

                entity.Property(e => e.SilkStatus).HasConversion<int>().IsRequired();

            });

            modelBuilder.Entity<BobbinTable>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.BobbinRFID).HasMaxLength(500);

                entity.Property(e => e.Side).HasConversion<int>().IsRequired();

                entity.Property(e => e.AmrCellName).HasMaxLength(500);

                entity.Property(e => e.SilkStatus).HasConversion<int>().IsRequired();

            });

            modelBuilder.Entity<LogTable>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityColumn();

                entity.Property(e => e.LogType).HasMaxLength(500);

                entity.Property(e => e.Equipment).HasMaxLength(500);

                entity.Property(e => e.RecordTime).HasColumnType("datetime2");

            });
        }
    }
}
