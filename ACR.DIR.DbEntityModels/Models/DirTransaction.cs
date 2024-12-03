using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACR.DIR.DbEntityModels
{
    [Table("dirTransaction")]
    [Index(nameof(CbsTransactionId), IsUnique = true, Name = "dirTransaction_cbsTransactionid_idx")]
    [Index(nameof(CreatedDate), Name = "dirTransaction_createdDateUtc_idx")]
    public class DirTransaction
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("cbsTransactionId")]
        public long? CbsTransactionId { get; set; }

        [Column("facilityId")]
        public long FacilityId { get; set; }

        [Column("corporateId")]
        public long CorporateId { get; set; }

        [Column("userId"), MaxLength(60)]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// {Unknown = 0, Created = 1, Authorized = 2, Unauthorized = 3}
        /// </summary>
        [Column("authorizationStatus")]
        public long AuthorizationStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("authorizationStatusLogs")]
        public string? AuthorizationStatusLogs { get; set; } = string.Empty;

        [Column("createdDateUtc")]
        public DateTime? CreatedDate { get; set; }

        [Column("updatedDateUtc")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey(nameof(DcmObject.DirTransactionId))]
        public ICollection<DcmObject>? DcmObjects { get; set; }
    }
}
