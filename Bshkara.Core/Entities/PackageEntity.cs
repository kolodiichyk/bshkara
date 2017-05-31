using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;
using Bshkara.Core.Localization;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// <see cref="PackageEntity" />
    /// </summary>
    [Table("Packages")]
    public class PackageEntity : AuditedDictionary
    {
        [Required]
        [Display(Name = "Package_Description", ResourceType = typeof (Res))]
        public LocalizedName Description { get; set; }

        [Display(Name = "Package_UsersCount", ResourceType = typeof (Res))]
        [Required]
        [Range(0, int.MaxValue, ErrorMessageResourceName = "Validation_PositiveNumbers",
            ErrorMessageResourceType = typeof (Res))]
        public int UsersCount { get; set; }

        [Display(Name = "Package_ListingCount", ResourceType = typeof (Res))]
        [Required]
        [Range(0, int.MaxValue, ErrorMessageResourceName = "Validation_PositiveNumbers",
            ErrorMessageResourceType = typeof (Res))]
        public int ListingCount { get; set; }

        [Display(Name = "Package_Duration", ResourceType = typeof (Res))]
        [Required]
        [Range(0, int.MaxValue, ErrorMessageResourceName = "Validation_PositiveNumbers",
            ErrorMessageResourceType = typeof (Res))]
        public int Duration { get; set; }


        [Display(Name = "Package_Price", ResourceType = typeof (Res))]
        [Required]
        [Range(0, double.MaxValue, ErrorMessageResourceName = "Validation_PositiveNumbers",
            ErrorMessageResourceType = typeof (Res))]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}