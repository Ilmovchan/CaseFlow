using System;
using System.Collections.Generic;
using CaseFlow.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseFlow.DAL.Data;

public partial class CaseFlowDbContext : DbContext
{
    public CaseFlowDbContext()
    {
    }

    public CaseFlowDbContext(DbContextOptions<CaseFlowDbContext> options)
        : base(options)
    {
        
    }

    #region DbSets
    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<AdminProfile> AdminProfiles { get; set; }
    public virtual DbSet<ApprovalStatus> ApprovalStatuses { get; set; }
    public virtual DbSet<Case> Cases { get; set; }
    public virtual DbSet<CasePriority> CasePriorities { get; set; }
    public virtual DbSet<CaseStatus> CaseStatuses { get; set; }
    public virtual DbSet<CaseSuspect> CaseSuspects { get; set; }
    public virtual DbSet<CaseType> CaseTypes { get; set; }
    public virtual DbSet<CaseWitness> CaseWitnesses { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<DetectiveProfile> DetectiveProfiles { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Evidence> Evidences { get; set; }
    public virtual DbSet<Expense> Expenses { get; set; }
    public virtual DbSet<LogStatus> LogStatuses { get; set; }
    public virtual DbSet<LogType> LogTypes { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<Report> Reports { get; set; }
    public virtual DbSet<ReportEntityType> ReportEntityTypes { get; set; }
    public virtual DbSet<ReportEvent> ReportEvents { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<SessionLog> SessionLogs { get; set; }
    public virtual DbSet<Suspect> Suspects { get; set; }
    public virtual DbSet<SuspectStatus> SuspectStatuses { get; set; }
    public virtual DbSet<Testimony> Testimonies { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Witness> Witnesses { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("activity_log_pkey");

            entity.ToTable("activity_log");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.Details)
                .HasColumnType("jsonb")
                .HasColumnName("details");
            entity.Property(e => e.LogStatusId).HasColumnName("log_status_id");
            entity.Property(e => e.LogTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("log_time");
            entity.Property(e => e.LogTypeId).HasColumnName("log_type_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.LogStatus).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.LogStatusId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_activity_log_status");

            entity.HasOne(d => d.LogType).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.LogTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_activity_log_type");

            entity.HasOne(d => d.Session).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("activity_log_session_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("activity_log_user_id_fkey");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("address_pkey");

            entity.ToTable("address");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.ApartmentNumber).HasColumnName("apartment_number");
            entity.Property(e => e.BuildingNumber).HasColumnName("building_number");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasColumnName("postal_code");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .HasColumnName("street");
        });

        modelBuilder.Entity<AdminProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("admin_profile_pkey");

            entity.ToTable("admin_profile");

            entity.HasIndex(e => e.UserId, "admin_profile_user_id_key").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.PersonalInfo).HasColumnName("personal_info");

            entity.HasOne(d => d.User).WithOne(p => p.AdminProfile)
                .HasForeignKey<AdminProfile>(d => d.UserId)
                .HasConstraintName("admin_profile_user_id_fkey");
        });

        modelBuilder.Entity<ApprovalStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("approval_status_pkey");

            entity.ToTable("approval_status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Code)
                .HasDefaultValueSql("20")
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasDefaultValueSql("20")
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Case>(entity =>
        {
            entity.HasKey(e => e.CaseId).HasName("case_pkey");

            entity.ToTable("case");

            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.ApprovalStatusDescription).HasColumnName("approval_status_description");
            entity.Property(e => e.ApprovalStatusId)
                .HasDefaultValue(1)
                .HasColumnName("approval_status_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_date");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(50)
                .HasColumnName("reference_number");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Subtitle)
                .HasMaxLength(100)
                .HasColumnName("subtitle");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Cases)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("case_address_id_fkey");

            entity.HasOne(d => d.ApprovalStatus).WithMany(p => p.Cases)
                .HasForeignKey(d => d.ApprovalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_case_approval_status");

            entity.HasOne(d => d.Client).WithMany(p => p.Cases)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("case_client_id_fkey");

            entity.HasOne(d => d.Priority).WithMany(p => p.Cases)
                .HasForeignKey(d => d.PriorityId)
                .HasConstraintName("case_priority_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Cases)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("case_status_id_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Cases)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("case_type_id_fkey");
        });

        modelBuilder.Entity<CasePriority>(entity =>
        {
            entity.HasKey(e => e.PriorityId).HasName("case_priority_pkey");

            entity.ToTable("case_priority");

            entity.Property(e => e.PriorityId).HasColumnName("priority_id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CaseStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("case_status_pkey");

            entity.ToTable("case_status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CaseSuspect>(entity =>
        {
            entity.HasKey(e => new { e.CaseId, e.SuspectId }).HasName("case_suspect_pkey");

            entity.ToTable("case_suspect");

            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.SuspectId).HasColumnName("suspect_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Case).WithMany(p => p.CaseSuspects)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("case_suspect_case_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.CaseSuspects)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("case_suspect_status_id_fkey");

            entity.HasOne(d => d.Suspect).WithMany(p => p.CaseSuspects)
                .HasForeignKey(d => d.SuspectId)
                .HasConstraintName("case_suspect_suspect_id_fkey");
        });

        modelBuilder.Entity<CaseType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("case_type_pkey");

            entity.ToTable("case_type");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CaseWitness>(entity =>
        {
            entity.HasKey(e => e.CaseWitnessId).HasName("case_witness_pkey");

            entity.ToTable("case_witness");

            entity.HasIndex(e => e.CaseId, "case_witness_case_id").IsUnique();

            entity.HasIndex(e => e.WitnessId, "case_witness_witness_id").IsUnique();

            entity.Property(e => e.CaseWitnessId).HasColumnName("case_witness_id");
            entity.Property(e => e.ApprovalStatusDescription).HasColumnName("approval_status_description");
            entity.Property(e => e.ApprovalStatusId)
                .HasDefaultValue(1)
                .HasColumnName("approval_status_id");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.WitnessId).HasColumnName("witness_id");

            entity.HasOne(d => d.ApprovalStatus).WithMany(p => p.CaseWitnesses)
                .HasForeignKey(d => d.ApprovalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_case_witness_approval_status");

            entity.HasOne(d => d.Case).WithOne(p => p.CaseWitness)
                .HasForeignKey<CaseWitness>(d => d.CaseId)
                .HasConstraintName("case_witness_case_id_fkey");

            entity.HasOne(d => d.Witness).WithOne(p => p.CaseWitness)
                .HasForeignKey<CaseWitness>(d => d.WitnessId)
                .HasConstraintName("case_witness_witness_id_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FatherName)
                .HasMaxLength(70)
                .HasColumnName("father_name");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(70)
                .HasColumnName("last_name");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registration_date");

            entity.HasOne(d => d.Address).WithMany(p => p.Clients)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("client_address_id_fkey");
        });

        modelBuilder.Entity<DetectiveProfile>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("detective_profile_pkey");

            entity.ToTable("detective_profile");

            entity.HasIndex(e => e.UserId, "detective_profile_user_id_key").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.PersonalInfo).HasColumnName("personal_info");
            entity.Property(e => e.Rank)
                .HasColumnType("character varying")
                .HasColumnName("rank");

            entity.HasOne(d => d.User).WithOne(p => p.DetectiveProfile)
                .HasForeignKey<DetectiveProfile>(d => d.UserId)
                .HasConstraintName("detective_profile_user_id_fkey");

            entity.HasMany(d => d.Cases).WithMany(p => p.Detectives)
                .UsingEntity<Dictionary<string, object>>(
                    "CaseDetective",
                    r => r.HasOne<Case>().WithMany()
                        .HasForeignKey("CaseId")
                        .HasConstraintName("case_detective_case_id_fkey"),
                    l => l.HasOne<DetectiveProfile>().WithMany()
                        .HasForeignKey("DetectiveId")
                        .HasConstraintName("case_detective_detective_id_fkey"),
                    j =>
                    {
                        j.HasKey("DetectiveId", "CaseId").HasName("case_detective_pkey");
                        j.ToTable("case_detective");
                        j.IndexerProperty<int>("DetectiveId").HasColumnName("detective_id");
                        j.IndexerProperty<int>("CaseId").HasColumnName("case_id");
                    });
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FatherName)
                .HasMaxLength(70)
                .HasColumnName("father_name");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("hire_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(70)
                .HasColumnName("last_name");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.TerminationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("termination_date");

            entity.HasOne(d => d.Address).WithMany(p => p.Employees)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("employee_address_id_fkey");
        });

        modelBuilder.Entity<Evidence>(entity =>
        {
            entity.HasKey(e => e.EvidenceId).HasName("evidence_pkey");

            entity.ToTable("evidence");

            entity.Property(e => e.EvidenceId).HasColumnName("evidence_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.ApprovalStatusDescription).HasColumnName("approval_status_description");
            entity.Property(e => e.ApprovalStatusId)
                .HasDefaultValue(1)
                .HasColumnName("approval_status_id");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.CollectedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("collected_at");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("file_path");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Purpose).HasColumnName("purpose");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Address).WithMany(p => p.Evidences)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("evidence_address_id_fkey");

            entity.HasOne(d => d.ApprovalStatus).WithMany(p => p.Evidences)
                .HasForeignKey(d => d.ApprovalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_evidence_approval_status");

            entity.HasOne(d => d.Case).WithMany(p => p.Evidences)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("evidence_case_id_fkey");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId).HasName("expense_pkey");

            entity.ToTable("expense");

            entity.Property(e => e.ExpenseId).HasColumnName("expense_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.ApprovalStatusDescription).HasColumnName("approval_status_description");
            entity.Property(e => e.ApprovalStatusId)
                .HasDefaultValue(1)
                .HasColumnName("approval_status_id");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");

            entity.HasOne(d => d.ApprovalStatus).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.ApprovalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_expense_approval_status");

            entity.HasOne(d => d.Case).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("expense_case_id_fkey");
        });

        modelBuilder.Entity<LogStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("log_status_pkey");

            entity.ToTable("log_status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<LogType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("log_type_pkey");

            entity.ToTable("log_type");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("permission_pkey");

            entity.ToTable("permission");

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasMany(d => d.Roles).WithMany(p => p.Permissions)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("role_permission_role_id_fkey"),
                    l => l.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("role_permission_permission_id_fkey"),
                    j =>
                    {
                        j.HasKey("PermissionId", "RoleId").HasName("role_permission_pkey");
                        j.ToTable("role_permission");
                        j.IndexerProperty<int>("PermissionId").HasColumnName("permission_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("report_pkey");

            entity.ToTable("report");

            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.AdditionalInfo).HasColumnName("additional_info");
            entity.Property(e => e.ApprovalStatusDescription).HasColumnName("approval_status_description");
            entity.Property(e => e.ApprovalStatusId)
                .HasDefaultValue(1)
                .HasColumnName("approval_status_id");
            entity.Property(e => e.CaseId).HasColumnName("case_id");
            entity.Property(e => e.Conclusions).HasColumnName("conclusions");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Summary).HasColumnName("summary");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.ApprovalStatus).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ApprovalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_report_approval_status");

            entity.HasOne(d => d.Case).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CaseId)
                .HasConstraintName("report_case_id_fkey");
        });

        modelBuilder.Entity<ReportEntityType>(entity =>
        {
            entity.HasKey(e => e.EntityTypeId).HasName("report_entity_type_pkey");

            entity.ToTable("report_entity_type");

            entity.Property(e => e.EntityTypeId).HasColumnName("entity_type_id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ReportEvent>(entity =>
        {
            entity.HasKey(e => e.ReportEventId).HasName("report_event_pkey");

            entity.ToTable("report_event");

            entity.Property(e => e.ReportEventId).HasColumnName("report_event_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityTypeId).HasColumnName("entity_type_id");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.ReportId).HasColumnName("report_id");

            entity.HasOne(d => d.EntityType).WithMany(p => p.ReportEvents)
                .HasForeignKey(d => d.EntityTypeId)
                .HasConstraintName("report_event_entity_type_id_fkey");

            entity.HasOne(d => d.Report).WithMany(p => p.ReportEvents)
                .HasForeignKey(d => d.ReportId)
                .HasConstraintName("report_event_report_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SessionLog>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("session_log_pkey");

            entity.ToTable("session_log");

            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.LoginStatusId).HasColumnName("login_status_id");
            entity.Property(e => e.LoginTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("login_time");
            entity.Property(e => e.LogoutTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("logout_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.LoginStatus).WithMany(p => p.SessionLogs)
                .HasForeignKey(d => d.LoginStatusId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_session_log_status");

            entity.HasOne(d => d.User).WithMany(p => p.SessionLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("session_log_user_id_fkey");
        });

        modelBuilder.Entity<Suspect>(entity =>
        {
            entity.HasKey(e => e.SuspectId).HasName("suspect_pkey");

            entity.ToTable("suspect");

            entity.Property(e => e.SuspectId).HasColumnName("suspect_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Alibi).HasColumnName("alibi");
            entity.Property(e => e.ApprovalStatusDescription).HasColumnName("approval_status_description");
            entity.Property(e => e.ApprovalStatusId)
                .HasDefaultValue(1)
                .HasColumnName("approval_status_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FatherName)
                .HasMaxLength(70)
                .HasColumnName("father_name");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.IsPriorConvicted).HasColumnName("is_prior_convicted");
            entity.Property(e => e.LastName)
                .HasMaxLength(70)
                .HasColumnName("last_name");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.Nickname)
                .HasMaxLength(70)
                .HasColumnName("nickname");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.PhysicalDescription).HasColumnName("physical_description");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Address).WithMany(p => p.Suspects)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("suspect_address_id_fkey");

            entity.HasOne(d => d.ApprovalStatus).WithMany(p => p.Suspects)
                .HasForeignKey(d => d.ApprovalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_suspect_approval_status");
        });

        modelBuilder.Entity<SuspectStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("suspect_status_pkey");

            entity.ToTable("suspect_status");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasColumnName("code");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Testimony>(entity =>
        {
            entity.HasKey(e => e.TestimonyId).HasName("testimony_pkey");

            entity.ToTable("testimony");

            entity.Property(e => e.TestimonyId).HasColumnName("testimony_id");
            entity.Property(e => e.CaseWitnessId).HasColumnName("case_witness_id");
            entity.Property(e => e.CollectedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("collected_at");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");

            entity.HasOne(d => d.CaseWitness).WithMany(p => p.Testimonies)
                .HasForeignKey(d => d.CaseWitnessId)
                .HasConstraintName("testimony_case_witness_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.EmployeeId, "user_employee_id_key").IsUnique();

            entity.HasIndex(e => e.Username, "user_username").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Employee).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.EmployeeId)
                .HasConstraintName("user_employee_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_role_id_fkey");
        });

        modelBuilder.Entity<Witness>(entity =>
        {
            entity.HasKey(e => e.WitnessId).HasName("witness_pkey");

            entity.ToTable("witness");

            entity.Property(e => e.WitnessId).HasColumnName("witness_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FatherName)
                .HasMaxLength(70)
                .HasColumnName("father_name");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(70)
                .HasColumnName("last_name");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("registration_date");

            entity.HasOne(d => d.Address).WithMany(p => p.Witnesses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("witness_address_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
