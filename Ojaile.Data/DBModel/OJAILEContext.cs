using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ojaile.Data.DBModel
{
    public partial class OJAILEContext : DbContext
    {
        public OJAILEContext()
        {
        }

        public OJAILEContext(DbContextOptions<OJAILEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Country1> Countries1 { get; set; } = null!;
        public virtual DbSet<Lga> Lgas { get; set; } = null!;
        public virtual DbSet<PropertyDocument> PropertyDocuments { get; set; } = null!;
        public virtual DbSet<PropertyFeature> PropertyFeatures { get; set; } = null!;
        public virtual DbSet<PropertyImage> PropertyImages { get; set; } = null!;
        public virtual DbSet<PropertyItem> PropertyItems { get; set; } = null!;
        public virtual DbSet<PropertyType> PropertyTypes { get; set; } = null!;
        public virtual DbSet<PropertyUnit> PropertyUnits { get; set; } = null!;
        public virtual DbSet<ServiceAvailable> ServiceAvailables { get; set; } = null!;
        public virtual DbSet<ServicePropertyUnit> ServicePropertyUnits { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-HNJ5GHD;Database= OJAILE;Trusted_Connection = True;MultipleActiveResultSets = True;");
            }
         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Country1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Country");

                entity.Property(e => e.Name).HasMaxLength(10);
            });

            modelBuilder.Entity<Lga>(entity =>
            {
                entity.ToTable("LGA");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<PropertyDocument>(entity =>
            {
                entity.ToTable("PropertyDocument");

                entity.Property(e => e.DocumentUrl)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PropertyUnit)
                    .WithMany(p => p.PropertyDocuments)
                    .HasForeignKey(d => d.PropertyUnitId)
                    .HasConstraintName("FK_PropertyDocument_PropertyUnit");
            });

            modelBuilder.Entity<PropertyFeature>(entity =>
            {
                entity.ToTable("PropertyFeature");

                entity.Property(e => e.Icon)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PropertyImage>(entity =>
            {
                entity.ToTable("PropertyImage");

                entity.Property(e => e.Decription)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.HasOne(d => d.PropertyUnit)
                    .WithMany(p => p.PropertyImages)
                    .HasForeignKey(d => d.PropertyUnitId)
                    .HasConstraintName("FK_PropertyImage_PropertyUnit");
            });

            modelBuilder.Entity<PropertyItem>(entity =>
            {
                entity.ToTable("PropertyItem");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.PropertyName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.LgaNavigation)
                    .WithMany(p => p.PropertyItems)
                    .HasForeignKey(d => d.Lga)
                    .HasConstraintName("FK_PropertyItem_LGA");

                entity.HasOne(d => d.PropertyType)
                    .WithMany(p => p.PropertyItems)
                    .HasForeignKey(d => d.PropertyTypeId)
                    .HasConstraintName("FK_PropertyItem_PropertyType");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.PropertyItems)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("FK_PropertyItem_State");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PropertyItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_PropertyItem_AspNetUsers");
            });

            modelBuilder.Entity<PropertyType>(entity =>
            {
                entity.ToTable("PropertyType");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PropertyUnit>(entity =>
            {
                entity.ToTable("PropertyUnit");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.UnitDescription)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.PropertyUnits)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK_PropertyUnit_PropertyItem");
            });

            modelBuilder.Entity<ServiceAvailable>(entity =>
            {
                entity.ToTable("ServiceAvailable");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ServicePropertyUnit>(entity =>
            {
                entity.ToTable("ServicePropertyUnit");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.PropertyUnit)
                    .WithMany(p => p.ServicePropertyUnits)
                    .HasForeignKey(d => d.PropertyUnitId)
                    .HasConstraintName("FK_ServicePropertyUnit_PropertyUnit");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServicePropertyUnits)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK_ServicePropertyUnit_ServiceAvailable");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
