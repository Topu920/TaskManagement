using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Entities.Models
{
    public partial class TaskManagementContext : DbContext
    {
        public TaskManagementContext()
        {
        }

        public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CmnStatus> CmnStatuses { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<EmailTracker> EmailTrackers { get; set; } = null!;
        public virtual DbSet<FileUpload> FileUploads { get; set; } = null!;
        public virtual DbSet<GroupMember> GroupMembers { get; set; } = null!;
        public virtual DbSet<GroupMemberDetail> GroupMemberDetails { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<MemberRole> MemberRoles { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<ProjectMember> ProjectMembers { get; set; } = null!;
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; } = null!;
        public virtual DbSet<TaskAssignment> TaskAssignments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CmnStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("CmnStatus");

                entity.Property(e => e.StatusId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ModuleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CommentDescription).IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<EmailTracker>(entity =>
            {
                entity.HasKey(e => e.EmailId);

                entity.ToTable("EmailTracker");

                entity.Property(e => e.RecieverEmailAddress).HasMaxLength(100);

                entity.Property(e => e.SenderEmailAddress).HasMaxLength(100);

                entity.Property(e => e.SendingDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<FileUpload>(entity =>
            {
                entity.HasKey(e => e.FileId);

                entity.Property(e => e.FileId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FileExtension)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FileUniqueName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.FileUploads)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FileUploads_Comments");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK_MemberGroup");

                entity.ToTable("GroupMember");

                entity.Property(e => e.GroupId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsPrivate)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GroupMemberDetail>(entity =>
            {
                entity.HasKey(e => e.GroupMemberDetailsId);

                entity.Property(e => e.GroupMemberDetailsId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMemberDetails)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GroupMemberDetails_GroupMember");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.Property(e => e.HistoryId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.HistoryDescription)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MemberRole>(entity =>
            {
                entity.Property(e => e.MemberRoleId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.MemberRoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.ProjectId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.FinishingDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectDescription).IsUnicode(false);

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartingDate).HasColumnType("datetime");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Projects_CmnStatus");
            });

            modelBuilder.Entity<ProjectMember>(entity =>
            {
                entity.Property(e => e.ProjectMemberId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectId).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK_Tasks");

                entity.Property(e => e.TaskId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Eddate)
                    .HasColumnType("datetime")
                    .HasColumnName("EDDate");

                entity.Property(e => e.FinishingDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.StartingDate).HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TaskDescription).IsUnicode(false);

                entity.Property(e => e.TaskName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Tasks_Projects");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_CmnStatus");
            });

            modelBuilder.Entity<TaskAssignment>(entity =>
            {
                entity.HasKey(e => e.TaskAssignId);

                entity.Property(e => e.TaskAssignId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateBy).HasColumnName("CreateBY");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EmpId).HasColumnName("EmpID");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskAssignments)
                    .HasForeignKey(d => d.TaskId)
                    .HasConstraintName("FK_TaskAssignments_Tasks");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
