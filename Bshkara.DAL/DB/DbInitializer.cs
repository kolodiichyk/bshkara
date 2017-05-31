using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Bashkra.Shared.Enums;
using Bshkara.Core.Base;
using Bshkara.Core.Entities;
using Bshkara.DAL.Identity;

namespace Bshkara.DAL.DB
{
    public class DbInitializer : CreateDatabaseIfNotExists<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            Task.Run(async () => { await SeedAsync(context); }).Wait();
        }

        protected async Task SeedAsync(EFDbContext context)
        {
            base.Seed(context);

            var userManager = new ApplicationUserManager(new CustomUserStore(context));
            var roleManager = new ApplicationRoleManager(new CustomRoleStore(context));

            // create default roles
            await roleManager.CreateAsync(new RoleEntity(RoleEntity.AdminRoleName));
            await roleManager.CreateAsync(new RoleEntity(RoleEntity.AgentAdminRoleName));
            await roleManager.CreateAsync(new RoleEntity(RoleEntity.AgentUserRoleName));

            // add admin
            await
                userManager.CreateAsync(
                    new UserEntity
                    {
                        UserName = "admin@bshkara.com",
                        Email = "admin@bshkara.com",
                        Name = "Bshkara",
                        EmailConfirmed = true
                    }, "bshkara");

            var admin = await userManager.FindByEmailAsync("admin@bshkara.com");
            await userManager.AddToRoleAsync(admin.Id, RoleEntity.AdminRoleName);
            await userManager.AddToRoleAsync(admin.Id, RoleEntity.AgentAdminRoleName);
            await userManager.AddToRoleAsync(admin.Id, RoleEntity.AgentUserRoleName);

            GetSkills().ForEach(skill => context.Skills.Add(skill));
            GetLanguages().ForEach(languege => context.Languages.Add(languege));
            GetNationalities().ForEach(newationality => context.Nationalities.Add(newationality));
            GetCountries().ForEach(country => context.Countries.Add(country));
            GetApiTokens(admin).ForEach(token => context.ApiTokens.Add(token));
            GetPackages().ForEach(package => context.Packages.Add(package));
            GetAgencies().ForEach(agency => context.Agencies.Add(agency));
            GetMaids().ForEach(maid => context.Maids.Add(maid));
        }

        private List<ApiTokenEntity> GetApiTokens(UserEntity admin)
        {
            return new List<ApiTokenEntity>
            {
                new ApiTokenEntity
                {
                    Token = "GJ9D0QN8n0KGHXlZ+J2byw==",
                    Name = "Mobile API",
                    UserId = admin.Id,
                    IsDeleted = false,
                    IsBloked = false,
                    User = admin
                }
            };
        }

        private List<PackageEntity> GetPackages()
        {
            return new List<PackageEntity>
            {
                new PackageEntity
                {
                    Name = new LocalizedName("Free", ""),
                    Description = new LocalizedName("Try our service", ""),
                    Price = 0,
                    UsersCount = 1,
                    ListingCount = 5,
                    Duration = 30
                },
                new PackageEntity
                {
                    Name = new LocalizedName("Start", ""),
                    Description = new LocalizedName("For small agencies", ""),
                    Price = 50,
                    UsersCount = 2,
                    ListingCount = 20,
                    Duration = 100
                },
                new PackageEntity
                {
                    Name = new LocalizedName("Business", ""),
                    Description = new LocalizedName("For growing up agencies", ""),
                    Price = 100,
                    UsersCount = 5,
                    ListingCount = 50,
                    Duration = 150
                },
                new PackageEntity
                {
                    Name = new LocalizedName("Professional", ""),
                    Description = new LocalizedName("For professional big agencies", ""),
                    Price = 200,
                    UsersCount = 10,
                    ListingCount = 100,
                    Duration = 300
                }
            };
        }

        private List<MaidEntity> GetMaids()
        {
            return new List<MaidEntity>
            {
                new MaidEntity
                {
                    Name = new LocalizedName("Eric Craig", "Eric Craig"),
                    AgencyId = new Guid("1fd0c5c4-5061-4881-bceb-e9fde112fa56"),
                    Photo = "/Temp/team01.jpg",
                    Address = "1323 Ritter Street Anniston, AL 36201",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("December 16, 1980"),
                    Phone = "619-218-6352",
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Single,
                    NoOfChildren = 1
                },
                new MaidEntity
                {
                    Name = new LocalizedName("Christopher Calvo", "Christopher Calvo"),
                    AgencyId = new Guid("1fd0c5c4-5061-4881-bceb-e9fde112fa56"),
                    Photo = "/Temp/team02.jpg",
                    Address = "709 Maple Avenue Gooding, ID 83330",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("March 28, 1985"),
                    Phone = "361-743-2664",
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Single,
                    NoOfChildren = 1
                },
                new MaidEntity
                {
                    Name = new LocalizedName("Joseph Kellam", "Joseph Kellam"),
                    AgencyId = new Guid("1fde7f85-99d4-4c79-a3f8-c40858187e93"),
                    Photo = "/Temp/team03.jpg",
                    Address = "53 Oak Drive Albany, NY 12210",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("July 22, 1971"),
                    Phone = "713-430-8261",
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Marid,
                    NoOfChildren = 1
                },
                new MaidEntity
                {
                    Name = new LocalizedName("Wanda Sullivan", "Wanda Sullivan"),
                    AgencyId = new Guid("1fd0c5c4-5061-4881-bceb-e9fde112fa56"),
                    Photo = "/Temp/team04.jpg",
                    Address = "2709 Maple Avenue Gooding, ID 83330",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("April 16, 1973"),
                    Phone = "352-239-6458",
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Single,
                    NoOfChildren = 0
                },
                new MaidEntity
                {
                    Name = new LocalizedName("Assunta Rorie", "Assunta Rorie"),
                    AgencyId = new Guid("1fde7f85-99d4-4c79-a3f8-c40858187e93"),
                    Photo = "/Temp/team05.jpg",
                    Address = "2248 Veltri Drive Homer, AK 99603",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("July 29, 1984"),
                    Phone = "212-981-1097",
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Marid,
                    NoOfChildren = 2
                },
                new MaidEntity
                {
                    Name = new LocalizedName("Rebecca Self", "Rebecca Self"),
                    AgencyId = new Guid("1fd0c5c4-5061-4881-bceb-e9fde112fa56"),
                    Photo = "/Temp/team06.jpg",
                    Address = "2827 Jody Road Philadelphia, PA 19108",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("March 25, 1978"),
                    Phone = "610-600-0614",
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Marid,
                    NoOfChildren = 2
                },
                new MaidEntity
                {
                    Name = new LocalizedName("David Kenney", "David Kenney"),
                    AgencyId = new Guid("1fde7f85-99d4-4c79-a3f8-c40858187e93"),
                    Photo = "/Temp/team07.jpg",
                    Address = "3232 Turkey Pen Road Huntington, NY 11743",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("May 7, 1978"),
                    Phone = "917-226-3111",
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Single,
                    NoOfChildren = 0
                },
                new MaidEntity
                {
                    Name = new LocalizedName("Mary Burchfield", "Mary Burchfield"),
                    AgencyId = new Guid("1fde7f85-99d4-4c79-a3f8-c40858187e93"),
                    Photo = "/Temp/team08.jpg",
                    Address = "458 Murphy Court Riverside, CA 92504",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("July 13, 1988"),
                    Phone = "951-789-6973",
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Single,
                    NoOfChildren = 0
                },
                new MaidEntity
                {
                    Name = new LocalizedName("Laura Heath", "Laura Heath"),
                    AgencyId = new Guid("1fd0c5c4-5061-4881-bceb-e9fde112fa56"),
                    Photo = "/Temp/team09.jpg",
                    Address = "1747 Edington Drive Roswell, GA 30076",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("May 24, 1989"),
                    Phone = "678-777-6407",
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Single,
                    NoOfChildren = 0
                },
                new MaidEntity
                {
                    Name = new LocalizedName("Victor Groves", "Victor Groves"),
                    AgencyId = new Guid("1fd0c5c4-5061-4881-bceb-e9fde112fa56"),
                    Photo = "/Temp/team10.jpg",
                    Address = "1714 Reppert Coal Road Jackson, MS 39201",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("November 15, 1989"),
                    Phone = "601-228-0469",
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Marid,
                    NoOfChildren = 3
                },
                new MaidEntity
                {
                    Name = new LocalizedName("Ellen Jones", "Ellen Jones"),
                    AgencyId = new Guid("1fd0c5c4-5061-4881-bceb-e9fde112fa56"),
                    Photo = "/Temp/team11.jpg",
                    Address = "2944 Rainbow Drive Akron, OH 44308",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("October 10, 1989"),
                    Phone = "330-436-9063",
                    Gender = Gender.Female,
                    MaritalStatus = MaritalStatus.Single,
                    NoOfChildren = 0
                },
                new MaidEntity
                {
                    Name = new LocalizedName("Rudolph Teeter", "Rudolph Teeter"),
                    AgencyId = new Guid("1fd0c5c4-5061-4881-bceb-e9fde112fa56"),
                    Photo = "/Temp/team12.jpg",
                    Address = "1453 Rhapsody Street Gainesville, FL 32601",
                    Availability = true,
                    DateOfBirth = DateTime.Parse("August 10, 1968"),
                    Phone = "352-359-5871",
                    Gender = Gender.Male,
                    MaritalStatus = MaritalStatus.Marid,
                    NoOfChildren = 2
                }
            };
        }

        private List<AgencyEntity> GetAgencies()
        {
            return new List<AgencyEntity>
            {
                new AgencyEntity
                {
                    Id = new Guid("1fd0c5c4-5061-4881-bceb-e9fde112fa56"),
                    Name = new LocalizedName("Agilies", "Agilies"),
                    Logo = "/Temp/agilies.png",
                    Email = "support@agilies.com",
                    Website = "www.agilies.com",
                    Address =
                        new LocalizedName("8651 Pineknoll Dr. Goodlettsville, TN 37072",
                            "8651 Pineknoll الدكتور ناشفيل، TN 37072")
                },
                new AgencyEntity
                {
                    Id = new Guid("1fde7f85-99d4-4c79-a3f8-c40858187e93"),
                    Name = new LocalizedName("Rainmeter", "Rainmeter"),
                    Logo = "/Temp/rainmeter.png",
                    Email = "support@rainmeter.com",
                    Website = "www.rainmeter.com",
                    Address =
                        new LocalizedName("32 E. Brewery Drive Perth Amboy, NJ 08861",
                            "32 E. بيرة محرك بيرث امبوي، NJ 08861")
                }
            };
        }

        private List<SkillEntity> GetSkills()
        {
            return new List<SkillEntity>
            {
                new SkillEntity
                {
                    Id = new Guid("404350b0-381f-4474-94f7-d414e2a7ef78"),
                    Name = new LocalizedName("Cocking", "رد ديك البندقية"),
                    Icon = "/Temp/skill_cocking.png"
                },
                new SkillEntity
                {
                    Id = new Guid("3f77673c-85a6-4554-8f74-09fd4fa21b62"),
                    Name = new LocalizedName("Washing", "غسل"),
                    Icon = "/Temp/skill_washing.png"
                },
                new SkillEntity
                {
                    Id = new Guid("d641f028-72b8-44fc-929d-197547516ff3"),
                    Name = new LocalizedName("Adult Care", "العناية الكبار"),
                    Icon = "/Temp/skill_adult_care.png"
                },
                new SkillEntity
                {
                    Id = new Guid("e272526f-6ba1-49aa-b98a-d770e592d9cb"),
                    Name = new LocalizedName("Driving", "القيادة"),
                    Icon = "/Temp/skill_driving.png"
                },
                new SkillEntity
                {
                    Id = new Guid("0845a5b6-0c08-4348-b82c-0e867a2a0193"),
                    Name = new LocalizedName("House cleaning", "تنظيف المنزل"),
                    Icon = "/Temp/skill_hause_cleaning.png"
                },
                new SkillEntity
                {
                    Id = new Guid("b4b20d71-aaeb-4550-9185-ce378d751cbe"),
                    Name = new LocalizedName("Babysitting", "مجالسة الأطفال"),
                    Icon = "/Temp/skill_babysitting.png"
                },
                new SkillEntity
                {
                    Id = new Guid("2bcf8d75-0a3d-4c33-85ad-6e36a780d519"),
                    Name = new LocalizedName("Pet Care", "رعاية الحيوانات الاليفة"),
                    Icon = "/Temp/skill_pet_care.png"
                },
                new SkillEntity
                {
                    Id = new Guid("a031c3e8-6473-432c-8066-fc6aa8651b4e"),
                    Name = new LocalizedName("Decorating", "تزيين"),
                    Icon = "/Temp/skill_decorating.png"
                }
            };
        }

        private List<LanguageEntity> GetLanguages()
        {
            return new List<LanguageEntity>
            {
                new LanguageEntity {Name = new LocalizedName("English", "الإنجليزية"), ShortName = "ENG"},
                new LanguageEntity {Name = new LocalizedName("Arabic", "العربية"), ShortName = "AR"},
                new LanguageEntity {Name = new LocalizedName("Indian", "هندي"), ShortName = "IND"}
            };
        }

        private List<NationalityEntity> GetNationalities()
        {
            return new List<NationalityEntity>
            {
                new NationalityEntity {Name = new LocalizedName("Afghan", "الأفغاني")},
                new NationalityEntity {Name = new LocalizedName("Albanian", "الألبانية")},
                new NationalityEntity {Name = new LocalizedName("Algerian", "جزائري")},
                new NationalityEntity {Name = new LocalizedName("Andorran", "أندورا")},
                new NationalityEntity {Name = new LocalizedName("Angolan", "الأنغولي")},
                new NationalityEntity {Name = new LocalizedName("Argentinian", "الأرجنتيني")},
                new NationalityEntity {Name = new LocalizedName("Armenian", "الأرميني")},
                new NationalityEntity {Name = new LocalizedName("Australian", "الأسترالي")},
                new NationalityEntity {Name = new LocalizedName("Austrian", "النمساوي")},
                new NationalityEntity {Name = new LocalizedName("Azerbaijani", "أذربيجان")},
                new NationalityEntity {Name = new LocalizedName("Bahamian", "باهامى")},
                new NationalityEntity {Name = new LocalizedName("Bahraini", "البحريني")},
                new NationalityEntity {Name = new LocalizedName("Bangladeshi", "بنجلاديش")},
                new NationalityEntity {Name = new LocalizedName("Barbadian", "باربادوسي")},
                new NationalityEntity {Name = new LocalizedName("Belarusian", "البيلاروسية")},
                new NationalityEntity {Name = new LocalizedName("Belgian", "بلجيكي")},
                new NationalityEntity {Name = new LocalizedName("Belizean", "بليز")},
                new NationalityEntity {Name = new LocalizedName("Beninese", "بنين")},
                new NationalityEntity {Name = new LocalizedName("Bhutanese", "بوتان")},
                new NationalityEntity {Name = new LocalizedName("Bolivian", "بوليفي")},
                new NationalityEntity {Name = new LocalizedName("Bosnian", "البوسنية")},
                new NationalityEntity {Name = new LocalizedName("Botswanan", "بوتسوانا")},
                new NationalityEntity {Name = new LocalizedName("Brazilian", "برازيلي")},
                new NationalityEntity {Name = new LocalizedName("British", "بريطاني")},
                new NationalityEntity {Name = new LocalizedName("Bruneian", "بروناى")},
                new NationalityEntity {Name = new LocalizedName("Bulgarian", "البلغارية")},
                new NationalityEntity {Name = new LocalizedName("Burkinese", "البوركينية")},
                new NationalityEntity {Name = new LocalizedName("Burmese", "البورمية")},
                new NationalityEntity {Name = new LocalizedName("Burundian", "بوروندي")},
                new NationalityEntity {Name = new LocalizedName("Cambodian", "كمبوديا")},
                new NationalityEntity {Name = new LocalizedName("Cameroonian", "الكاميروني")},
                new NationalityEntity {Name = new LocalizedName("Canadian", "الكندية")},
                new NationalityEntity {Name = new LocalizedName("Cape Verdean", "الرأس الأخضر")},
                new NationalityEntity {Name = new LocalizedName("Chadian", "تشاد")},
                new NationalityEntity {Name = new LocalizedName("Chilean", "شيلي")},
                new NationalityEntity {Name = new LocalizedName("Chinese", "الصينية")},
                new NationalityEntity {Name = new LocalizedName("Colombian", "كولومبي")},
                new NationalityEntity {Name = new LocalizedName("Congolese", "الكونغولي")},
                new NationalityEntity {Name = new LocalizedName("Costa Rican", "كوستاريكا")},
                new NationalityEntity {Name = new LocalizedName("Croatian", "الكرواتية")},
                new NationalityEntity {Name = new LocalizedName("Cuban", "الكوبية")},
                new NationalityEntity {Name = new LocalizedName("Cypriot", "القبرصي")},
                new NationalityEntity {Name = new LocalizedName("Czech", "تشيكي")},
                new NationalityEntity {Name = new LocalizedName("Danish", "دانماركي")},
                new NationalityEntity {Name = new LocalizedName("Djiboutian", "جيبوتي")},
                new NationalityEntity {Name = new LocalizedName("Dominican", "الدومينيكان")},
                new NationalityEntity {Name = new LocalizedName("Dominican", "الدومينيكان")},
                new NationalityEntity {Name = new LocalizedName("Ecuadorean", "الإكوادوري")},
                new NationalityEntity {Name = new LocalizedName("Egyptian", "مصري")},
                new NationalityEntity {Name = new LocalizedName("Salvadorean", "السلفادورية")},
                new NationalityEntity {Name = new LocalizedName("English", "الإنجليزية")},
                new NationalityEntity {Name = new LocalizedName("Eritrean", "الإريترية")},
                new NationalityEntity {Name = new LocalizedName("Estonian", "الإستونية")},
                new NationalityEntity {Name = new LocalizedName("Ethiopian", "حبشي")},
                new NationalityEntity {Name = new LocalizedName("Fijian", "الفيجية")},
                new NationalityEntity {Name = new LocalizedName("Finnish", "اللغة الفنلندية")},
                new NationalityEntity {Name = new LocalizedName("French", "اللغة الفرنسية")},
                new NationalityEntity {Name = new LocalizedName("Gabonese", "الجابون")},
                new NationalityEntity {Name = new LocalizedName("Gambian", "غامبي")},
                new NationalityEntity {Name = new LocalizedName("Georgian", "الجورجية")},
                new NationalityEntity {Name = new LocalizedName("German", "ألماني")},
                new NationalityEntity {Name = new LocalizedName("Ghanaian", "الغاني")},
                new NationalityEntity {Name = new LocalizedName("Greek", "اللغة اليونانية")},
                new NationalityEntity {Name = new LocalizedName("Grenadian", "جرينادا")},
                new NationalityEntity {Name = new LocalizedName("Guatemalan", "غواتيمالا")},
                new NationalityEntity {Name = new LocalizedName("Guinean", "غينيا")},
                new NationalityEntity {Name = new LocalizedName("Guyanese", "جويانا")},
                new NationalityEntity {Name = new LocalizedName("Haitian", "هايتي")},
                new NationalityEntity {Name = new LocalizedName("Dutch", "اللغة الهولندية")},
                new NationalityEntity {Name = new LocalizedName("Honduran", "هندوراس")},
                new NationalityEntity {Name = new LocalizedName("Hungarian", "الهنغارية")},
                new NationalityEntity {Name = new LocalizedName("Icelandic", "أيسلندي")},
                new NationalityEntity {Name = new LocalizedName("Indian", "هندي")},
                new NationalityEntity {Name = new LocalizedName("Indonesian", "الأندونيسية")},
                new NationalityEntity {Name = new LocalizedName("Iranian", "إيراني")},
                new NationalityEntity {Name = new LocalizedName("Iraqi", "عراقي")},
                new NationalityEntity {Name = new LocalizedName("Irish", "الأيرلندية")},
                new NationalityEntity {Name = new LocalizedName("Italian", "الإيطالي")},
                new NationalityEntity {Name = new LocalizedName("Jamaican", "الجامايكي")},
                new NationalityEntity {Name = new LocalizedName("Japanese", "اليابانية")},
                new NationalityEntity {Name = new LocalizedName("Jordanian", "أردني")},
                new NationalityEntity {Name = new LocalizedName("Kazakh", "الكازاخستاني")},
                new NationalityEntity {Name = new LocalizedName("Kenyan", "الكيني")},
                new NationalityEntity {Name = new LocalizedName("Kuwaiti", "كويتي")},
                new NationalityEntity {Name = new LocalizedName("Laotian", "اللاوسي")},
                new NationalityEntity {Name = new LocalizedName("Latvian", "اللاتفية")},
                new NationalityEntity {Name = new LocalizedName("Lebanese", "لبناني")},
                new NationalityEntity {Name = new LocalizedName("Liberian", "ليبيري")},
                new NationalityEntity {Name = new LocalizedName("Libyan", "ليبي")},
                new NationalityEntity {Name = new LocalizedName("Lithuanian", "اللتوانية")},
                new NationalityEntity {Name = new LocalizedName("Macedonian", "المقدونية")},
                new NationalityEntity {Name = new LocalizedName("Malagasy", "مدغشقر")},
                new NationalityEntity {Name = new LocalizedName("Malawian", "مالاوى")},
                new NationalityEntity {Name = new LocalizedName("Malaysian", "الماليزية")},
                new NationalityEntity {Name = new LocalizedName("Maldivian", "المالديف")},
                new NationalityEntity {Name = new LocalizedName("Malian", "مالي")},
                new NationalityEntity {Name = new LocalizedName("Maltese", "المالطية")},
                new NationalityEntity {Name = new LocalizedName("Mauritanian", "الموريتاني")},
                new NationalityEntity {Name = new LocalizedName("Mauritian", "موريشيوس")},
                new NationalityEntity {Name = new LocalizedName("Mexican", "المكسيكي")},
                new NationalityEntity {Name = new LocalizedName("Moldovan", "مولدوفا")},
                new NationalityEntity {Name = new LocalizedName("Monacan", "موناكو")},
                new NationalityEntity {Name = new LocalizedName("Mongolian", "المنغولية")},
                new NationalityEntity {Name = new LocalizedName("Montenegrin", "الجبل الأسود")},
                new NationalityEntity {Name = new LocalizedName("Moroccan", "مغربي")},
                new NationalityEntity {Name = new LocalizedName("Mozambican", "موزمبيق")},
                new NationalityEntity {Name = new LocalizedName("Namibian", "الناميبي")},
                new NationalityEntity {Name = new LocalizedName("Nepalese", "النيبالية")},
                new NationalityEntity {Name = new LocalizedName("Dutch", "اللغة الهولندية")},
                new NationalityEntity {Name = new LocalizedName("New Zealand", "نيوزيلاندا")},
                new NationalityEntity {Name = new LocalizedName("Nicaraguan", "نيكاراغوا")},
                new NationalityEntity {Name = new LocalizedName("Nigerien", "النيجر")},
                new NationalityEntity {Name = new LocalizedName("Nigerian", "النيجيري")},
                new NationalityEntity {Name = new LocalizedName("North Korean", "كوري شمالي")},
                new NationalityEntity {Name = new LocalizedName("Norwegian", "النرويجية")},
                new NationalityEntity {Name = new LocalizedName("Omani", "العماني")},
                new NationalityEntity {Name = new LocalizedName("Pakistani", "باكستاني")},
                new NationalityEntity {Name = new LocalizedName("Panamanian", "بنمي")},
                new NationalityEntity
                {
                    Name = new LocalizedName("Papua New Guinean or Guinean", "بابوا غينيا الجديدة أو الغينية")
                },
                new NationalityEntity {Name = new LocalizedName("Paraguayan", "باراغواي")},
                new NationalityEntity {Name = new LocalizedName("Peruvian", "بيرو")},
                new NationalityEntity {Name = new LocalizedName("Philippine", "الفلبين")},
                new NationalityEntity {Name = new LocalizedName("Polish", "بولندي")},
                new NationalityEntity {Name = new LocalizedName("Portuguese", "البرتغالية")},
                new NationalityEntity {Name = new LocalizedName("Qatari", "القطري")},
                new NationalityEntity {Name = new LocalizedName("Romanian", "روماني")},
                new NationalityEntity {Name = new LocalizedName("Russian", "الروسية")},
                new NationalityEntity {Name = new LocalizedName("Rwandan", "رواندا")},
                new NationalityEntity {Name = new LocalizedName("Saudi Arabian", "سعودي")},
                new NationalityEntity {Name = new LocalizedName("Scottish", "الاسكتلندي")},
                new NationalityEntity {Name = new LocalizedName("Senegalese", "سنغالي")},
                new NationalityEntity {Name = new LocalizedName("Serbian", "صربي")},
                new NationalityEntity {Name = new LocalizedName("Seychellois", "سيشل")},
                new NationalityEntity {Name = new LocalizedName("Sierra Leonian", "السيراليوني")},
                new NationalityEntity {Name = new LocalizedName("Singaporean", "سنغافورة")},
                new NationalityEntity {Name = new LocalizedName("Slovak", "السلوفاكية")},
                new NationalityEntity {Name = new LocalizedName("Slovenian", "سلوفيني")},
                new NationalityEntity {Name = new LocalizedName("Somali", "الصومالية")},
                new NationalityEntity {Name = new LocalizedName("South African", "جنوب افريقيا")},
                new NationalityEntity {Name = new LocalizedName("South Korean", "كوريا الجنوبية")},
                new NationalityEntity {Name = new LocalizedName("Spanish", "الأسبانية")},
                new NationalityEntity {Name = new LocalizedName("Sri Lankan", "سري لانكا")},
                new NationalityEntity {Name = new LocalizedName("Sudanese", "سوداني")},
                new NationalityEntity {Name = new LocalizedName("Surinamese", "سورينامي")},
                new NationalityEntity {Name = new LocalizedName("Swazi", "سوازي")},
                new NationalityEntity {Name = new LocalizedName("Swedish", "اللغة السويدية")},
                new NationalityEntity {Name = new LocalizedName("Swiss", "سويسري")},
                new NationalityEntity {Name = new LocalizedName("Syrian", "سوري")},
                new NationalityEntity {Name = new LocalizedName("Taiwanese", "تايوانية")},
                new NationalityEntity {Name = new LocalizedName("Tadjik", "الطاجيك")},
                new NationalityEntity {Name = new LocalizedName("Tanzanian", "تنزانية")},
                new NationalityEntity {Name = new LocalizedName("Thai", "التايلاندية")},
                new NationalityEntity {Name = new LocalizedName("Togolese", "توجو")},
                new NationalityEntity {Name = new LocalizedName("Trinidadian", "ترينيداد")},
                new NationalityEntity {Name = new LocalizedName("Tunisian", "التونسية")},
                new NationalityEntity {Name = new LocalizedName("Turkish", "اللغة التركية")},
                new NationalityEntity {Name = new LocalizedName("Turkmen", "التركمان")},
                new NationalityEntity {Name = new LocalizedName("Tuvaluan", "التوفالية")},
                new NationalityEntity {Name = new LocalizedName("Ugandan", "الأوغندي")},
                new NationalityEntity {Name = new LocalizedName("Ukrainian", "الأوكراني")},
                new NationalityEntity {Name = new LocalizedName("Emirates", "الإمارات")},
                new NationalityEntity {Name = new LocalizedName("British", "بريطاني")},
                new NationalityEntity {Name = new LocalizedName("US", "لنا")},
                new NationalityEntity {Name = new LocalizedName("Uruguayan", "أوروجواي")},
                new NationalityEntity {Name = new LocalizedName("Uzbek", "الأوزبكي")},
                new NationalityEntity {Name = new LocalizedName("Vanuatuan", "فانواتو")},
                new NationalityEntity {Name = new LocalizedName("Venezuelan", "فنزويلا")},
                new NationalityEntity {Name = new LocalizedName("Vietnamese", "الفيتنامية")},
                new NationalityEntity {Name = new LocalizedName("Welsh", "ويلزي")},
                new NationalityEntity {Name = new LocalizedName("Western Samoan", "ساموا الغربية")},
                new NationalityEntity {Name = new LocalizedName("Yemeni", "يمني")},
                new NationalityEntity {Name = new LocalizedName("Yugoslav", "اليوغوسلافية")},
                new NationalityEntity {Name = new LocalizedName("Zaïrean", "زائير")},
                new NationalityEntity {Name = new LocalizedName("Zambian", "زامبيا")},
                new NationalityEntity {Name = new LocalizedName("Zimbabwean", "زيمبابوي")}
            };
        }

        private List<CountryEntity> GetCountries()
        {
            return new List<CountryEntity>
            {
                new CountryEntity {Name = new LocalizedName("Afghanistan", "أفغانستان")},
                new CountryEntity {Name = new LocalizedName("Albania", "ألبانيا")},
                new CountryEntity {Name = new LocalizedName("Algeria", "الجزائر")},
                new CountryEntity {Name = new LocalizedName("Andorra", "أندورا")},
                new CountryEntity {Name = new LocalizedName("Angola", "أنغولا")},
                new CountryEntity {Name = new LocalizedName("Argentina", "الأرجنتين")},
                new CountryEntity {Name = new LocalizedName("Armenia", "أرمينيا")},
                new CountryEntity {Name = new LocalizedName("Australia", "أستراليا")},
                new CountryEntity {Name = new LocalizedName("Austria", "النمسا")},
                new CountryEntity {Name = new LocalizedName("Azerbaijan", "أذربيجان")},
                new CountryEntity {Name = new LocalizedName("Bahamas", "الباهاما")},
                new CountryEntity {Name = new LocalizedName("Bahrain", "البحرين")},
                new CountryEntity {Name = new LocalizedName("Bangladesh", "بنغلاديش")},
                new CountryEntity {Name = new LocalizedName("Barbados", "بربادوس")},
                new CountryEntity {Name = new LocalizedName("Belarus", "روسيا البيضاء")},
                new CountryEntity {Name = new LocalizedName("Belgium", "بلجيكا")},
                new CountryEntity {Name = new LocalizedName("Belize", "بليز")},
                new CountryEntity {Name = new LocalizedName("Benin", "بنين")},
                new CountryEntity {Name = new LocalizedName("Bhutan", "بوتان")},
                new CountryEntity {Name = new LocalizedName("Bolivia", "بوليفيا")},
                new CountryEntity {Name = new LocalizedName("Bosnia-Herzegovina", "البوسنة والهرسك")},
                new CountryEntity {Name = new LocalizedName("Botswana", "بوتسوانا")},
                new CountryEntity {Name = new LocalizedName("Brazil", "البرازيل")},
                new CountryEntity {Name = new LocalizedName("Britain", "بريطانيا")},
                new CountryEntity {Name = new LocalizedName("Brunei", "بروناي")},
                new CountryEntity {Name = new LocalizedName("Bulgaria", "بلغاريا")},
                new CountryEntity {Name = new LocalizedName("Burkina", "فاسو")},
                new CountryEntity {Name = new LocalizedName("Burma", "بورما")},
                new CountryEntity {Name = new LocalizedName("Burundi", "بوروندي")},
                new CountryEntity {Name = new LocalizedName("Cambodia", "كمبوديا")},
                new CountryEntity {Name = new LocalizedName("Cameroon", "الكاميرون")},
                new CountryEntity {Name = new LocalizedName("Canada", "كندا")},
                new CountryEntity {Name = new LocalizedName("Cape Verde Islands", "جزر الرأس الأخضر")},
                new CountryEntity {Name = new LocalizedName("Chad", "تشاد")},
                new CountryEntity {Name = new LocalizedName("Chile", "تشيلي")},
                new CountryEntity {Name = new LocalizedName("China", "الصين")},
                new CountryEntity {Name = new LocalizedName("Colombia", "كولومبيا")},
                new CountryEntity {Name = new LocalizedName("Congo", "الكونغو")},
                new CountryEntity {Name = new LocalizedName("Costa Rica", "كوستا ريكا")},
                new CountryEntity {Name = new LocalizedName("Croatia", "كرواتيا")},
                new CountryEntity {Name = new LocalizedName("Cuba", "كوبا")},
                new CountryEntity {Name = new LocalizedName("Cyprus", "قبرص")},
                new CountryEntity {Name = new LocalizedName("Czech Republic", "جمهورية التشيك")},
                new CountryEntity {Name = new LocalizedName("Denmark", "الدنمارك")},
                new CountryEntity {Name = new LocalizedName("Djibouti", "جيبوتي")},
                new CountryEntity {Name = new LocalizedName("Dominica", "دومينيكا")},
                new CountryEntity {Name = new LocalizedName("Dominican Republic", "جمهورية الدومنيكان")},
                new CountryEntity {Name = new LocalizedName("Ecuador", "الإكوادور")},
                new CountryEntity {Name = new LocalizedName("Egypt", "مصر")},
                new CountryEntity {Name = new LocalizedName("El Salvador", "السلفادور")},
                new CountryEntity {Name = new LocalizedName("England", "إنكلترا")},
                new CountryEntity {Name = new LocalizedName("Eritrea", "إريتريا")},
                new CountryEntity {Name = new LocalizedName("Estonia", "استونيا")},
                new CountryEntity {Name = new LocalizedName("Ethiopia", "أثيوبيا")},
                new CountryEntity {Name = new LocalizedName("Fiji", "فيجي")},
                new CountryEntity {Name = new LocalizedName("Finland", "فنلندا")},
                new CountryEntity {Name = new LocalizedName("France", "فرنسا")},
                new CountryEntity {Name = new LocalizedName("Gabon", "الغابون")},
                new CountryEntity {Name = new LocalizedName("Gambia, the", "غامبيا، و")},
                new CountryEntity {Name = new LocalizedName("Georgia", "جورجيا")},
                new CountryEntity {Name = new LocalizedName("Germany", "ألمانيا")},
                new CountryEntity {Name = new LocalizedName("Ghana", "غانا")},
                new CountryEntity {Name = new LocalizedName("Greece", "اليونان")},
                new CountryEntity {Name = new LocalizedName("Grenada", "غرينادا")},
                new CountryEntity {Name = new LocalizedName("Guatemala", "غواتيمالا")},
                new CountryEntity {Name = new LocalizedName("Guinea", "غينيا")},
                new CountryEntity {Name = new LocalizedName("Guyana", "غيانا")},
                new CountryEntity {Name = new LocalizedName("Haiti", "هايتي")},
                new CountryEntity {Name = new LocalizedName("Holland", "هولندا")},
                new CountryEntity {Name = new LocalizedName("Honduras", "هندوراس")},
                new CountryEntity {Name = new LocalizedName("Hungary", "هنغاريا")},
                new CountryEntity {Name = new LocalizedName("Iceland", "أيسلندا")},
                new CountryEntity {Name = new LocalizedName("India", "الهند")},
                new CountryEntity {Name = new LocalizedName("Indonesia", "أندونيسيا")},
                new CountryEntity {Name = new LocalizedName("Iran", "إيران")},
                new CountryEntity {Name = new LocalizedName("Iraq", "العراق")},
                new CountryEntity {Name = new LocalizedName("Ireland, Republic of", "ايرلندا، جمهورية")},
                new CountryEntity {Name = new LocalizedName("Italy", "إيطاليا")},
                new CountryEntity {Name = new LocalizedName("Jamaica", "جامايكا")},
                new CountryEntity {Name = new LocalizedName("Japan", "اليابان")},
                new CountryEntity {Name = new LocalizedName("Jordan", "الأردن")},
                new CountryEntity {Name = new LocalizedName("Kazakhstan", "كازاخستان")},
                new CountryEntity {Name = new LocalizedName("Kenya", "كينيا")},
                new CountryEntity {Name = new LocalizedName("Kuwait", "الكويت")},
                new CountryEntity {Name = new LocalizedName("Laos", "لاوس")},
                new CountryEntity {Name = new LocalizedName("Latvia", "لاتفيا")},
                new CountryEntity {Name = new LocalizedName("Lebanon", "لبنان")},
                new CountryEntity {Name = new LocalizedName("Liberia", "ليبيريا")},
                new CountryEntity {Name = new LocalizedName("Libya", "ليبيا")},
                new CountryEntity {Name = new LocalizedName("Liechtenstein", "ليختنشتاين")},
                new CountryEntity {Name = new LocalizedName("Lithuania", "ليتوانيا")},
                new CountryEntity {Name = new LocalizedName("Macedonia", "مقدونيا")},
                new CountryEntity {Name = new LocalizedName("Madagascar", "مدغشقر")},
                new CountryEntity {Name = new LocalizedName("Malawi", "ملاوي")},
                new CountryEntity {Name = new LocalizedName("Malaysia", "ماليزيا")},
                new CountryEntity {Name = new LocalizedName("Maldives", "جزر المالديف")},
                new CountryEntity {Name = new LocalizedName("Mali", "مالي")},
                new CountryEntity {Name = new LocalizedName("Malta", "مالطا")},
                new CountryEntity {Name = new LocalizedName("Mauritania", "موريتانيا")},
                new CountryEntity {Name = new LocalizedName("Mauritius", "موريشيوس")},
                new CountryEntity {Name = new LocalizedName("Mexico", "المكسيك")},
                new CountryEntity {Name = new LocalizedName("Moldova", "مولدوفا")},
                new CountryEntity {Name = new LocalizedName("Monaco", "موناكو")},
                new CountryEntity {Name = new LocalizedName("Mongolia", "منغوليا")},
                new CountryEntity {Name = new LocalizedName("Montenegro", "الجبل الأسود")},
                new CountryEntity {Name = new LocalizedName("Morocco", "المغرب")},
                new CountryEntity {Name = new LocalizedName("Mozambique", "موزمبيق")},
                new CountryEntity {Name = new LocalizedName("Myanmar see Burma", "ميانمار نرى بورما")},
                new CountryEntity {Name = new LocalizedName("Namibia", "ناميبيا")},
                new CountryEntity {Name = new LocalizedName("Nepal", "نيبال")},
                new CountryEntity {Name = new LocalizedName("New Zealand", "نيوزيلاندا")},
                new CountryEntity {Name = new LocalizedName("Nicaragua", "نيكاراغوا")},
                new CountryEntity {Name = new LocalizedName("Niger", "النيجر")},
                new CountryEntity {Name = new LocalizedName("Nigeria", "نيجيريا")},
                new CountryEntity {Name = new LocalizedName("North Korea", "كوريا الشمالية")},
                new CountryEntity {Name = new LocalizedName("Norway", "النرويج")},
                new CountryEntity {Name = new LocalizedName("Oman", "سلطنة عمان")},
                new CountryEntity {Name = new LocalizedName("Pakistan", "باكستان")},
                new CountryEntity {Name = new LocalizedName("Panama", "بناما")},
                new CountryEntity {Name = new LocalizedName("Papua New Guinea", "بابوا غينيا الجديدة")},
                new CountryEntity {Name = new LocalizedName("Paraguay", "باراغواي")},
                new CountryEntity {Name = new LocalizedName("Peru", "بيرو")},
                new CountryEntity {Name = new LocalizedName("the Philippines", "الفلبينيين")},
                new CountryEntity {Name = new LocalizedName("Poland", "بولندا")},
                new CountryEntity {Name = new LocalizedName("Portugal", "البرتغال")},
                new CountryEntity {Name = new LocalizedName("Qatar", "قطر")},
                new CountryEntity {Name = new LocalizedName("Romania", "رومانيا")},
                new CountryEntity {Name = new LocalizedName("Russia", "روسيا")},
                new CountryEntity {Name = new LocalizedName("Rwanda", "رواندا")},
                new CountryEntity {Name = new LocalizedName("Saudi Arabia", "المملكة العربية السعودية")},
                new CountryEntity {Name = new LocalizedName("Scotland", "أسكتلندا")},
                new CountryEntity {Name = new LocalizedName("Senegal", "السنغال")},
                new CountryEntity {Name = new LocalizedName("Serbia", "صربيا")},
                new CountryEntity {Name = new LocalizedName("Seychelles, the", "سيشيل، و")},
                new CountryEntity {Name = new LocalizedName("Sierra Leone", "سيرا ليون")},
                new CountryEntity {Name = new LocalizedName("Singapore", "سنغافورة")},
                new CountryEntity {Name = new LocalizedName("Slovakia", "سلوفاكيا")},
                new CountryEntity {Name = new LocalizedName("Slovenia", "سلوفينيا")},
                new CountryEntity {Name = new LocalizedName("Solomon Islands", "جزر سليمان")},
                new CountryEntity {Name = new LocalizedName("Somalia", "الصومال")},
                new CountryEntity {Name = new LocalizedName("South Africa", "جنوب أفريقيا")},
                new CountryEntity {Name = new LocalizedName("South Korea", "كوريا الجنوبية")},
                new CountryEntity {Name = new LocalizedName("Spain", "إسبانيا")},
                new CountryEntity {Name = new LocalizedName("Sri Lanka", "سيريلانكا")},
                new CountryEntity {Name = new LocalizedName("Sudan", "سودان")},
                new CountryEntity {Name = new LocalizedName("Suriname", "سورينام")},
                new CountryEntity {Name = new LocalizedName("Swaziland", "سوازيلاند")},
                new CountryEntity {Name = new LocalizedName("Sweden", "السويد")},
                new CountryEntity {Name = new LocalizedName("Switzerland", "سويسرا")},
                new CountryEntity {Name = new LocalizedName("Syria", "سوريا")},
                new CountryEntity {Name = new LocalizedName("Taiwan", "تايوان")},
                new CountryEntity {Name = new LocalizedName("Tajikistan", "طاجيكستان")},
                new CountryEntity {Name = new LocalizedName("Tanzania", "تنزانيا")},
                new CountryEntity {Name = new LocalizedName("Thailand", "تايلاند")},
                new CountryEntity {Name = new LocalizedName("Togo", "توغو")},
                new CountryEntity {Name = new LocalizedName("Trinidad and Tobago", "ترينداد وتوباغو")},
                new CountryEntity {Name = new LocalizedName("Tunisia", "تونس")},
                new CountryEntity {Name = new LocalizedName("Turkey", "ديك رومي")},
                new CountryEntity {Name = new LocalizedName("Turkmenistan", "تركمانستان")},
                new CountryEntity {Name = new LocalizedName("Tuvalu", "توفالو")},
                new CountryEntity {Name = new LocalizedName("Uganda", "أوغندا")},
                new CountryEntity {Name = new LocalizedName("Ukraine", "أوكرانيا")},
                new CountryEntity {Name = new LocalizedName("United Arab Emirates", "الإمارات العربية المتحدة")},
                new CountryEntity {Name = new LocalizedName("United Kingdom", "المملكة المتحدة")},
                new CountryEntity {Name = new LocalizedName("United States of America", "الولايات المتحدة")},
                new CountryEntity {Name = new LocalizedName("Uruguay", "أوروغواي")},
                new CountryEntity {Name = new LocalizedName("Uzbekistan", "أوزبكستان")},
                new CountryEntity {Name = new LocalizedName("Vanuatu", "فانواتو")},
                new CountryEntity {Name = new LocalizedName("Vatican City", "مدينة الفاتيكان")},
                new CountryEntity {Name = new LocalizedName("Venezuela", "فنزويلا")},
                new CountryEntity {Name = new LocalizedName("Vietnam", "فيتنام")},
                new CountryEntity {Name = new LocalizedName("Wales", "ويلز")},
                new CountryEntity {Name = new LocalizedName("Western Samoa", "ساموا الغربية")},
                new CountryEntity {Name = new LocalizedName("Yemen", "اليمن")},
                new CountryEntity {Name = new LocalizedName("Yugoslavia", "يوغوسلافيا")},
                new CountryEntity {Name = new LocalizedName("Zaire", "زائير")},
                new CountryEntity {Name = new LocalizedName("Zambia", "زامبيا")},
                new CountryEntity {Name = new LocalizedName("Zimbabwe", "زيمبابوي")}
            };
        }
    }
}