using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace ACR.DIR.DbEntityModels
{
    [Table("dcmStudies")]
    [Index(nameof(StudyInstanceUid), Name = "dcmStudies_studyInstanceUid_idx")]
    public class DcmStudy
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("studyInstanceUid"), MaxLength(70)]
        public string? StudyInstanceUid { get; set; }

        [Column("description"), MaxLength(500)]
        public string? Description { get; set; }

        [Column("date"), MaxLength(20)]
        public string? Date { get; set; }

        [Column("time"), MaxLength(20)]
        public string? Time { get; set; }

        [Column("accessionNumber"), MaxLength(45)]
        public string? AccessionNumber { get; set; }

        [Column("studyId"), MaxLength(45)]
        public string? StudyId { get; set; }

        [Column("patientId"), MaxLength(70)]
        public string? PatientId { get; set; }

        [Column("createdDateUTC")]
        public DateTime? CreatedDateUtc { get; set; }

        [Column("lastModifiedDateUtc")]
        public DateTime? LastModifiedDateUtc { get; set; }

        [ForeignKey(nameof(DcmSeries.DcmStudyId))]
        public ICollection<DcmSeries>? DcmSeriesList { get; set; }
    }
}