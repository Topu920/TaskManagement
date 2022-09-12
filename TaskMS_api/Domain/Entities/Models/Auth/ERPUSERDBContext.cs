using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Models.Auth
{
    public partial class ERPUSERDBContext : DbContext
    {
        public ERPUSERDBContext()
        {
        }

        public ERPUSERDBContext(DbContextOptions<ERPUSERDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserInfo> UserInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:UserConnectionsString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("pk_userinfo");

                entity.ToTable("UserInfo");

                entity.HasIndex(e => e.EmpId, "IX_UserInfo")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmpId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HrmisUser).HasColumnName("HRMIS_User");

                entity.Property(e => e.InvalidLoginTry).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('y')")
                    .IsFixedLength();

                entity.Property(e => e.IsItadmin).HasColumnName("IsITAdmin");

                entity.Property(e => e.IsLocked).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsSetPass).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.MerchandisingUser)
                    .HasColumnName("Merchandising_User")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RowDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UsrDesig)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UsrPass)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
