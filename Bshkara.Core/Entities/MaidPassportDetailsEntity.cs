using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// What languages maid know
    /// </summary>
    [Table("MaidPassportDetails")]
    public class MaidPassportDetailEntity : AuditedEntity
    {
        /// <summary>
        /// Number of maid passport
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        /// Passport issue date
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
      "{0:yyyy-MM-dd}",
       ApplyFormatInEditMode = true)]
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Passport expiration date
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString =
      "{0:yyyy-MM-dd}",
       ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Passport issue place
        /// </summary>
        public string IssuePlace { get; set; }
    }
}