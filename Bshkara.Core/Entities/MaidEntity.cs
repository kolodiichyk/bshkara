using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Bashkra.Shared.Enums;
using Bshkara.Core.Base;
using Bshkara.Core.Localization;

namespace Bshkara.Core.Entities
{
    /// <summary>
    ///     <see cref="MaidEntity" /> profile
    /// </summary>
    [Table("Maids")]
    public class MaidEntity : AuditedDictionary
    {
        public MaidEntity()
        {
            Documents = new List<MaidDocumentEntity>();
            EmploymentHistory = new List<MaidEmploymentHistoryEntity>();
            Languages = new List<MaidLanguageEntity>();
            Skills = new List<MaidSkillEntity>();
            Note = new LocalizedName(null, null);

            Passport = new MaidPassportDetailEntity();
        }

        /// <summary>
        ///     Date of birthday
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
                 "{0:yyyy-MM-dd}",
             ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        ///     <c>Maid</c> marital status
        /// </summary>
        public MaritalStatus MaritalStatus { get; set; }

        /// <summary>
        ///     Number of maid children
        /// </summary>
        public int NoOfChildren { get; set; }

        /// <summary>
        ///     <c>Maid</c> weight
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        ///     <c>Maid</c> height
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        ///     <c>Maid</c> religion
        /// </summary>
        public Religion Religion { get; set; }

        /// <summary>
        ///     Mail gender
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        ///     <c>Maid</c> education
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        ///     Number of years Of experience
        /// </summary>
        public int YearsOfExperience { get; set; }

        /// <summary>
        ///     <c>Maid</c> phone number
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     <c>Maid</c> address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     <c>Maid</c> expected salary
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        ///     Is maid available now
        /// </summary>
        public bool Availability { get; set; }

        /// <summary>
        ///     <c>Maid</c> photo
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        ///     <c>Description</c>
        /// </summary>
        public LocalizedName Note { get; set; }

        /// <summary>
        ///     <see cref="Bshkara.Core.Entities.MaidEntity.Agency" /> code where
        ///     maid belongs
        /// </summary>
        public Guid? AgencyId { get; set; }

        /// <summary>
        ///     <see cref="Bshkara.Core.Entities.MaidEntity.Agency" /> where maid
        ///     belongs
        /// </summary>
        [ForeignKey("AgencyId")]
        public virtual AgencyEntity Agency { get; set; }

        public Guid? LivingCityId { get; set; }

        [ForeignKey("LivingCityId")]
        public virtual CityEntity LivingCity { get; set; }


        /// <summary>
        ///     Maid passport details
        /// </summary>
        public virtual MaidPassportDetailEntity Passport { get; set; }

        /// <summary>
        ///     Visa status code
        /// </summary>
        public Guid? VisaStatusId { get; set; }

        /// <summary>
        ///     Visa status
        /// </summary>
        [ForeignKey("VisaStatusId")]
        [Display(Name = "Maids_VisaStatus", ResourceType = typeof(Res))]
        public virtual VisaStatusEntity VisaStatus { get; set; }

        /// <summary>
        ///     <c>Nationality</c> code
        /// </summary>
        public Guid? NationalityId { get; set; }

        /// <summary>
        ///     <see cref="Bshkara.Core.Entities.MaidEntity.Nationality" />
        /// </summary>
        [ForeignKey("NationalityId")]
        public virtual NationalityEntity Nationality { get; set; }

        public Guid? AgencyPackageId { get; set; }

        [ForeignKey("AgencyPackageId")]
        public virtual AgencyPackageEntity AgencyPackage { get; set; }

        /// <summary>
        ///     Maid skills
        /// </summary>
        public virtual List<MaidSkillEntity> Skills { get; set; }

        /// <summary>
        ///     Maid languages
        /// </summary>
        public virtual List<MaidLanguageEntity> Languages { get; set; }

        /// <summary>
        ///     Maid employment history
        /// </summary>
        public virtual List<MaidEmploymentHistoryEntity> EmploymentHistory { get; set; }

        /// <summary>
        ///     Maid documents
        /// </summary>
        public virtual List<MaidDocumentEntity> Documents { get; set; }

        public int Age()
        {
            // Save today's date.
            var today = DateTime.Today;

            // Calculate the age.
            if (DateOfBirth != null)
            {
                var age = today.Year - DateOfBirth.Value.Year;
                return age;
            }

            return 0;
        }

        public string GetSkills()
        {
            return string.Join(", ", Skills.Select(t => t.Skill.Name.Default).OrderBy(t => t));
        }
    }
}