using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.DAL.Models;

public partial class DetectiveAgencyDbContext : DbContext
{
    public DetectiveAgencyDbContext()
    {
    }

    public DetectiveAgencyDbContext(DbContextOptions<DetectiveAgencyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<CaseSuspect> CaseSuspects { get; set; }

    public virtual DbSet<CaseType> CaseTypes { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Detective> Detectives { get; set; }

    public virtual DbSet<Evidence> Evidences { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Suspect> Suspects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DetectiveAgencyDb;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("case_status", new[] { "Відкрито", "Закрито", "Призупинено" })
            .HasPostgresEnum("detective_status", new[] { "Активний(а)", "У відпустці", "У відставці", "Звільнений(а)" });

        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("case_pkey");

            entity.ToTable("case");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaseTypeId).HasColumnName("case_type_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CloseDate).HasColumnName("close_date");
            entity.Property(e => e.DeadlineDate).HasColumnName("deadline_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DetectiveId).HasColumnName("detective_id");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("start_date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.CaseType).WithMany(p => p.Cases)
                .HasForeignKey(d => d.CaseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_case_case_type_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Cases)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_case_client_id");

            entity.HasOne(d => d.Detective).WithMany(p => p.Cases)
                .HasForeignKey(d => d.DetectiveId)
                .HasConstraintName("FK_case_detective_id");
        });

        modelBuilder.Entity<CaseSuspect>(entity =>
        {
            entity.HasKey(e => new { e.SuspectId, e.CaseId }).HasName("case_suspect_pkey");

            entity.ToTable("case_suspect");

            entity.Property(e => e.SuspectId).HasColumnName("suspect_id");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.Alibi).HasColumnName("alibi");
            entity.Property(e => e.IsInterrogated).HasColumnName("is_interrogated");

            entity.HasOne(d => d.Case).WithMany(p => p.CaseSuspects)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_case_suspect_case_id");

            entity.HasOne(d => d.Suspect).WithMany(p => p.CaseSuspects)
                .HasForeignKey(d => d.SuspectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_case_suspect_suspect_id");
        });

        modelBuilder.Entity<CaseType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("case_type_pkey");

            entity.ToTable("case_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApartmentNumber).HasColumnName("apartment_number");
            entity.Property(e => e.BuildingNumber)
                .HasMaxLength(30)
                .HasColumnName("building_number");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("city");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FatherName)
                .HasMaxLength(100)
                .HasColumnName("father_name");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Region)
                .HasMaxLength(30)
                .HasColumnName("region");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("registration_date");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Detective>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("detective_pkey");

            entity.ToTable("detective");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApartmentNumber).HasColumnName("apartment_number");
            entity.Property(e => e.BuildingNumber)
                .HasMaxLength(30)
                .HasColumnName("building_number");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("city");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FatherName)
                .HasMaxLength(100)
                .HasColumnName("father_name");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PersonalNotes).HasColumnName("personal_notes");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Region)
                .HasMaxLength(30)
                .HasColumnName("region");
            entity.Property(e => e.Salary)
                .HasPrecision(10, 2)
                .HasColumnName("salary");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Evidence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("evidence_pkey");

            entity.ToTable("evidence");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CollectionDate).HasColumnName("collection_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");

            entity.HasMany(d => d.Cases).WithMany(p => p.Evidences)
                .UsingEntity<Dictionary<string, object>>(
                    "CaseEvidence",
                    r => r.HasOne<Case>().WithMany()
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_case_evidence_case_id"),
                    l => l.HasOne<Evidence>().WithMany()
                        .HasForeignKey("EvidenceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_case_evidence_evidence_id"),
                    j =>
                    {
                        j.HasKey("EvidenceId", "CaseId").HasName("case_evidence_pkey");
                        j.ToTable("case_evidence");
                        j.IndexerProperty<int>("EvidenceId").HasColumnName("evidence_id");
                        j.IndexerProperty<int>("CaseId").HasColumnName("case_id");
                    });
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("expense_pkey");

            entity.ToTable("expense");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Annotation).HasColumnName("annotation");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_time");
            entity.Property(e => e.Purpose)
                .HasMaxLength(255)
                .HasColumnName("purpose");

            entity.HasOne(d => d.Case).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("expense_case_id_fk");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("report_pkey");

            entity.ToTable("report");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.Comments).HasColumnName("comments");
            entity.Property(e => e.ReportDate)
                .HasDefaultValueSql("CURRENT_DATE")
                .HasColumnName("report_date");
            entity.Property(e => e.Summary).HasColumnName("summary");

            entity.HasOne(d => d.Case).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_report_case_id");
        });

        modelBuilder.Entity<Suspect>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("suspect_pkey");

            entity.ToTable("suspect");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApartmentNumber).HasColumnName("apartment_number");
            entity.Property(e => e.BuildingNumber)
                .HasMaxLength(30)
                .HasColumnName("building_number");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("city");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.FatherName)
                .HasMaxLength(100)
                .HasColumnName("father_name");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .HasColumnName("nickname");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.PhysicalDescription).HasColumnName("physical_description");
            entity.Property(e => e.PriorConvictions).HasColumnName("prior_convictions");
            entity.Property(e => e.Region)
                .HasMaxLength(30)
                .HasColumnName("region");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
