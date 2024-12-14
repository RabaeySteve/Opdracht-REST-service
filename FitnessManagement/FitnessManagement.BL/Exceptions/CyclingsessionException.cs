using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Exceptions {
    public class CyclingSessionException : Exception {
        public CyclingSessionException(string? message) : base(message) {
        }

        public CyclingSessionException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
