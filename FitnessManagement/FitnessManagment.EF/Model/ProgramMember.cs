using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    
    public class ProgramMember {
        //[Key, Column("member_id", Order = 1)] // Primaire sleutel - Deel 1
        //public int MemberId { get; set; }

        //[ForeignKey("member_id")]
        //public MemberEF Member { get; set; }

        //[Key, Column("programCode", TypeName = "nvarchar(10)", Order = 2)] // Primaire sleutel - Deel 2
        //public string ProgramCode { get; set; }

        //[ForeignKey("programCode")]
        //public ProgramEF Program { get; set; }



        [Key, Column("member_id", Order = 1)]
        public int MemberId { get; set; }
        public MemberEF Member { get; set; }

        [Key, Column("programCode", TypeName = "nvarchar(10)", Order = 2)]
        public string ProgramCode { get; set; }
        public ProgramEF Program { get; set; }
    }
}
