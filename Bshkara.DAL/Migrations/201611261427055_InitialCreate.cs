using System.Data.Entity.Migrations;

namespace Bshkara.DAL.Migrations
{
    public class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Agencies",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        CountryId = c.Guid(),
                        City = c.String(),
                        Logo = c.String(),
                        TradeLicenseNumber = c.String(),
                        Website = c.String(),
                        Email = c.String(),
                        Address_En = c.String(),
                        Address_Ar = c.String(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        Name_En = c.String(),
                        Name_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CountryId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.Countries",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        CountryCode = c.String(),
                        Name_En = c.String(),
                        Name_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.Cities",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        CountryId = c.Guid(false),
                        Name_En = c.String(),
                        Name_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CountryId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.Users",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Name = c.String(),
                        Mobile = c.String(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        IsDeleted = c.Boolean(false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(false),
                        TwoFactorEnabled = c.Boolean(false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(false),
                        AccessFailedCount = c.Int(false),
                        UserName = c.String(false, 256)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                    "dbo.UserClaims",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UserId = c.Guid(false),
                        ClaimType = c.String(),
                        ClaimValue = c.String()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, true)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.UserLogins",
                    c => new
                    {
                        LoginProvider = c.String(false, 128),
                        ProviderKey = c.String(false, 128),
                        UserId = c.Guid(false)
                    })
                .PrimaryKey(t => new {t.LoginProvider, t.ProviderKey, t.UserId})
                .ForeignKey("dbo.Users", t => t.UserId, true)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.UserRoles",
                    c => new
                    {
                        UserId = c.Guid(false),
                        RoleId = c.Guid(false)
                    })
                .PrimaryKey(t => new {t.UserId, t.RoleId})
                .ForeignKey("dbo.Users", t => t.UserId, true)
                .ForeignKey("dbo.Roles", t => t.RoleId, true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                    "dbo.AgencyPackages",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        AgencyId = c.Guid(false),
                        PackageId = c.Guid(false),
                        PackageStatus = c.Int(false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agencies", t => t.AgencyId, true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Packages", t => t.PackageId, true)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.AgencyId)
                .Index(t => t.PackageId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.Packages",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Description_En = c.String(),
                        Description_Ar = c.String(),
                        UsersCount = c.Int(false),
                        ListingCount = c.Int(false),
                        Duration = c.Int(false),
                        Price = c.Decimal(false, 18, 2),
                        Name_En = c.String(),
                        Name_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.AgencyUsers",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        AgencyId = c.Guid(false),
                        UserId = c.Guid(false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agencies", t => t.AgencyId, true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId, true)
                .Index(t => t.AgencyId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.ApiTokens",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Name = c.String(),
                        Token = c.String(),
                        UserId = c.Guid(false),
                        IsBloked = c.Boolean(false),
                        IsDeleted = c.Boolean(false),
                        WhiteDomainList = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId, true)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.Bookings",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        MaidId = c.Guid(false),
                        UserId = c.Guid(false),
                        BookingRef = c.String(),
                        BookingStatus = c.Int(false),
                        Notes = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Maids", t => t.MaidId, true)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.Users", t => t.UserId, true)
                .Index(t => t.MaidId)
                .Index(t => t.UserId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.Maids",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        DateOfBirth = c.DateTime(),
                        MaritalStatus = c.Int(false),
                        NoOfChildren = c.Int(false),
                        Weight = c.Decimal(false, 18, 2),
                        Height = c.Decimal(false, 18, 2),
                        Religion = c.Int(false),
                        Gender = c.Int(false),
                        Education = c.String(),
                        YearsOfExperience = c.Int(false),
                        Phone = c.String(),
                        Address = c.String(),
                        Salary = c.Decimal(false, 18, 2),
                        Availability = c.Boolean(false),
                        Photo = c.String(),
                        Note_En = c.String(),
                        Note_Ar = c.String(),
                        AgencyId = c.Guid(),
                        LivingCityId = c.Guid(),
                        VisaStatusId = c.Guid(),
                        NationalityId = c.Guid(),
                        Name_En = c.String(),
                        Name_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false),
                        Passport_Id = c.Guid()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agencies", t => t.AgencyId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Cities", t => t.LivingCityId)
                .ForeignKey("dbo.Nationalities", t => t.NationalityId)
                .ForeignKey("dbo.MaidPassportDetails", t => t.Passport_Id)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .ForeignKey("dbo.VisaStatus", t => t.VisaStatusId)
                .Index(t => t.AgencyId)
                .Index(t => t.LivingCityId)
                .Index(t => t.VisaStatusId)
                .Index(t => t.NationalityId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById)
                .Index(t => t.Passport_Id);

            CreateTable(
                    "dbo.MaidDocuments",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        MaidId = c.Guid(false),
                        FileName = c.String(),
                        DocumentType = c.String(),
                        DocumentFormat = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Maids", t => t.MaidId, true)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.MaidId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.MaidEmploymentHistory",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        MaidId = c.Guid(false),
                        CountryId = c.Guid(false),
                        Duration = c.Time(false, 7),
                        Descripion_En = c.String(),
                        Descripion_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, true)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Maids", t => t.MaidId, true)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.MaidId)
                .Index(t => t.CountryId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.MaidLanguages",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        MaidId = c.Guid(false),
                        LanguageId = c.Guid(false),
                        SpokenLevel = c.Int(false),
                        ReadLevel = c.Int(false),
                        WrittenLevel = c.Int(false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Languages", t => t.LanguageId, true)
                .ForeignKey("dbo.Maids", t => t.MaidId, true)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.MaidId)
                .Index(t => t.LanguageId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.Languages",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        ShortName = c.String(),
                        Name_En = c.String(),
                        Name_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.Nationalities",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Name_En = c.String(),
                        Name_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.MaidPassportDetails",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        PassportNumber = c.String(),
                        IssueDate = c.DateTime(),
                        ExpiryDate = c.DateTime(),
                        IssuePlace = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.MaidSkills",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        MaidId = c.Guid(false),
                        SkillId = c.Guid(false),
                        SkillLevel = c.Int(false),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Maids", t => t.MaidId, true)
                .ForeignKey("dbo.Skills", t => t.SkillId, true)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.MaidId)
                .Index(t => t.SkillId)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.Skills",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Icon = c.String(),
                        Name_En = c.String(),
                        Name_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.VisaStatus",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Name_En = c.String(),
                        Name_Ar = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CreatedById = c.Guid(),
                        UpdatedById = c.Guid(),
                        IsDeleted = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.UpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.UpdatedById);

            CreateTable(
                    "dbo.Roles",
                    c => new
                    {
                        Id = c.Guid(false, true),
                        Name = c.String(false, 256)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
        }

        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Bookings", "MaidId", "dbo.Maids");
            DropForeignKey("dbo.Maids", "VisaStatusId", "dbo.VisaStatus");
            DropForeignKey("dbo.VisaStatus", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.VisaStatus", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Maids", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.MaidSkills", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.MaidSkills", "SkillId", "dbo.Skills");
            DropForeignKey("dbo.Skills", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Skills", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.MaidSkills", "MaidId", "dbo.Maids");
            DropForeignKey("dbo.MaidSkills", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Maids", "Passport_Id", "dbo.MaidPassportDetails");
            DropForeignKey("dbo.MaidPassportDetails", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.MaidPassportDetails", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Maids", "NationalityId", "dbo.Nationalities");
            DropForeignKey("dbo.Nationalities", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Nationalities", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Maids", "LivingCityId", "dbo.Cities");
            DropForeignKey("dbo.MaidLanguages", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.MaidLanguages", "MaidId", "dbo.Maids");
            DropForeignKey("dbo.MaidLanguages", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.Languages", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Languages", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.MaidLanguages", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.MaidEmploymentHistory", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.MaidEmploymentHistory", "MaidId", "dbo.Maids");
            DropForeignKey("dbo.MaidEmploymentHistory", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.MaidEmploymentHistory", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.MaidDocuments", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.MaidDocuments", "MaidId", "dbo.Maids");
            DropForeignKey("dbo.MaidDocuments", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Maids", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Maids", "AgencyId", "dbo.Agencies");
            DropForeignKey("dbo.Bookings", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.ApiTokens", "UserId", "dbo.Users");
            DropForeignKey("dbo.ApiTokens", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.ApiTokens", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.AgencyUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.AgencyUsers", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.AgencyUsers", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.AgencyUsers", "AgencyId", "dbo.Agencies");
            DropForeignKey("dbo.Agencies", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.AgencyPackages", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.AgencyPackages", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Packages", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Packages", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.AgencyPackages", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.AgencyPackages", "AgencyId", "dbo.Agencies");
            DropForeignKey("dbo.Agencies", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Agencies", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Countries", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Cities", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.Cities", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Users", "UpdatedById", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.VisaStatus", new[] {"UpdatedById"});
            DropIndex("dbo.VisaStatus", new[] {"CreatedById"});
            DropIndex("dbo.Skills", new[] {"UpdatedById"});
            DropIndex("dbo.Skills", new[] {"CreatedById"});
            DropIndex("dbo.MaidSkills", new[] {"UpdatedById"});
            DropIndex("dbo.MaidSkills", new[] {"CreatedById"});
            DropIndex("dbo.MaidSkills", new[] {"SkillId"});
            DropIndex("dbo.MaidSkills", new[] {"MaidId"});
            DropIndex("dbo.MaidPassportDetails", new[] {"UpdatedById"});
            DropIndex("dbo.MaidPassportDetails", new[] {"CreatedById"});
            DropIndex("dbo.Nationalities", new[] {"UpdatedById"});
            DropIndex("dbo.Nationalities", new[] {"CreatedById"});
            DropIndex("dbo.Languages", new[] {"UpdatedById"});
            DropIndex("dbo.Languages", new[] {"CreatedById"});
            DropIndex("dbo.MaidLanguages", new[] {"UpdatedById"});
            DropIndex("dbo.MaidLanguages", new[] {"CreatedById"});
            DropIndex("dbo.MaidLanguages", new[] {"LanguageId"});
            DropIndex("dbo.MaidLanguages", new[] {"MaidId"});
            DropIndex("dbo.MaidEmploymentHistory", new[] {"UpdatedById"});
            DropIndex("dbo.MaidEmploymentHistory", new[] {"CreatedById"});
            DropIndex("dbo.MaidEmploymentHistory", new[] {"CountryId"});
            DropIndex("dbo.MaidEmploymentHistory", new[] {"MaidId"});
            DropIndex("dbo.MaidDocuments", new[] {"UpdatedById"});
            DropIndex("dbo.MaidDocuments", new[] {"CreatedById"});
            DropIndex("dbo.MaidDocuments", new[] {"MaidId"});
            DropIndex("dbo.Maids", new[] {"Passport_Id"});
            DropIndex("dbo.Maids", new[] {"UpdatedById"});
            DropIndex("dbo.Maids", new[] {"CreatedById"});
            DropIndex("dbo.Maids", new[] {"NationalityId"});
            DropIndex("dbo.Maids", new[] {"VisaStatusId"});
            DropIndex("dbo.Maids", new[] {"LivingCityId"});
            DropIndex("dbo.Maids", new[] {"AgencyId"});
            DropIndex("dbo.Bookings", new[] {"UpdatedById"});
            DropIndex("dbo.Bookings", new[] {"CreatedById"});
            DropIndex("dbo.Bookings", new[] {"UserId"});
            DropIndex("dbo.Bookings", new[] {"MaidId"});
            DropIndex("dbo.ApiTokens", new[] {"UpdatedById"});
            DropIndex("dbo.ApiTokens", new[] {"CreatedById"});
            DropIndex("dbo.ApiTokens", new[] {"UserId"});
            DropIndex("dbo.AgencyUsers", new[] {"UpdatedById"});
            DropIndex("dbo.AgencyUsers", new[] {"CreatedById"});
            DropIndex("dbo.AgencyUsers", new[] {"UserId"});
            DropIndex("dbo.AgencyUsers", new[] {"AgencyId"});
            DropIndex("dbo.Packages", new[] {"UpdatedById"});
            DropIndex("dbo.Packages", new[] {"CreatedById"});
            DropIndex("dbo.AgencyPackages", new[] {"UpdatedById"});
            DropIndex("dbo.AgencyPackages", new[] {"CreatedById"});
            DropIndex("dbo.AgencyPackages", new[] {"PackageId"});
            DropIndex("dbo.AgencyPackages", new[] {"AgencyId"});
            DropIndex("dbo.UserRoles", new[] {"RoleId"});
            DropIndex("dbo.UserRoles", new[] {"UserId"});
            DropIndex("dbo.UserLogins", new[] {"UserId"});
            DropIndex("dbo.UserClaims", new[] {"UserId"});
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Users", new[] {"UpdatedById"});
            DropIndex("dbo.Users", new[] {"CreatedById"});
            DropIndex("dbo.Cities", new[] {"UpdatedById"});
            DropIndex("dbo.Cities", new[] {"CreatedById"});
            DropIndex("dbo.Cities", new[] {"CountryId"});
            DropIndex("dbo.Countries", new[] {"UpdatedById"});
            DropIndex("dbo.Countries", new[] {"CreatedById"});
            DropIndex("dbo.Agencies", new[] {"UpdatedById"});
            DropIndex("dbo.Agencies", new[] {"CreatedById"});
            DropIndex("dbo.Agencies", new[] {"CountryId"});
            DropTable("dbo.Roles");
            DropTable("dbo.VisaStatus");
            DropTable("dbo.Skills");
            DropTable("dbo.MaidSkills");
            DropTable("dbo.MaidPassportDetails");
            DropTable("dbo.Nationalities");
            DropTable("dbo.Languages");
            DropTable("dbo.MaidLanguages");
            DropTable("dbo.MaidEmploymentHistory");
            DropTable("dbo.MaidDocuments");
            DropTable("dbo.Maids");
            DropTable("dbo.Bookings");
            DropTable("dbo.ApiTokens");
            DropTable("dbo.AgencyUsers");
            DropTable("dbo.Packages");
            DropTable("dbo.AgencyPackages");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Cities");
            DropTable("dbo.Countries");
            DropTable("dbo.Agencies");
        }
    }
}