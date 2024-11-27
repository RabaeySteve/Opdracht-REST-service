using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Exceptions {
    public class ProgramException : Exception {
        public ProgramException(string? message) : base(message) {
        }

        public ProgramException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
