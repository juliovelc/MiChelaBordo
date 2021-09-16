using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MiChelaBordo.Models
{
    public partial class MiChelaBordoContext : DbContext
    {
        public MiChelaBordoContext()
        {
        }

        public MiChelaBordoContext(DbContextOptions<MiChelaBordoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Concept> Concepts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Road> Roads { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-11J5CAP\\SQLEXPRESS;Database=MiChelaBordo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Concept>(entity =>
            {
                entity.ToTable("Concept");

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.PurchaseId).HasColumnName("Purchase_Id");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Unit_Price");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductId");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.Concepts)
                    .HasForeignKey(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseId");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdMail);

                entity.ToTable("Customer");

                entity.Property(e => e.IdMail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Id_Mail");

                entity.Property(e => e.CompleteName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Complete_Name");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Rfc)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("RFC");

                entity.Property(e => e.TelNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Tel_Number");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Product_Name");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Unit_Price");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.ToTable("Purchase");

                entity.Property(e => e.ConceptId).HasColumnName("Concept_Id");

                entity.Property(e => e.PurchaseTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Purchase_Time")
                    .HasDefaultValueSql("(format(getdate(),'yyyy-MM-dd HH:MM'))");

                entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UserMail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("User_Mail");

                entity.HasOne(d => d.UserMailNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.UserMail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerId");
            });

            modelBuilder.Entity<Road>(entity =>
            {
                entity.ToTable("Road");

                entity.Property(e => e.AvailableCapacity).HasColumnName("Available_Capacity");

                entity.Property(e => e.RoadDescription)
                    .IsRequired()
                    .HasMaxLength(512)
                    .IsUnicode(false)
                    .HasColumnName("Road_Description");

                entity.Property(e => e.RoadName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Road_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
