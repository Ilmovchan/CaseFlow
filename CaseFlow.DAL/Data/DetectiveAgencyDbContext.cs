﻿using CaseFlow.DAL.Enums;
using CaseFlow.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.DAL.Data;

public partial class DetectiveAgencyDbContext : DbContext
{
    public DetectiveAgencyDbContext()
    {
    }

    public DetectiveAgencyDbContext(DbContextOptions<DetectiveAgencyDbContext> options)
        : base(options)
    {
    }

    #region ConnectionString

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DetectiveAgencyDb;Username=postgres;Password=12345");

    #endregion
    
    #region DbSet

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<CaseSuspect> CaseSuspects { get; set; }

    public virtual DbSet<CaseType> CaseTypes { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Detective> Detectives { get; set; }

    public virtual DbSet<Evidence> Evidences { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Suspect> Suspects { get; set; }

    #endregion
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum<DetectiveStatus>("detective_status")
            .HasPostgresEnum<CaseStatus>("case_status")
            .HasPostgresEnum<EvidenceType>("evidence_type")
            .HasPostgresEnum<ApprovalStatus>("expense_status");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("name_format", 
                    @"first_name ~ '^[А-ЯІЇЄа-яіїє]+$' AND last_name ~ '^[А-ЯІЇЄа-яіїє]+$' AND (father_name IS NULL OR father_name ~ '^[А-ЯІЇЄа-яіїє]+$')");

                t.HasCheckConstraint("phone_number_format", 
                    @"phone_number ~ '^\+380\d{9}$'");

                t.HasCheckConstraint("email_format", 
                    @"email ~ '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$'");

                t.HasCheckConstraint("date_of_birth_format", 
                    @"date_of_birth <= CURRENT_DATE");

                t.HasCheckConstraint("region_format", 
                    @"region ~ '^[А-ЯІЇЄа-яіїє]+$'");

                t.HasCheckConstraint("city_format", 
                    @"city ~ '^[А-ЯІЇЄа-яіїє\-]+$'");

                t.HasCheckConstraint("street_format", 
                    @"street ~ '^[А-ЯІЇЄа-яіїє\s\-]+$'");

                t.HasCheckConstraint("building_number_format", 
                    @"building_number ~ '^[0-9/]+$'");

                t.HasCheckConstraint("apartment_number_format", 
                    @"apartment_number IS NULL OR apartment_number > 0");
                
                t.HasCheckConstraint("registration_date_format", 
                    @"registration_date <= CURRENT_TIMESTAMP");
            });
        });
        
        modelBuilder.Entity<Case>(entity =>
        {
            entity.Property(e => e.Status)
                .HasDefaultValue(CaseStatus.Opened);
            
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("CURRENT_DATE");

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

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("deadline_format",
                    @"deadline_date >= start_date");
                
                t.HasCheckConstraint("close_date_format", 
                    @"close_date IS NULL OR close_date >= start_date");
            });
        });
        
        modelBuilder.Entity<CaseType>(entity =>
        {
            
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("name_format", 
                    @"name ~ '^[А-ЯІЇЄа-яіїє0-9\s\-.,:;/]+$'");
                
                t.HasCheckConstraint("price_format", 
                    @"price > 0");
            });
        });
        
        modelBuilder.Entity<Detective>(entity =>
        {
            entity.Property(e => e.Status)
                .HasDefaultValue(DetectiveStatus.Active);

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("name_format", 
                    @"first_name ~ '^[А-ЯІЇЄа-яіїє]+$' AND last_name ~ '^[А-ЯІЇЄа-яіїє]+$' AND (father_name IS NULL OR father_name ~ '^[А-ЯІЇЄа-яіїє]+$')");

                t.HasCheckConstraint("phone_number_format", 
                    @"phone_number ~ '^\+380\d{9}$'");

                t.HasCheckConstraint("email_format", 
                    @"email ~ '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$'");

                t.HasCheckConstraint("date_of_birth_format", 
                    @"date_of_birth <= CURRENT_DATE");

                t.HasCheckConstraint("region_format", 
                    @"region ~ '^[А-ЯІЇЄа-яіїє]+$'");

                t.HasCheckConstraint("city_format", 
                    @"city ~ '^[А-ЯІЇЄа-яіїє\-]+$'");

                t.HasCheckConstraint("street_format", 
                    @"street ~ '^[А-ЯІЇЄа-яіїє\s\-]+$'");

                t.HasCheckConstraint("building_number_format", 
                    @"building_number ~ '^[0-9/]+$'");

                t.HasCheckConstraint("apartment_number_format", 
                    @"apartment_number IS NULL OR apartment_number > 0");
                
                t.HasCheckConstraint("detective_hire_date_format", 
                    @"hire_date <= CURRENT_DATE");
                
                t.HasCheckConstraint("detective_salary_format", 
                    @"salary >= 0");
            });
        });
        
        modelBuilder.Entity<Evidence>(entity =>
        {
            entity.Property(e => e.Region)
                .HasDefaultValue("Не вказано");

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("type_format", 
                    @"type ~ '^[А-ЯІЇЄа-яіїє0-9 .,!?;:-]+$'");
        
                t.HasCheckConstraint("collection_date_format", 
                    @"collection_date <= CURRENT_TIMESTAMP");
            });
        });

        
        modelBuilder.Entity<CaseSuspect>(entity =>
        {
            entity.HasKey(e => new { e.SuspectId, e.CaseId });
            
            entity.HasOne(d => d.Case).WithMany(p => p.CaseSuspects)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_case_suspect_case_id");

            entity.HasOne(d => d.Suspect).WithMany(p => p.CaseSuspects)
                .HasForeignKey(d => d.SuspectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_case_suspect_suspect_id");
            
            entity.Property(e => e.ApprovalStatus)
                .HasDefaultValue(ApprovalStatus.Draft);
        });
        
        modelBuilder.Entity<CaseEvidence>(entity =>
        {
            entity.HasKey(e => new { e.EvidenceId, e.CaseId });
            
            entity.HasOne(d => d.Case).WithMany(p => p.CaseEvidences)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_case_evidence_case_id");
            
            entity.HasOne(d => d.Evidence).WithMany(p => p.CaseEvidences)
                .HasForeignKey(d => d.EvidenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_case_evidence_suspect_id");
            
            entity.Property(e => e.ApprovalStatus)
                .HasDefaultValue(ApprovalStatus.Draft);
        });

        
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasOne(d => d.Case).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("expense_case_id_fk");

            entity.ToTable(t =>
            {
                t.HasCheckConstraint("date_time_format", 
                    @"date_time <= CURRENT_TIMESTAMP");
                
                t.HasCheckConstraint("amount_format", 
                    @"amount > 0");
                
                t.HasCheckConstraint("purpose_format", 
                    @"purpose ~ '^[А-ЯІЇЄа-яіїєA-Za-z0-9\s\-\.,:;’]+$'");
            });
            
            entity.Property(e => e.ApprovalStatus)
                .HasDefaultValue(ApprovalStatus.Draft);
        });
        
        modelBuilder.Entity<Report>(entity =>
        {
            entity.Property(e => e.ReportDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Case).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_report_case_id");
            
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("date_format", 
                    @"report_date <= CURRENT_TIMESTAMP");
            });
            
            entity.Property(e => e.ApprovalStatus)
                .HasDefaultValue(ApprovalStatus.Draft);
        });

        modelBuilder.Entity<Evidence>(entity =>
        {
            entity.Property(e => e.Region)
                .HasDefaultValue("Не вказано");
            
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("type_format", 
                    @"type ~ '^[А-ЯІЇЄа-яіїє0-9 .,!?;:-]+$'");
        
                t.HasCheckConstraint("collection_date_format", 
                    @"collection_date <= CURRENT_TIMESTAMP");
            });
        });
        
        modelBuilder.Entity<Suspect>(entity =>
        {
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("name_format", 
                    @"first_name ~ '^[А-ЯІЇЄа-яіїє]+$' AND last_name ~ '^[А-ЯІЇЄа-яіїє]+$' AND (father_name IS NULL OR father_name ~ '^[А-ЯІЇЄа-яіїє]+$')");

                t.HasCheckConstraint("phone_number_format", 
                    @"phone_number ~ '^\+380\d{9}$'");

                t.HasCheckConstraint("date_of_birth_format", 
                    @"date_of_birth <= CURRENT_DATE");

                t.HasCheckConstraint("region_format", 
                    @"region ~ '^[А-ЯІЇЄа-яіїє]+$'");

                t.HasCheckConstraint("city_format", 
                    @"city ~ '^[А-ЯІЇЄа-яіїє\-]+$'");

                t.HasCheckConstraint("street_format", 
                    @"street ~ '^[А-ЯІЇЄа-яіїє\s\-]+$'");

                t.HasCheckConstraint("building_number_format", 
                    @"building_number ~ '^[0-9/]+$'");

                t.HasCheckConstraint("apartment_number_format", 
                    @"apartment_number IS NULL OR apartment_number > 0");
                
                t.HasCheckConstraint("weight_height_format", 
                    @"weight IS NOT NULL AND weight > 0 AND height IS NOT NULL AND height > 0 OR weight IS NULL AND height IS NULL");
            });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

