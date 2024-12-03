using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace ACR.DIR.DbEntityModels
{
    [Table("dcmSeries")]
    [Index(nameof(SeriesInstanceUid), Name = "dcmSeries_seriesInstanceUid_idx")]
    public class DcmSeries
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("seriesInstanceUid"), MaxLength(70)]
        public string? SeriesInstanceUid { get; set; }

        [Column("dcmStudyId")]
        public long? DcmStudyId { get; set; }

        [Column("modality"), MaxLength(20)]
        public string? Modality { get; set; }

        [Column("seriesNumber")]
        public long? SeriesNumber { get; set; }

        [Column("description"), MaxLength(500)]
        public string? Description { get; set; }

        [Column("date"), MaxLength(20)]
        public string? Date { get; set; }

        [Column("createdDateUtc")]
        public DateTime? CreatedDateUtc { get; set; }

        [Column("lastModifiedDateUtc")]
        public DateTime? LastModifiedDateUtc { get; set; }

        [ForeignKey(nameof(DcmObject.DcmSeriesId))]
        public ICollection<DcmObject>? DcmObjects { get; set; }
    }
}