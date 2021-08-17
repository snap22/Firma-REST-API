using Microsoft.EntityFrameworkCore;


#nullable disable

namespace FirmaRest.Models
{
    public partial class RestDBContext : DbContext
    {

        public RestDBContext()
        {
        }

        public RestDBContext(DbContextOptions<RestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Slovak_CI_AS");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.HasIndex(e => e.Code, "UQ__Company__A25C5AA75FE56C1C")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.DirectorNavigation)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.Director)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_Director");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.HasIndex(e => e.Code, "UQ__Departme__A25C5AA73782CA47")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.LeaderNavigation)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.Leader)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_Leader");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_ProjectId");
            });

            modelBuilder.Entity<Division>(entity =>
            {
                entity.ToTable("Division");

                entity.HasIndex(e => e.Code, "UQ__Division__A25C5AA72D894976")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Divisions)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Division_CompanyId");

                entity.HasOne(d => d.LeaderNavigation)
                    .WithMany(p => p.Divisions)
                    .HasForeignKey(d => d.Leader)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Division_Leader");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534745F8E85")
                    .IsUnique();

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Employee_CompanyId");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.HasIndex(e => e.Code, "UQ__Project__A25C5AA714CED005")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Division)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.DivisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_DivisionId");

                entity.HasOne(d => d.LeaderNavigation)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.Leader)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Leader");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<FirmaRest.Models.DTO.DepartmentDto> DepartmentDto { get; set; }

        public DbSet<FirmaRest.Models.DTO.ProjectDto> ProjectDto { get; set; }
    }
}
