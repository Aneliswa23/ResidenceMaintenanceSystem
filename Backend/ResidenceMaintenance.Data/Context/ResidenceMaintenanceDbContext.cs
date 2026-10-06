using Microsoft.EntityFrameworkCore;
using ResidenceMaintenance.Data.Entities;

namespace ResidenceMaintenance.Data.Context;

public class ResidenceMaintenanceDbContext : DbContext
{
    public ResidenceMaintenanceDbContext(
        DbContextOptions<ResidenceMaintenanceDbContext> options)
        : base(options)
    {
    }

    public DbSet<Residence> Residences => Set<Residence>();

    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<BedSpace> BedSpaces => Set<BedSpace>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Residence>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.Address)
                .HasMaxLength(250);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.RoomNumber)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(x => x.Residence)
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.ResidenceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BedSpace>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.BedNumber)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(x => x.Room)
                .WithMany(x => x.BedSpaces)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}