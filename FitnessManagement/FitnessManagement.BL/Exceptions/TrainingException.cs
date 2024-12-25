using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Exceptions {
    public class TrainingException : Exception {
        public TrainingException(string? message) : base(message) {
        }

        public TrainingException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
