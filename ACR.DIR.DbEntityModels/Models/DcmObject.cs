using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace ACR.DIR.DbEntityModels
{
    [Table("dcmObjects")]
    [Index(nameof(CbsObjectId), IsUnique = true, Name = "dcmObject_cbsObjectId_idx")]
    [Index(nameof(CreatedDateUtc), Name = "dcmObject_createdDateUtc_idx")]
    public class DcmObject
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("sopInstanceUid"), MaxLength(100)]
        public string? SopInstanceUid { get; set; }

        [Column("sopClassUid"), MaxLength(100)]
        public string? SopClassUid { get; set; }

        [Column("instanceNumber")]
        public int? InstanceNumber { get; set; }

        [Column("dcmSeriesId")]
        public long? DcmSeriesId { get; set; }

        [Column("attributes", TypeName = "JSON")]
        public string? Attributes { get; set; }

        [Column("sizeBytes")]
        public long? SizeBytes { get; set; }

        [Column("createdDateUtc")]
        public DateTime? CreatedDateUtc { get; set; }
        [Column("lastModifiedDateUtc")]
        public DateTime? LastModifiedDateUtc { get; set; }
        [Column("s3Path"), MaxLength(5000)]
        public string? S3Path { get; set; }

        [Column("dirTransactionId")]
        public long? DirTransactionId { get; set; } 

        [Column("cbsObjectId")]
        public long? CbsObjectId { get; set; }

        [Column("objectStatus")]
        public long ObjectStatus { get; set; }

        /// <summary>
        /// { Unknown = 0, Created = 1, Processed = 2, ProcessingFailed = 3, Duplicate = 4}
        /// </summary>
        [Column("objectStatusLogs")]
        public string? ObjectStatusLogs { get; set; } = string.Empty;
        [Column("reprocessAttemptId")]
        public long? ReprocessAttemptId { get; set; }
        [Column("reprocessAttemptCount")]
        public int ReprocessAttemptCount { get; set; } = 0;
    }
}