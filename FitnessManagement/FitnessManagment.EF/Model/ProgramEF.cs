using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Column(TypeName = "nvarchar(10)")]
        public string ProgramCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(45)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string Target { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int MaxMembers { get; set; }
    }
}
