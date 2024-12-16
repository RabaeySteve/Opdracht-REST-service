using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Exceptions {
    internal class RunningSessionException : Exception {
        public RunningSessionException(string? message) : base(message) {
        }

        public RunningSessionException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
