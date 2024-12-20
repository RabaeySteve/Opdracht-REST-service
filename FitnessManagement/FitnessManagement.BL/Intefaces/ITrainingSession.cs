using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface ITrainingSession {
        int TrainingId { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        string TrainingType { get; set; }
        string GetImpact();
    }
}
