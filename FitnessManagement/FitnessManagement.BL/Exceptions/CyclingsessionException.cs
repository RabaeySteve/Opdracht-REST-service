using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Exceptions {
    public class CyclingsessionException : Exception {
        public CyclingsessionException(string? message) : base(message) {
        }

        public CyclingsessionException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
