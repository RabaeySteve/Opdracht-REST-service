using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessManagement.EF.Model {
    
    public class ProgramMember {
        
        [Key, Column("member_id")]
        public int MemberId { get; set; }
        public MemberEF Member { get; set; }

        [Key, Column("programCode", TypeName = "nvarchar(10)")]
        public string ProgramCode { get; set; }
        public ProgramEF Program { get; set; }
    }
}
