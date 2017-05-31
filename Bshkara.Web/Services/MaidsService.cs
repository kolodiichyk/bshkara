using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Bashkra.ApiClient.Models;
using Bashkra.ApiClient.Requests;
using Bashkra.Shared.Enums;
using Bshkara.Core.Entities;
using Bshkara.Core.Services;
using Bshkara.Web.Extentions;
using Bshkara.Web.Helpers;
using Bshkara.Web.Models;
using Bshkara.Web.Services.Bases;
using Bshkara.Web.ViewModels;
using PagedList;

namespace Bshkara.Web.Services
{
    public class MaidsService : CRUDService<MaidEntity, MaidsViewModel>
    {
        public MaidsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override MaidEntity GetEntity(Guid id)
        {
            return UnitOfWork.Context.Set<MaidEntity>().Include(t => t.Passport).FirstOrDefault(t => t.Id == id);
        }

        public override MaidsViewModel GetViewModelForIndex(FilterArgs args)
        {
            var viewModel = new MaidsViewModel
            {
                Title = BshkaraRes.Maid_Title,
                SearchString = args.SearchString,
                PageSize = args.PageSize
            };

            var query = UnitOfWork.Repository<MaidEntity>().Query()
                .Include(x => x.Documents)
                .Include(x => x.EmploymentHistory)
                .Include(x => x.Languages)
                .Include(x => x.Skills)
                .Include(x => x.Passport)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy);

            if (!string.IsNullOrWhiteSpace(args.SearchString))
                switch (CultureHelper.GetCurrentNeutralCulture().ToLower())
                {
                    case "en":
                        query.Filter(x => x.Name.En.Contains(args.SearchString));
                        break;
                    case "ar":
                        query.Filter(x => x.Name.Ar.Contains(args.SearchString));
                        break;
                }

            var agency = HttpContext.Current.User.Identity.GetAgency();
            if (agency != null)
                query.Filter(x => x.AgencyId == agency.Id);

            query.Filter(x => x.IsDeleted == false);

            query.OrderBy(q => q.OrderBy(d => d.Name.En));

            int count;
            var items = query.GetPage(args.PageNumber, args.PageSize, out count);

            viewModel.Items = new StaticPagedList<MaidEntity>(items, args.PageNumber, args.PageSize, count);

            return viewModel;
        }

        public override string CanDeleteEntity(MaidEntity entity)
        {
            if (UnitOfWork.Repository<BookingEntity>().Query().Filter(x => x.MaidId == entity.Id).Count() > 0)
                return BshkaraRes.Languages_CantDeleteExistsInMaidLanguages;

            if (
                UnitOfWork.Repository<BookingEntity>()
                    .Query()
                    .Filter(x => x.MaidId == entity.Id)
                    .Count() > 0)
                return BshkaraRes.Maids_CantDeleteExistsInBooking;

            return string.Empty;
        }

        public override List<string> AutocompleteSearch(string key)
        {
            return
                UnitOfWork.Database.SqlQuery<string>(
                        $"select name{Lang} from maids where isDeleted = 0 and name{Lang} like N'%{key}%' order by name{Lang}")
                    .ToList();
        }


        public List<ApiMaid> GetMaidsForApi(MaidsArgs args, out int total)
        {
            var culture = CultureHelper.GetCurrentCulture();

            Expression<Func<MaidEntity, bool>> searchPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> idPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> maidsPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> skillPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> languagesPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> availabilityPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> agencyPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> genderPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> maritalStatusPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> nationalityPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> licingCityPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> maxSalaryPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> minSalaryPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> maxYearsOfExperiencePredicate = maid => true;

            Expression<Func<MaidEntity, bool>> minYearsOfExperiencePredicate = maid => true;

            Expression<Func<MaidEntity, bool>> visaStatusPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> religionPredicate = maid => true;

            Expression<Func<MaidEntity, bool>> picturePredicate = maid => true;

            Expression<Func<MaidEntity, bool>> notDeleted = maid => !maid.IsDeleted;

            Expression<Func<MaidEntity, bool>> notFormUnavaileblePackeges = maid =>
                maid.AgencyPackage != null &&
                maid.AgencyPackage.PackageStatus == PackageStatus.Active;

            if (args != null)
            {
                if (args.Id != null)
                    idPredicate = maid => maid.Id == args.Id;

                if (!string.IsNullOrWhiteSpace(args.Search))
                    if (culture == "ar")
                        searchPredicate = maid => maid.Name.Ar.Contains(args.Search);
                    else
                        searchPredicate = maid => maid.Name.En.Contains(args.Search);

                if (args.Maids.Any())
                    maidsPredicate = maid => args.Maids.Contains(maid.Id);

                if (args.Skills.Any())
                    skillPredicate =
                        maid =>
                            maid.Skills.Select(skill => skill.SkillId).Intersect(args.Skills).Count() ==
                            args.Skills.Count;

                if (args.Languages.Any())
                    languagesPredicate =
                        maid =>
                            maid.Languages.Select(language => language.LanguageId).Intersect(args.Languages).Count() ==
                            args.Languages.Count;

                if (args.Availability != null)
                    languagesPredicate =
                        maid => maid.Availability == args.Availability;

                if (args.Agency != null)
                    agencyPredicate =
                        maid => maid.AgencyId == args.Agency.Value;

                if (args.Gender != null)
                    genderPredicate =
                        maid => maid.Gender == args.Gender.Value;

                if (args.MaritalStatus != null)
                    maritalStatusPredicate =
                        maid => maid.MaritalStatus == args.MaritalStatus.Value;

                if (args.Nationality != null)
                    nationalityPredicate =
                        maid => maid.NationalityId == args.Nationality.Value;

                if (args.City != null)
                    licingCityPredicate = maid => maid.LivingCityId == args.City;

                if (args.MaxSalary != null)
                    maxSalaryPredicate =
                        maid => maid.Salary <= args.MaxSalary;

                if (args.MinSalary != null)
                    minSalaryPredicate =
                        maid => maid.Salary >= args.MinSalary;

                if (args.MaxYearsOfExperience != null)
                    maxYearsOfExperiencePredicate =
                        maid => maid.YearsOfExperience <= args.MaxYearsOfExperience;

                if (args.MinYearsOfExperience != null)
                    minYearsOfExperiencePredicate =
                        maid => maid.YearsOfExperience >= args.MinYearsOfExperience;

                if (args.VisaStatus != null)
                    visaStatusPredicate =
                        maid => maid.VisaStatusId == args.VisaStatus.Value;

                if (args.Religions.Any())
                    religionPredicate =
                        maid => args.Religions.Contains(maid.Religion);

                if (args.OnlyWithPhotos != null && args.OnlyWithPhotos.Value)
                    picturePredicate = maid => maid.Photo != null && maid.Photo != "";
            }

            total = UnitOfWork.Context.Set<MaidEntity>()
                .Where(idPredicate)
                .Where(searchPredicate)
                .Where(maidsPredicate)
                .Where(skillPredicate)
                .Where(languagesPredicate)
                .Where(availabilityPredicate)
                .Where(agencyPredicate)
                .Where(genderPredicate)
                .Where(maritalStatusPredicate)
                .Where(nationalityPredicate)
                .Where(licingCityPredicate)
                .Where(maxSalaryPredicate)
                .Where(minSalaryPredicate)
                .Where(maxYearsOfExperiencePredicate)
                .Where(minYearsOfExperiencePredicate)
                .Where(visaStatusPredicate)
                .Where(religionPredicate)
                .Where(picturePredicate)
                .Where(notDeleted)
                .Where(notFormUnavaileblePackeges)
                .Count();

            var maids =
                UnitOfWork.Context.Set<MaidEntity>()
                    .Include(maid => maid.LivingCity)
                    .Include(maid => maid.Documents)
                    .Include(maid => maid.EmploymentHistory)
                    .Include(maid => maid.Agency)
                    .Include(maid => maid.Nationality)
                    .Include(maid => maid.VisaStatus)
                    .Where(idPredicate)
                    .Where(searchPredicate)
                    .Where(maidsPredicate)
                    .Where(skillPredicate)
                    .Where(languagesPredicate)
                    .Where(availabilityPredicate)
                    .Where(agencyPredicate)
                    .Where(genderPredicate)
                    .Where(maritalStatusPredicate)
                    .Where(nationalityPredicate)
                    .Where(licingCityPredicate)
                    .Where(maxSalaryPredicate)
                    .Where(minSalaryPredicate)
                    .Where(maxYearsOfExperiencePredicate)
                    .Where(minYearsOfExperiencePredicate)
                    .Where(visaStatusPredicate)
                    .Where(religionPredicate)
                    .Where(picturePredicate)
                    .Where(notDeleted)
                    .Where(notFormUnavaileblePackeges)
                    .OrderBy(maid => maid.Id)
                    .Skip(args.Paging.PageNumber * args.Paging.PageSize)
                    .Take(args.Paging.PageSize == 0 ? total : args.Paging.PageSize)
                    .ToList();

            var list = maids.Select(maid => new ApiMaid
            {
                Id = maid.Id,
                Name = maid.Name?.Default,
                Availability = maid.Availability,
                Address = maid.Address,
                Note = maid.Note?.Default,
                Photo = maid.Photo,
                Salary = maid.Salary,
                DateOfBirth = maid.DateOfBirth,
                YearsOfExperience = maid.YearsOfExperience,
                Gender = maid.Gender,
                Religion = maid.Religion,
                MaritalStatus = maid.MaritalStatus,
                NoOfChildren = maid.NoOfChildren,
                Weight = maid.Weight,
                Height = maid.Height,
                LivingCity = maid.LivingCity != null
                    ? new ApiCity
                    {
                        Id = maid.LivingCity.Id,
                        Name = maid.LivingCity.Name?.Default,
                        CountryId = maid.LivingCity.CountryId,
                        Country = maid.LivingCity.Country != null
                            ? new ApiCountry
                            {
                                Id = maid.LivingCity.CountryId,
                                CountryCode = maid.LivingCity.Country.CountryCode,
                                Name = maid.LivingCity.Country.Name.Default
                            }
                            : null
                    }
                    : null,
                VisaStatus = maid.VisaStatus == null
                    ? null
                    : new ApiVisaStatus
                    {
                        Id = maid.VisaStatus.Id,
                        Name = maid.VisaStatus.Name?.Default
                    },
                Agency = maid.Agency == null
                    ? null
                    : new ApiAgency
                    {
                        Id = maid.Agency.Id,
                        Name = maid.Agency.Name?.Default,
                        City = maid.Agency.City != null
                            ? new ApiCity
                            {
                                Id = maid.Agency.City.Id,
                                Name = maid.Agency.City.Name?.Default,
                                CountryId = maid.Agency.City.CountryId,
                                Country = maid.Agency.City.Country != null
                                    ? new ApiCountry
                                    {
                                        Id = maid.Agency.City.CountryId,
                                        CountryCode = maid.Agency.City.Country.CountryCode,
                                        Name = maid.Agency.City.Country.Name.Default
                                    }
                                    : null
                            }
                            : null,
                        Logo = maid.Agency.Logo,
                        TradeLicenseNumber = maid.Agency.TradeLicenseNumber,
                        Email = maid.Agency.Email,
                        Website = maid.Agency.Website,
                        Mobile = maid.Agency.Mobile,
                        Address = maid.Agency.Address?.Default
                    },
                Nationality = maid.Nationality == null
                    ? null
                    : new ApiNationality
                    {
                        Id = maid.Nationality.Id,
                        Name = maid.Nationality.Name?.Default
                    },
                Skills = maid.Skills?.Select(skill => new ApiMaidSkill
                {
                    Id = skill.Id,
                    SkillLevel = skill.SkillLevel,
                    Skill = new ApiSkill
                    {
                        Id = skill.SkillId,
                        Name = skill.Skill.Name?.Default,
                        Icon = skill.Skill.Icon
                    }
                }).ToList(),
                Languages = maid.Languages?.Select(language => new ApiMaidLanguage
                {
                    Id = language.Id,
                    SpokenLevel = language.SpokenLevel,
                    ReadLevel = language.ReadLevel,
                    WrittenLevel = language.WrittenLevel,
                    Language = new ApiLanguage
                    {
                        Id = language.Language.Id,
                        Name = language.Language.Name?.Default,
                        ShortName = language.Language.ShortName
                    }
                }).ToList(),
                EmploymentHistory = maid.EmploymentHistory?.Select(employmentHistory => new ApiMaidEmploymentHistory
                {
                    Id = employmentHistory.Id,
                    Descripion = employmentHistory.Descripion?.Default,
                    Duration = employmentHistory.Duration,
                    Country = employmentHistory.Country != null
                        ? new ApiCountry
                        {
                            Id = employmentHistory.Country.Id,
                            CountryCode = employmentHistory.Country.CountryCode,
                            Name = employmentHistory.Country.Name.Default
                        }
                        : null
                }).ToList(),
                Documents = maid.Documents?.Select(document => new ApiMaidDocument
                {
                    Id = document.Id,
                    DocumentFormat = document.DocumentFormat,
                    File = document.FileName,
                    DocumentType = document.DocumentType != null
                        ? new ApiDocumentType
                        {
                            Id = document.DocumentType.Id,
                            Name = document.DocumentType.Name.Default,
                            Icon = document.DocumentType.Icon,
                            ShowAsPicture = document.DocumentType.ShowAsPicture
                        }
                        : null
                }).ToList()
            }).ToList();

            return list;
        }
    }
}