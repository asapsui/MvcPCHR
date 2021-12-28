using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MvcPCHR.Models
{
    // since IdentityDbContext inherits from DbContext, we can use one DbContext that contains classes,
    // identities, and roles.
    public partial class PCHRDBContext : IdentityDbContext<IdentityUser>
    {
        public PCHRDBContext()
        {
        }

        public PCHRDBContext(DbContextOptions<PCHRDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AllergyTbl> AllergyTbls { get; set; } = null!;
        public virtual DbSet<Condition> Conditions { get; set; } = null!;
        public virtual DbSet<ImmunizationTbl> ImmunizationTbls { get; set; } = null!;
        public virtual DbSet<MedProcTbl> MedProcTbls { get; set; } = null!;
        public virtual DbSet<MedicationTbl> MedicationTbls { get; set; } = null!;
        public virtual DbSet<PatientTbl> PatientTbls { get; set; } = null!;
        public virtual DbSet<PerDetailsTbl> PerDetailsTbls { get; set; } = null!;
        public virtual DbSet<PrimaryCareTbl> PrimaryCareTbls { get; set; } = null!;
        public virtual DbSet<TestTbl> TestTbls { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        //keys of identity tables are being mapped in OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllergyTbl>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.AllergyId });

                entity.ToTable("ALLERGY_TBL");

                entity.Property(e => e.PatientId)
                    .HasMaxLength(10)
                    .HasColumnName("PATIENT_ID")
                    .IsFixedLength();

                entity.Property(e => e.AllergyId)
                    .HasMaxLength(10)
                    .HasColumnName("ALLERGY_ID")
                    .IsFixedLength();

                entity.Property(e => e.Allergen)
                    .HasMaxLength(50)
                    .HasColumnName("ALLERGEN");

                entity.Property(e => e.Note)
                    .IsUnicode(false)
                    .HasColumnName("NOTE");

                entity.Property(e => e.OnsetDate)
                    .HasColumnType("date")
                    .HasColumnName("ONSET_DATE");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.AllergyTbls)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ALLERGY_TBL_PATIENT_TBL");
            });

            modelBuilder.Entity<Condition>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.ConditionId });

                entity.ToTable("CONDITION");

                entity.Property(e => e.PatientId)
                    .HasMaxLength(10)
                    .HasColumnName("PATIENT_ID")
                    .IsFixedLength();

                entity.Property(e => e.ConditionId)
                    .HasMaxLength(10)
                    .HasColumnName("CONDITION_ID")
                    .IsFixedLength();

                entity.Property(e => e.Acute).HasColumnName("ACUTE");

                entity.Property(e => e.Chronic).HasColumnName("CHRONIC");

                entity.Property(e => e.Condition1)
                    .HasMaxLength(50)
                    .HasColumnName("CONDITION");

                entity.Property(e => e.Note).HasColumnName("NOTE");

                entity.Property(e => e.OnsetDate)
                    .HasColumnType("date")
                    .HasColumnName("ONSET_DATE");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Conditions)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CONDITION_PATIENT_TBL");
            });

            modelBuilder.Entity<ImmunizationTbl>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.ImmunizationId });

                entity.ToTable("IMMUNIZATION_TBL");

                entity.Property(e => e.PatientId)
                    .HasMaxLength(10)
                    .HasColumnName("PATIENT_ID")
                    .IsFixedLength();

                entity.Property(e => e.ImmunizationId)
                    .HasMaxLength(10)
                    .HasColumnName("IMMUNIZATION_ID")
                    .IsFixedLength();

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.Immunization)
                    .HasMaxLength(50)
                    .HasColumnName("IMMUNIZATION");

                entity.Property(e => e.Note).HasColumnName("NOTE");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.ImmunizationTbls)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IMMUNIZATION_TBL_PATIENT_TBL");
            });

            modelBuilder.Entity<MedProcTbl>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.ProcedureId });

                entity.ToTable("MED_PROC_TBL");

                entity.Property(e => e.PatientId)
                    .HasMaxLength(10)
                    .HasColumnName("PATIENT_ID")
                    .IsFixedLength();

                entity.Property(e => e.ProcedureId)
                    .HasMaxLength(10)
                    .HasColumnName("PROCEDURE_ID")
                    .IsFixedLength();

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.Doctor)
                    .HasMaxLength(50)
                    .HasColumnName("DOCTOR");

                entity.Property(e => e.MedProcedure)
                    .HasMaxLength(50)
                    .HasColumnName("MED_PROCEDURE");

                entity.Property(e => e.Note).HasColumnName("NOTE");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.MedProcTbls)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MED_PROC_TBL_PATIENT_TBL");
            });

            modelBuilder.Entity<MedicationTbl>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.MedId });

                entity.ToTable("MEDICATION_TBL");

                entity.Property(e => e.PatientId)
                    .HasMaxLength(10)
                    .HasColumnName("PATIENT_ID")
                    .IsFixedLength();

                entity.Property(e => e.MedId)
                    .HasMaxLength(10)
                    .HasColumnName("MED_ID")
                    .IsFixedLength();

                entity.Property(e => e.Chronic).HasColumnName("CHRONIC");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.Medication)
                    .HasMaxLength(50)
                    .HasColumnName("MEDICATION");

                entity.Property(e => e.Note).HasColumnName("NOTE");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.MedicationTbls)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MEDICATION_TBL_PATIENT_TBL");
            });

            modelBuilder.Entity<PatientTbl>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.ToTable("PATIENT_TBL");

                entity.Property(e => e.PatientId)
                    .HasMaxLength(10)
                    .HasColumnName("PATIENT_ID")
                    .IsFixedLength();

                entity.Property(e => e.AddressCity)
                    .HasMaxLength(20)
                    .HasColumnName("ADDRESS_CITY");

                entity.Property(e => e.AddressState)
                    .HasMaxLength(20)
                    .HasColumnName("ADDRESS_STATE");

                entity.Property(e => e.AddressStreet)
                    .HasMaxLength(50)
                    .HasColumnName("ADDRESS_STREET");

                entity.Property(e => e.AddressZip)
                    .HasMaxLength(10)
                    .HasColumnName("ADDRESS_ZIP")
                    .IsFixedLength();

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("DATE_Of_BIRTH");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("FIRST_NAME");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .HasColumnName("LAST_NAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.PhoneHome)
                    .HasMaxLength(10)
                    .HasColumnName("PHONE_HOME")
                    .IsFixedLength();

                entity.Property(e => e.PhoneMobile)
                    .HasMaxLength(10)
                    .HasColumnName("PHONE_MOBILE")
                    .IsFixedLength();

                entity.Property(e => e.PrimaryId)
                    .HasMaxLength(10)
                    .HasColumnName("PRIMARY_ID")
                    .IsFixedLength();

                entity.Property(e => e.Username)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");
            });

            modelBuilder.Entity<PerDetailsTbl>(entity =>
            {
                entity.HasKey(e => e.PatientId);

                entity.ToTable("PER_DETAILS_TBL");

                entity.Property(e => e.PatientId)
                    .HasMaxLength(10)
                    .HasColumnName("PATIENT_ID")
                    .IsFixedLength();

                entity.Property(e => e.BloodType)
                    .HasMaxLength(10)
                    .HasColumnName("BLOOD_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.HeightInches).HasColumnName("HEIGHT_INCHES");

                entity.Property(e => e.HivStatus).HasColumnName("HIV_STATUS");

                entity.Property(e => e.OrganDonor).HasColumnName("ORGAN_DONOR");

                entity.Property(e => e.WeightLbs).HasColumnName("WEIGHT_LBS");

                entity.HasOne(d => d.Patient)
                    .WithOne(p => p.PerDetailsTbl)
                    .HasForeignKey<PerDetailsTbl>(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PER_DETAILS_TBL_PATIENT_TBL");
            });

            modelBuilder.Entity<PrimaryCareTbl>(entity =>
            {
                entity.HasKey(e => e.PrimaryId);

                entity.ToTable("PRIMARY_CARE_TBL");

                entity.Property(e => e.PrimaryId)
                    .HasMaxLength(10)
                    .HasColumnName("PRIMARY_ID")
                    .IsFixedLength();

                entity.Property(e => e.NameFisrt)
                    .HasMaxLength(10)
                    .HasColumnName("NAME_FISRT")
                    .IsFixedLength();

                entity.Property(e => e.NameLast)
                    .HasMaxLength(10)
                    .HasColumnName("NAME_LAST")
                    .IsFixedLength();

                entity.Property(e => e.PhoneMobile)
                    .HasMaxLength(10)
                    .HasColumnName("PHONE_MOBILE")
                    .IsFixedLength();

                entity.Property(e => e.PhoneOffice)
                    .HasMaxLength(10)
                    .HasColumnName("PHONE_OFFICE")
                    .IsFixedLength();

                entity.Property(e => e.Specialty)
                    .HasMaxLength(10)
                    .HasColumnName("SPECIALTY")
                    .IsFixedLength();

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .HasColumnName("TITLE")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TestTbl>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.TestId });

                entity.ToTable("TEST_TBL");

                entity.Property(e => e.PatientId)
                    .HasMaxLength(10)
                    .HasColumnName("PATIENT_ID")
                    .IsFixedLength();

                entity.Property(e => e.TestId)
                    .HasMaxLength(10)
                    .HasColumnName("TEST_ID")
                    .IsFixedLength();

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.Note).HasColumnName("NOTE");

                entity.Property(e => e.Result)
                    .HasMaxLength(50)
                    .HasColumnName("RESULT");

                entity.Property(e => e.Test)
                    .HasMaxLength(50)
                    .HasColumnName("TEST");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.TestTbls)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TEST_TBL_PATIENT_TBL");
            });
            OnModelCreatingPartial(modelBuilder);

            base.OnModelCreating(modelBuilder); // had to call this method
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
