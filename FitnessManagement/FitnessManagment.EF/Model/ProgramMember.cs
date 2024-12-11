using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.EF.Model {
    public class ProgramMember {
        public int MemberId { get; set; }
        public MemberEF Member { get; set; }

        public string ProgramCode { get; set; }
        public ProgramEF Program { get; set; }
    }
}
