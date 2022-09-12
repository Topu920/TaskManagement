using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Entities.Models.HrmsModels
{
    public partial class CoreERPContext : DbContext
    {
        public CoreERPContext()
        {
        }

        public CoreERPContext(DbContextOptions<CoreERPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommonDepartment> CommonDepartments { get; set; } = null!;
        public virtual DbSet<CommonDesignation> CommonDesignations { get; set; } = null!;
        public virtual DbSet<HumanResourceEmployeeBasic> HumanResourceEmployeeBasics { get; set; } = null!;
        public virtual DbSet<HumanResourceEmployeeContact> HumanResourceEmployeeContacts { get; set; } = null!;
        public virtual DbSet<OgranogramApplicationsApproval> OgranogramApplicationsApprovals { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:hrmsConnectionsString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommonDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                    .HasName("PK_Common.Department");

                entity.ToTable("Common_Department");

                entity.HasIndex(e => e.DepartmentName, "IX_Common_Department")
                    .IsUnique();

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentNameBan).HasMaxLength(150);

                entity.Property(e => e.DepartmentShortName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TerminalId)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CommonDesignation>(entity =>
            {
                entity.HasKey(e => e.DesignationId)
                    .HasName("PK_Common.Designation");

                entity.ToTable("Common_Designation");

                entity.HasIndex(e => e.DesignationName, "IX_Common.Designation")
                    .IsUnique();

                entity.Property(e => e.DesignationId).HasColumnName("DesignationID");

                entity.Property(e => e.DesGroupId).HasColumnName("DesGroupID");

                entity.Property(e => e.DesignationName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationNameBan).HasMaxLength(150);

                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GadgetTypeId).HasComment("1=Gadget , 2=Non Gadget");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.TerminalId)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HumanResourceEmployeeBasic>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK_HumanResource.EmployeeBasic_1");

                entity.ToTable("HumanResource_EmployeeBasic");

                entity.HasIndex(e => e.EmpCode, "_dta_index_HumanResource.EmployeeBasic_5_1394820031__K2_1")
                    .IsUnique();

                entity.HasIndex(e => new { e.EmpCode, e.EmpId }, "_dta_index_HumanResource_EmployeeBasic_6_592825274__K2_K1");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.Bgmeaid)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("BGMEAID");

                entity.Property(e => e.BirthCertificateNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BloodGroupId).HasColumnName("BloodGroupID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DesignationId).HasColumnName("DesignationID");

                entity.Property(e => e.EmpCategoryId).HasColumnName("EmpCategoryID");

                entity.Property(e => e.EmpCode)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.EmpStatusId)
                    .HasColumnName("EmpStatusID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.EmpTypeId).HasColumnName("EmpTypeID");

                entity.Property(e => e.FathersName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FinalSubmitDate).HasColumnType("date");

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("InsertUserID");

                entity.Property(e => e.IsApproved).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsCompOtfixed).HasColumnName("IsCompOTFixed");

                entity.Property(e => e.JoiningDate).HasColumnType("date");

                entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

                entity.Property(e => e.MothersName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NameBan).HasMaxLength(50);

                entity.Property(e => e.NationalityId).HasColumnName("NationalityID");

                entity.Property(e => e.Nidno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NIDNo");

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.PrevEmpId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PrevEmpID");

                entity.Property(e => e.PrevPunchNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProbationDate).HasColumnType("date");

                entity.Property(e => e.ReligionId).HasColumnName("ReligionID");

                entity.Property(e => e.RollBackDate).HasColumnType("datetime");

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.Property(e => e.SpouseName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeamId).HasColumnName("TeamID");

                entity.Property(e => e.TerminalId)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("TerminalID");

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TitleBan).HasMaxLength(50);

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UpdateUserID");

                entity.Property(e => e.WingId).HasColumnName("WingID");
            });

            modelBuilder.Entity<HumanResourceEmployeeContact>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK_HumanResource.EmployeeContact");

                entity.ToTable("HumanResource_EmployeeContact");

                entity.Property(e => e.EmpId)
                    .ValueGeneratedNever()
                    .HasColumnName("EmpID");

                entity.Property(e => e.BusStop).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EmailOffice).HasMaxLength(50);

                entity.Property(e => e.EmergContact)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmergContactAddress)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EmergContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pabx)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PABX");

                entity.Property(e => e.PerDistrictId).HasColumnName("PerDistrictID");

                entity.Property(e => e.PerDivisionId).HasColumnName("PerDivisionID");

                entity.Property(e => e.PerPostCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PerPostOffice)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PerPostOfficeBan).HasMaxLength(50);

                entity.Property(e => e.PerRoad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PerRoadBan).HasMaxLength(50);

                entity.Property(e => e.PerThanaId).HasColumnName("PerThanaID");

                entity.Property(e => e.PerVillage).IsUnicode(false);

                entity.Property(e => e.PerVillageBan).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PreDistrictId).HasColumnName("PreDistrictID");

                entity.Property(e => e.PreDivisionId).HasColumnName("PreDivisionID");

                entity.Property(e => e.PrePostCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrePostOffice)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrePostOfficeBan).HasMaxLength(50);

                entity.Property(e => e.PreRoad)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PreRoadBan).HasMaxLength(50);

                entity.Property(e => e.PreThanaId).HasColumnName("PreThanaID");

                entity.Property(e => e.PreVillage)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PreVillageBan)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RelationWith)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SocialMediaId)
                    .HasMaxLength(50)
                    .HasColumnName("SocialMediaID");
            });

            modelBuilder.Entity<OgranogramApplicationsApproval>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Ogranogram_ApplicationsApproval");

                entity.Property(e => e.EffectDate).HasColumnType("date");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InActiveDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
