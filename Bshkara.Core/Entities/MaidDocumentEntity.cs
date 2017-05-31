using System;
using System.ComponentModel.DataAnnotations.Schema;
using Bshkara.Core.Base;

namespace Bshkara.Core.Entities
{
    /// <summary>
    /// Maid Document files
    /// </summary>
    [Table("MaidDocuments")]
    public class MaidDocumentEntity : AuditedEntity
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
        /// Path to file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// <c>File</c> type
        /// </summary>
        public Guid DocumentTypeEntityId { get; set; }

        /// <summary>
        /// <c>File</c> type
        /// </summary>
        [ForeignKey("DocumentTypeEntityId")]
        public virtual DocumentTypeEntity DocumentType { get; set; }

        /// <summary>
        /// <c>File</c> format
        /// </summary>
        public string DocumentFormat { get; set; }
    }
}