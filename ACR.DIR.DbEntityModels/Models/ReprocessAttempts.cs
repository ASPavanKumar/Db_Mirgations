using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ACR.DIR.DbEntityModels
{
    [Table("reprocessAttempts")]
    public class ReprocessAttempts
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("configPath"), MaxLength(5000)]
        public string? ConfigPath { get; set; }
        [Column("objectCount")]
        public long? ObjectCount { get; set; }
        [Column("createdDate")]
        public DateTime? CreatedDate { get; set; }

        [ForeignKey(nameof(DcmObject.ReprocessAttemptId))]
        public ICollection<DcmObject>? DcmObjects { get; set; }
    }
}
