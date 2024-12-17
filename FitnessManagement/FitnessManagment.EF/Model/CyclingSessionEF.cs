using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    
    public class CyclingSessionEF {
        public CyclingSessionEF() { }

        public CyclingSessionEF(int cyclingSessionId, DateTime date, int duration, int avgWatt, int maxWatt, int avgCadence, int maxCadence, string? trainingType, string? comment, MemberEF member) {
            CyclingSessionId = cyclingSessionId;
            Date = date;
            Duration = duration;
            AvgWatt = avgWatt;
            MaxWatt = maxWatt;
            AvgCadence = avgCadence;
            MaxCadence = maxCadence;
            TrainingType = trainingType;
            Comment = comment;
            Member = member;
        }

        [Key]
        [Column("cyclingsession_id")] // Primaire sleutel
        public int CyclingSessionId { get; set; }

        [Required]
        [Column("date", TypeName = "datetime2(0)")] // Datum
        public DateTime Date { get; set; }

        [Required]
        [Column("duration")]
        public int Duration { get; set; }

        [Required]
        [Column("avg_watt")]
        public int AvgWatt { get; set; }

        [Required]
        [Column("max_watt")]
        public int MaxWatt { get; set; }

        [Required]
        [Column("avg_cadence")]
        public int AvgCadence { get; set; }

        [Required]
        [Column("max_cadence")]
        public int MaxCadence { get; set; }

        [Required]
        [Column("trainingtype", TypeName = "nvarchar(45)")]
        public string? TrainingType { get; set; }

        [Column("comment", TypeName = "nvarchar(500)")]
        public string? Comment { get; set; }

        //[Required]
        //[Column("member_id")]
        //public int MemberId { get; set; }

        [ForeignKey("member_id")]
        public MemberEF Member { get; set; }
    }
}
