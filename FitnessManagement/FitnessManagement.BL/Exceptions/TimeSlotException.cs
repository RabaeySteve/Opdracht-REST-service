using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Exceptions {
    internal class TimeSlotException : Exception {
        public TimeSlotException(string? message) : base(message) {
        }

        public TimeSlotException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
