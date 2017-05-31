using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bashkra.ApiClient.Models;
using Bashkra.ApiClient.Responses;
using Bashkra.Shared.Enums;

namespace Bashkra.ApiClient
{
    public static class MockDataHelper
    {
        public static async Task<ApiResponse> GetSuccessApiResponse()
        {
            return await Task.FromResult(new ApiResponse
            {
                Error = null,
                Lang = Language.En,
                ExecutionTime = default(TimeSpan),
                ServerTimestamp = default(double)
            });
        }

        public static async Task<SignInApiResponse> GetAuthSignIn(string email, string password)
        {
            return await Task.FromResult(new SignInApiResponse
            {
                Error = null,
                Lang = Language.En,
                ExecutionTime = default(TimeSpan),
                ServerTimestamp = default(double),
                AccessToken = Guid.NewGuid().ToString(),
                SignInStatus = email == "admin@bashkra.com" && password == "bashkra"
                    ? SignInStatus.Success
                    : SignInStatus.InvalidLoginOrPassword,
                User = new ApiUser {Id = Guid.NewGuid(), Name = "Admin", Mobile = "+380671572417"}
            });
        }

        public static async Task<ApiResponse> GetSuccessAuthSignUp(string email, string password)
        {
            return await GetSuccessApiResponse();
        }

        public static async Task<MaidsApiResponse> GetMaids(string accessToken)
        {
            return await Task.FromResult(new MaidsApiResponse
            {
                Error = null,
                Lang = Language.En,
                ExecutionTime = default(TimeSpan),
                ServerTimestamp = default(double),
                Maids = new List<ApiMaid>
                {
                    new ApiMaid
                    {
                        Id = Guid.NewGuid(),
                        Name = "Angela Johnson",
                        DateOfBirth = DateTime.Parse("01.02.1981"),
                        Address = "77 Middle Hillgate, Stockport SK1 3EH, GB",
                        Availability = true,
                        Salary = 2100,
                        Religion = Religion.Christian,
                        Nationality = GetNationality()[0],
                        Photo =
                            "http://1.bp.blogspot.com/-_EbbnAAjnqM/TcafiRb_FfI/AAAAAAAAAGY/JdJC8L9L9I4/s1600/531_AM-395.jpg",
                        Note =
                            "She seems hardworking, trustworthy, and honest. She is a mother of 3 kids. She loves kids. Her salary expectation is ",
                        Skills = new List<ApiMaidSkill>
                        {
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[0],
                                SkillLevel = Level.Little
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[1],
                                SkillLevel = Level.Fair
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[2],
                                SkillLevel = Level.Fulent
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[3],
                                SkillLevel = Level.Poor
                            }
                        },
                        Education = "Birkbeck, University of London",
                        EmploymentHistory = new List<ApiMaidEmploymentHistory>
                        {
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "Maria was required to provide her work history for the last 10 years of employment when applying for a job with ABC Company."
                            },
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "The ABD employment application requests five years of work history."
                            }
                        },
                        Documents = new List<ApiMaidDocument>(),
                        Languages = new List<ApiMaidLanguage>
                        {
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[0],
                                SpokenLevel = Level.Fulent,
                                ReadLevel = Level.Little,
                                WrittenLevel = Level.Poor
                            },
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[1],
                                SpokenLevel = Level.Little,
                                ReadLevel = Level.Poor,
                                WrittenLevel = Level.Poor
                            }
                        }
                    },
                    new ApiMaid
                    {
                        Id = Guid.NewGuid(),
                        Name = "Anna Kurachi",
                        DateOfBirth = DateTime.Parse("10.10.1986"),
                        Address = "B9008, Ballindalloch AB37 9JA, , GB",
                        Availability = false,
                        Salary = 2300,
                        Religion = Religion.Islam,
                        Nationality = GetNationality()[1],
                        Photo =
                            "http://www.tmclife.com/images/stories/edw_0033_passport%20size.jpg",
                        Note = "She is a mother of 3 kids. She seems hardworking and loyal and trustworthy.",
                        Skills = new List<ApiMaidSkill>
                        {
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[4],
                                SkillLevel = Level.Fulent
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[2],
                                SkillLevel = Level.Poor
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[0],
                                SkillLevel = Level.Fair
                            }
                        },
                        Education = "Royal Holloway, University of London",
                        EmploymentHistory = new List<ApiMaidEmploymentHistory>
                        {
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "Maria was required to provide her work history for the last 10 years of employment when applying for a job with ABC Company."
                            },
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "The ABD employment application requests five years of work history."
                            }
                        },
                        Documents = new List<ApiMaidDocument>(),
                        Languages = new List<ApiMaidLanguage>
                        {
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[0],
                                SpokenLevel = Level.Little,
                                ReadLevel = Level.Poor,
                                WrittenLevel = Level.Little
                            },
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[1],
                                SpokenLevel = Level.Fulent,
                                ReadLevel = Level.Fulent,
                                WrittenLevel = Level.Fulent
                            },
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[2],
                                SpokenLevel = Level.Little,
                                ReadLevel = Level.Poor,
                                WrittenLevel = Level.Little
                            }
                        }
                    },
                    new ApiMaid
                    {
                        Id = Guid.NewGuid(),
                        Name = "Ligaya Arnesto",
                        DateOfBirth = DateTime.Parse("11.12.1975"),
                        Address = "Located at Dubai",
                        Availability = true,
                        Salary = 1700,
                        Religion = Religion.Christian,
                        Nationality = GetNationality()[2],
                        Photo =
                            "http://www.maidcv.com/Uploads/CV/Photo/8cc580d8ab4e72eb4fc0bcd300a1b84910492.png",
                        Note =
                            "She is a well-experienced housemaid/nanny. She finishes all her contracts. She seems hardworking and trustworthy. She loves to be with expats family (western family)",
                        Skills = new List<ApiMaidSkill>
                        {
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[0],
                                SkillLevel = Level.Little
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[1],
                                SkillLevel = Level.Fair
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[2],
                                SkillLevel = Level.Fair
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[4],
                                SkillLevel = Level.Fulent
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[5],
                                SkillLevel = Level.Poor
                            }
                        },
                        Education = "Angeles City High School",
                        EmploymentHistory = new List<ApiMaidEmploymentHistory>
                        {
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "Maria was required to provide her work history for the last 10 years of employment when applying for a job with ABC Company."
                            },
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "The ABD employment application requests five years of work history."
                            }
                        },
                        Documents = new List<ApiMaidDocument>(),
                        Languages = new List<ApiMaidLanguage>
                        {
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[0],
                                SpokenLevel = Level.Fulent,
                                ReadLevel = Level.Fair,
                                WrittenLevel = Level.Fair
                            },
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[1],
                                SpokenLevel = Level.Fair,
                                ReadLevel = Level.Poor,
                                WrittenLevel = Level.Little
                            },
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[2],
                                SpokenLevel = Level.Little,
                                ReadLevel = Level.Fair,
                                WrittenLevel = Level.Little
                            }
                        }
                    },
                    new ApiMaid
                    {
                        Id = Guid.NewGuid(),
                        Name = "Estella Peradillo ",
                        DateOfBirth = DateTime.Parse("01.12.1995"),
                        Address = "77 Middle Hillgate, Stockport SK1 3EH, GB",
                        Availability = true,
                        Salary = 1900,
                        Religion = Religion.Hinduism,
                        Nationality = GetNationality()[0],
                        Photo =
                            "http://www.maidcv.com/Uploads/CV/Photo/80ae3262ffe32a72c71026dcf1e229656312.png",
                        Note =
                            "She seem's a hardworking and flexible at work. She has a little bit idea on how to cook arabic foods.",
                        Skills = new List<ApiMaidSkill>
                        {
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[0],
                                SkillLevel = Level.Little
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[3],
                                SkillLevel = Level.Poor
                            }
                        },
                        Education = "Mambago V. National High School",
                        EmploymentHistory = new List<ApiMaidEmploymentHistory>
                        {
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "Maria was required to provide her work history for the last 10 years of employment when applying for a job with ABC Company."
                            },
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "The ABD employment application requests five years of work history."
                            }
                        },
                        Documents = new List<ApiMaidDocument>(),
                        Languages = new List<ApiMaidLanguage>
                        {
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[0],
                                SpokenLevel = Level.Fulent,
                                ReadLevel = Level.Fair,
                                WrittenLevel = Level.Little
                            },
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[1],
                                SpokenLevel = Level.Little,
                                ReadLevel = Level.Fair,
                                WrittenLevel = Level.Poor
                            }
                        }
                    },
                    new ApiMaid
                    {
                        Id = Guid.NewGuid(),
                        Name = "Renilda Catig",
                        DateOfBirth = DateTime.Parse("10.10.1956"),
                        Address = "B9008, Ballindalloch AB37 9JA, , GB",
                        Availability = false,
                        Salary = 5300,
                        Religion = Religion.Christian,
                        Nationality = GetNationality()[1],
                        Photo =
                            "http://www.maidcv.com/Uploads/CV/Photo/69d80ab5635e150bdde9d5987aa2c6d611528.png",
                        Note =
                            "She can take care of kids well because she has 4 children of her own. She loves kids. She is well experience being a housemaid. She seems hardworking and honest.",
                        Skills = new List<ApiMaidSkill>
                        {
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[4],
                                SkillLevel = Level.Fulent
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[2],
                                SkillLevel = Level.Poor
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[0],
                                SkillLevel = Level.Fair
                            }
                        },
                        Education = "Royal Holloway, University of London",
                        EmploymentHistory = new List<ApiMaidEmploymentHistory>
                        {
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "Maria was required to provide her work history for the last 10 years of employment when applying for a job with ABC Company."
                            },
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "The ABD employment application requests five years of work history."
                            }
                        },
                        Documents = new List<ApiMaidDocument>(),
                        Languages = new List<ApiMaidLanguage>
                        {
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[0],
                                SpokenLevel = Level.Little,
                                ReadLevel = Level.Little,
                                WrittenLevel = Level.Poor
                            },
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[1],
                                SpokenLevel = Level.Fair,
                                ReadLevel = Level.Fair,
                                WrittenLevel = Level.Fair
                            },
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[2],
                                SpokenLevel = Level.Fulent,
                                ReadLevel = Level.Fair,
                                WrittenLevel = Level.Fulent
                            }
                        }
                    },
                    new ApiMaid
                    {
                        Id = Guid.NewGuid(),
                        Name = "Proscovia Nambajjo",
                        DateOfBirth = DateTime.Parse("10.10.1986"),
                        Address = "B9008, Ballindalloch AB37 9JA, , GB",
                        Availability = false,
                        Salary = 1300,
                        Religion = Religion.Christian,
                        Nationality = GetNationality()[2],
                        Photo =
                            "http://www.maidcv.com/Uploads/CV/Photo/9e567f170f3927ace158c802ca1ffe212847.png",
                        Note = "She is a mother so she can take care of kids well. She seems hardworking and honest.",
                        Skills = new List<ApiMaidSkill>
                        {
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[4],
                                SkillLevel = Level.Fulent
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[2],
                                SkillLevel = Level.Poor
                            },
                            new ApiMaidSkill
                            {
                                Id = Guid.NewGuid(),
                                Skill = GetSkills()[0],
                                SkillLevel = Level.Fair
                            }
                        },
                        Education = "Royal Holloway, University of London",
                        EmploymentHistory = new List<ApiMaidEmploymentHistory>
                        {
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "Maria was required to provide her work history for the last 10 years of employment when applying for a job with ABC Company."
                            },
                            new ApiMaidEmploymentHistory
                            {
                                Id = Guid.NewGuid(),
                                Country = GetCountries()[3],
                                Descripion =
                                    "The ABD employment application requests five years of work history."
                            }
                        },
                        Documents = new List<ApiMaidDocument>(),
                        Languages = new List<ApiMaidLanguage>
                        {
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[0],
                                SpokenLevel = Level.Poor,
                                ReadLevel = Level.Little,
                                WrittenLevel = Level.Fair
                            },
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[1],
                                SpokenLevel = Level.Fulent,
                                ReadLevel = Level.Fair,
                                WrittenLevel = Level.Little
                            },
                            new ApiMaidLanguage
                            {
                                Id = Guid.NewGuid(),
                                Language = GetLanguages()[2],
                                SpokenLevel = Level.Little,
                                ReadLevel = Level.Fair,
                                WrittenLevel = Level.Fulent
                            }
                        }
                    }
                }
            });
        }

        private static List<ApiSkill> GetSkills(Language lan = Language.En)
        {
            return new List<ApiSkill>
            {
                new ApiSkill
                {
                    Id = Guid.Parse("05f97c72-ea2b-4894-b0f2-e98600d077f3"),
                    Name = lan == Language.En ? "Cocking" : "رد ديك البندقية",
                    Icon = "https://s13.postimg.org/nulqptp8n/knife_and_fork.png"
                },
                new ApiSkill
                {
                    Id = Guid.Parse("35762fef-df9f-4823-84d4-0736a0eb5562"),
                    Name = lan == Language.En ? "Washing" : "غسل",
                    Icon = "https://s10.postimg.org/9gvgga4hl/washing.png"
                },
                new ApiSkill
                {
                    Id = Guid.Parse("84a8a7fd-8582-46b7-ab66-2f93130f7999"),
                    Name = lan == Language.En ? "Adult Care" : "العناية الكبار",
                    Icon = "https://s9.postimg.org/w21hny6kf/old_man_from_frontal_view_with_a_cane.png"
                },
                new ApiSkill
                {
                    Id = Guid.Parse("a594e724-09c6-4e94-8803-f31020ac915b"),
                    Name = lan == Language.En ? "Driving" : "القيادة",
                    Icon = "https://s13.postimg.org/xv80zr9dj/driving.png"
                },
                new ApiSkill
                {
                    Id = Guid.Parse("3479b467-45fd-4ab4-b562-0d8156cb3c98"),
                    Name = lan == Language.En ? "House cleaning" : "تنظيف المنزل",
                    Icon = "https://s11.postimg.org/d7rfd45j7/vacuum.png"
                },
                new ApiSkill
                {
                    Id = Guid.Parse("0713d048-fcbe-4f31-8c9d-ec69e5ee3394"),
                    Name = lan == Language.En ? "Babysitting" : "مجالسة الأطفال",
                    Icon = "https://s16.postimg.org/g015pt39h/babysitting.png"
                },
                new ApiSkill
                {
                    Id = Guid.Parse("05f97c72-ea2b-4894-b0f2-e98600d077f3"),
                    Name = lan == Language.En ? "Pet Care" : "رعاية الحيوانات الاليفة",
                    Icon = "https://s9.postimg.org/ssaj6p1f3/walking_the_dog.png"
                },
                new ApiSkill
                {
                    Id = Guid.Parse("b1271d48-0e22-42b0-bfee-fce551d73151"),
                    Name = lan == Language.En ? "Decorating" : "تزيين",
                    Icon = "https://s14.postimg.org/el7zoba7l/lamp.png"
                },
                new ApiSkill
                {
                    Id = Guid.Parse("b1271d48-0e22-42b0-bfee-fce551d73151"),
                    Name = lan == Language.En ? "Education" : "التعليم",
                    Icon = "https://s10.postimg.org/eyp8y31c9/education.png"
                }
            };
        }

        private static List<ApiCountry> GetCountries(Language lan = Language.En)
        {
            return new List<ApiCountry>
            {
                new ApiCountry
                {
                    Id = Guid.Parse("43818cc4-6242-4efd-97ba-8bfd13821d43"),
                    Name = lan == Language.En ? "England" : "إنكلترا",
                    CountryCode = "+44"
                },
                new ApiCountry
                {
                    Id = Guid.Parse("43818cc4-6242-4efd-97ba-8bfd13821d43"),
                    Name = lan == Language.En ? "United States of America" : "الولايات المتحدة",
                    CountryCode = "+1"
                },
                new ApiCountry
                {
                    Id = Guid.Parse("bb1511fe-eefd-4d20-bb0c-a0121d254a22"),
                    Name = lan == Language.En ? "United Arab Emirates" : "الإمارات العربية المتحدة",
                    CountryCode = "+971"
                },
                new ApiCountry
                {
                    Id = Guid.Parse("823b14f0-d03e-4b62-b958-8bce1efe1e6c"),
                    Name = lan == Language.En ? "India" : "الهند",
                    CountryCode = "+91"
                },
                new ApiCountry
                {
                    Id = Guid.Parse("e3da5bce-9623-4bba-89ee-69b52a19b77c"),
                    Name = lan == Language.En ? "Iran" : "إيران",
                    CountryCode = "+98"
                }
            };
        }

        private static List<ApiLanguage> GetLanguages(Language lan = Language.En)
        {
            return new List<ApiLanguage>
            {
                new ApiLanguage
                {
                    Id = Guid.Parse("a8d82fb8-aa64-4357-b63c-44ecae2c579f"),
                    Name = "English",
                    ShortName = "EN"
                },
                new ApiLanguage
                {
                    Id = Guid.Parse("95c7a05e-9b64-433f-8bd8-386d7083d26b"),
                    Name = "Arabic",
                    ShortName = "AR"
                },
                new ApiLanguage
                {
                    Id = Guid.Parse("423c1159-4357-40cc-b8c7-085654844ec1"),
                    Name = "Indian",
                    ShortName = "IND"
                }
            };
        }

        private static List<ApiNationality> GetNationality(Language lan = Language.En)
        {
            return new List<ApiNationality>
            {
                new ApiNationality
                {
                    Id = Guid.Parse("a8d82fb8-aa64-1357-b63c-14ecae1c579f"),
                    Name = "Indonesia"
                },
                new ApiNationality
                {
                    Id = Guid.Parse("a3d82fb8-aa64-1357-b63c-14ecae1c579f"),
                    Name = "Filipino"
                },
                new ApiNationality
                {
                    Id = Guid.Parse("a8d85fb8-aa64-1357-b63c-14ecae1f579c"),
                    Name = "Cameronian"
                }
            };
        }
    }
}