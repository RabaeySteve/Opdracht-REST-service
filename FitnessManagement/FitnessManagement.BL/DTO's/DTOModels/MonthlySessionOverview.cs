using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.DTO_s.DTOModels {
    public class MonthlySessionOverview {
        public string Month { get; set; }
        public int RunningSessions { get; set; }
        public int CyclingSessionsFun { get; set; }
        public int CyclingSessionsEndurance { get; set; }
        public int CyclingSessionsInterval { get; set; }
        public int CyclingSessionsRecovery { get; set; }
       
    }

}
