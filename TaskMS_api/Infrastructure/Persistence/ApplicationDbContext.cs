using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities.Common;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


/*Dont even dare to add new model without knowing the logic because some models are working through ADO.NET*/


namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime, DbSet<RequestLoggerEntity> loggerEntities) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            LoggerEntities = loggerEntities;
        }

        private DbSet<RequestLoggerEntity> LoggerEntities { get; set; }

        //employee related 



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity>? entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.EmployeeId;
                        entry.Entity.CreateDate = _dateTime.Now;
                        entry.Entity.HeadOfficeId = _currentUserService.HeadOfficeId;
                        entry.Entity.BranchOfficeId = _currentUserService.BranchOfficeId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateBy = _currentUserService.EmployeeId;
                        entry.Entity.UpdateDate = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();


            });





        }
    }
}
