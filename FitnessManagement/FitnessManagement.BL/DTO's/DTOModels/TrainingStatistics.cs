using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.DTO_s.DTOModels {
    public class TrainingStatistics {
        public int TotalSessions { get; set; }
        public double TotalDurationInHours { get; set; }
        public int LongestSessionInMinutes { get; set; }
        public int ShortestSessionInMinutes { get; set; }
        public double AverageSessionDurationInMinutes { get; set; }
        public string TrainingImpact { get; set; } // Low, Medium, High
    }
}
