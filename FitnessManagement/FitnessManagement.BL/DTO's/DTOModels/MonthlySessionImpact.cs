using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.DTO_s.DTOModels {
    public class MonthlySessionImpact {
        public string Month { get; set; }
        public int RunningSessions { get; set; }
        public int CyclingSessionsFun {  get; set; }
        public int FunImpactLow { get; set; }
        public int FunImpactMedium { get; set; }
        public int FunImpactHigh { get; set; }
        
        public int CyclingSessionsEndurance { get; set; }
        public int EnduranceImpactLow { get; set; }
        public int EnduranceImpactMedium { get; set; }
        public int EnduranceImpactHigh { get; set; }
        
        public int CyclingSessionsInterval { get; set; }
        public int IntervalImpactLow { get; set; }
        public int IntervalImpactMedium { get; set; }
        public int IntervalImpactHigh { get; set; }
        public int CyclingSessionsRecovery { get; set; }
        public int RecoveryImpactLow { get; set; }
        public int RecoveryImpactMedium { get; set; }
        public int RecoveryImpactHigh { get; set; }
    }

}
