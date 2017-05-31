using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Bshkara.Core.Base;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.DAL.Extentions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Bshkara.DAL.DB
{
    /// <summary>
    ///     Entity framework database context
    /// </summary>
    public class EFDbContext :
        IdentityDbContext<UserEntity, RoleEntity, Guid, UserLoginEntity, UserRoleEntity, UserClaimEntity>, IDbContext
    {
        /// <summary>
        ///     Entity framework database context
        /// </summary>
        public EFDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new DbInitializer());
        }

        /// <summary>
        ///     <c>Agencies</c>
        /// </summary>
        public DbSet<AgencyEntity> Agencies { get; set; }

        /// <summary>
        ///     Agency packages
        /// </summary>
        public DbSet<AgencyPackageEntity> AgencyPackages { get; set; }

        /// <summary>
        ///     Agency users
        /// </summary>
        public DbSet<AgencyUserEntity> AgencyUsers { get; set; }

        /// <summary>
        ///     <c>Bookings</c> />
        /// </summary>
        public DbSet<BookingEntity> Bookings { get; set; }

        /// <summary>
        ///     Countries
        /// </summary>
        public DbSet<CountryEntity> Countries { get; set; }

        /// <summary>
        ///     Languages
        /// </summary>
        public DbSet<LanguageEntity> Languages { get; set; }

        /// <summary>
        ///     MaidDocuments
        /// </summary>
        public DbSet<MaidDocumentEntity> MaidDocuments { get; set; }

        /// <summary>
        ///     Maid employment history
        /// </summary>
        public DbSet<MaidEmploymentHistoryEntity> MaidEmploymentHistory { get; set; }

        /// <summary>
        ///     Maids
        /// </summary>
        public DbSet<MaidEntity> Maids { get; set; }

        /// <summary>
        ///     Maid languages
        /// </summary>
        public DbSet<MaidLanguageEntity> MaidLanguages { get; set; }

        /// <summary>
        ///     <c>Maids</c> passports
        /// </summary>
        public DbSet<MaidPassportDetailEntity> MaidsPassports { get; set; }

        /// <summary>
        ///     Maid skills
        /// </summary>
        public DbSet<MaidSkillEntity> MaidSkills { get; set; }

        /// <summary>
        ///     Nationalities
        /// </summary>
        public DbSet<NationalityEntity> Nationalities { get; set; }

        /// <summary>
        ///     Packages
        /// </summary>
        public DbSet<PackageEntity> Packages { get; set; }

        /// <summary>
        ///     Skills
        /// </summary>
        public DbSet<SkillEntity> Skills { get; set; }

        /// <summary>
        ///     VisaStatus
        /// </summary>
        public DbSet<VisaStatusEntity> VisaStatus { get; set; }

        /// <summary>
        ///     Api tokens
        /// </summary>
        public DbSet<ApiTokenEntity> ApiTokens { get; set; }

        /// <summary>
        ///     Get database set
        /// </summary>
        /// <typeparam name="T">Database set</typeparam>
        /// <returns></returns>
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        /// <summary>
        ///     Fill audited fields
        /// </summary>
        /// <returns>Modified entities</returns>
        public override int SaveChanges()
        {
            ChangeTracker.Entries<IAuditedEntity>().ToList().ForEach(
                entry =>
                {
                    var auditedEntity = entry.Entity;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditedEntity.CreatedAt = DateTime.UtcNow;
                            auditedEntity.CreatedById = HttpContext.Current?.User?.GetId();
                            break;
                        case EntityState.Deleted:
                            auditedEntity.UpdatedAt = DateTime.UtcNow;
                            auditedEntity.UpdatedById = HttpContext.Current?.User?.GetId();
                            break;
                        case EntityState.Modified:
                            auditedEntity.UpdatedAt = DateTime.UtcNow;
                            auditedEntity.UpdatedById = HttpContext.Current?.User?.GetId();
                            break;
                        default:
                            return;
                    }
                });

            base.SaveChanges();
            return ChangeTracker.Entries<IAuditedEntity>().Count();
        }

        /// <summary>
        ///     <see cref="Create" /> entity framework database context
        /// </summary>
        /// <returns>
        ///     Entity framework database contex
        /// </returns>
        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        /// <summary>
        ///     Additional changes on model creating
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<RoleEntity>().ToTable("Roles");
            modelBuilder.Entity<UserRoleEntity>().ToTable("UserRoles");
            modelBuilder.Entity<UserClaimEntity>().ToTable("UserClaims");
            modelBuilder.Entity<UserLoginEntity>().ToTable("UserLogins");

            modelBuilder.Entity<UserEntity>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<RoleEntity>().Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<UserEntity>()
                .HasOptional(a => a.CreatedBy).WithMany();

            modelBuilder.Entity<UserEntity>()
                .HasOptional(a => a.UpdatedBy).WithMany();
        }
    }
}