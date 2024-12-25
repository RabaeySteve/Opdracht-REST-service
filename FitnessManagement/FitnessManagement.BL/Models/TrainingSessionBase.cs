using FitnessBL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Models {
    public abstract class TrainingSessionBase {
        public abstract int TrainingId { get; set; }
        public abstract Member Member { get; set; }
        public abstract DateTime Date { get; set; }
        public abstract int Duration { get; set; }
        public  TrainingSessionType TrainingSessionType { get; set; }

        protected virtual string SubType { get; set; }
        public abstract string GetImpact();
    }
    public enum TrainingSessionType {
        Cycling,
        Running,
    }
}
