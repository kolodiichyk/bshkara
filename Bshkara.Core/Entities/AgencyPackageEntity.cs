using System;
using System.ComponentModel.DataAnnotations.Schema;
using Bashkra.Shared.Enums;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// Agency package
    /// </summary>
    [Table("AgencyPackages")]
    public class AgencyPackageEntity : AuditedEntity
    {
        /// <summary>
        /// <c>Agency</c> code
        /// </summary>
        public Guid AgencyId { get; set; }

        /// <summary>
        /// <c>Agency</c>
        /// </summary>
        [ForeignKey("AgencyId")]
        public AgencyEntity Agency { get; set; }

        /// <summary>
        /// <c>Package code </c>
        /// </summary>
        public Guid PackageId { get; set; }

        /// <summary>
        /// <c>Package</c>
        /// </summary>
        [ForeignKey("PackageId")]
        public PackageEntity Package { get; set; }

        /// <summary>
        /// Status of package
        /// </summary>
        public PackageStatus PackageStatus { get; set; }
    }
}