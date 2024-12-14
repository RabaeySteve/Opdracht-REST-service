using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Exceptions {
    internal class RusnningSessionException : Exception {
        public RusnningSessionException(string? message) : base(message) {
        }

        public RusnningSessionException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
