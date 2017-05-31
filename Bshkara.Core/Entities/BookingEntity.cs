using System;
using System.ComponentModel.DataAnnotations.Schema;
using Bashkra.Shared.Enums;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// <see cref="BookingEntity" />
    /// </summary>
    [Table("Bookings")]
    public class BookingEntity : AuditedEntity
    {
        /// <summary>
        /// <c>Maid</c> code
        /// </summary>
        public Guid MaidId { get; set; }

        /// <summary>
        /// <c>Maid object</c>
        /// </summary>
        [ForeignKey("MaidId")]
        public virtual MaidEntity Maid { get; set; }

        /// <summary>
        /// <c>User</c> code
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// <c>User</c>
        /// </summary>
        [ForeignKey("UserId")]
        public virtual UserEntity User { get; set; }

        /// <summary>
        /// <c>Booking</c> reference
        /// </summary>
        public string BookingRef { get; set; }

        /// <summary>
        /// <c>Booking</c> status
        /// </summary>
        public BookingStatus BookingStatus { get; set; }

        /// <summary>
        /// <c>Notes</c>
        /// </summary>
        public string Notes { get; set; }
    }
}