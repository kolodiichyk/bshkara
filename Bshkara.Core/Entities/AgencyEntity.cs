using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;
using Bshkara.Core.Localization;

namespace Bshkara.Core.Entities
{
    /// <summary>
    ///     Agency
    /// </summary>
    [Table("Agencies")]
    public class AgencyEntity : AuditedDictionary
    {
        public AgencyEntity()
        {
            Address = new LocalizedName(string.Empty, string.Empty);
        }

        /// <summary>
        ///     <c>City</c> code
        /// </summary>
        [Display(Name = "Agency_City", ResourceType = typeof(Res))]
        public Guid? CityId { get; set; }

        /// <summary>
        ///     <c>City object</c>
        /// </summary>
        [ForeignKey("CityId")]
        [Display(Name = "Agency_City", ResourceType = typeof(Res))]
        public virtual CityEntity City { get; set; }

        /// <summary>
        ///     <see cref="Bshkara.Core.Entities.AgencyEntity.Logo" /> of agency
        /// </summary>
        [Display(Name = "Agency_Logo", ResourceType = typeof(Res))]
        public string Logo { get; set; }

        /// <summary>
        ///     Trade license number of agency
        /// </summary>
        [Display(Name = "Agency_TradeLicenseNumber", ResourceType = typeof(Res))]
        public string TradeLicenseNumber { get; set; }

        /// <summary>
        ///     Agency web site
        /// </summary>
        [Display(Name = "Agency_Website", ResourceType = typeof(Res))]
        public string Website { get; set; }

        /// <summary>
        ///     Agency email
        /// </summary>
        [Display(Name = "Agency_Email", ResourceType = typeof(Res))]
        public string Email { get; set; }

        /// <summary>
        ///     Agency address
        /// </summary>
        [Display(Name = "Agency_Address", ResourceType = typeof(Res))]
        public LocalizedName Address { get; set; }

        /// <summary>
        ///     Agency phone number
        /// </summary>
        [Display(Name = "Agency_Phone", ResourceType = typeof(Res))]
        public string Phone { get; set; }

        /// <summary>
        ///     Agency mobile phone number
        /// </summary>
        [Display(Name = "Agency_Mobile", ResourceType = typeof(Res))]
        public string Mobile { get; set; }

        /// <summary>
        ///     <c> Packages </c>
        /// </summary>
        public List<AgencyPackageEntity> Packages { get; set; }
    }
}