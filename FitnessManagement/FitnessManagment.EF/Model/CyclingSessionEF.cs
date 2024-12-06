using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitnessBL.Models;

namespace FitnessManagement.EF.Model {
    public class CyclingSessionEF {
        public CyclingSessionEF() { }

        public CyclingSessionEF(int cyclingSessionId, DateTime date, int duration, float avgWatt, float maxWatt, float avgCadence, float maxCadence, string? trainingType, string? comment, MemberEF member) {
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
        public int CyclingSessionId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Duration { get; set; }

        public float AvgWatt { get; set; }
        public float MaxWatt { get; set; }
        public float AvgCadence { get; set; }
        public float MaxCadence { get; set; }

        [Column(TypeName = "nvarchar(45)")]
        public string? TrainingType { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string? Comment { get; set; }

        // FK
        
        public MemberEF Member { get; set; }
    }
}
