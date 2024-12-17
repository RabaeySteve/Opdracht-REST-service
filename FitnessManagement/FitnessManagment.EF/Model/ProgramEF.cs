using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    
    public class ProgramEF {
        public ProgramEF() { }

        public ProgramEF(string programCode, string name, string? target, DateTime startDate, int maxMembers) {
            ProgramCode = programCode;
            Name = name;
            Target = target;
            StartDate = startDate;
            MaxMembers = maxMembers;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("programCode", TypeName = "nvarchar(10)")] // Primaire sleutel
        public string ProgramCode { get; set; }

        [Required]
        [Column("name", TypeName = "nvarchar(45)")]
        public string Name { get; set; }

        [Required]
        [Column("target", TypeName = "nvarchar(25)")]
        public string Target { get; set; }

        [Required]
        [Column("startdate", TypeName = "datetime2(0)")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("max_members")]
        public int MaxMembers { get; set; }
    }
}
