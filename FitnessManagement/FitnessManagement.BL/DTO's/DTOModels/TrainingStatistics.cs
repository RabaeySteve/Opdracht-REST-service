using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.DTO_s.DTOModels {
    public class TrainingStatistics {
        public int TotalSessions { get; set; }
        public double TotalDurationInMinutes { get; set; }
        public int ShortestSessionInMinutes {  get; set; }
        public int LongestSessionInMinutes { get; set; }
        public double AverageSessionDurationInMinutes { get; set; }
        
    }

}
