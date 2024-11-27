using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Exceptions {
    public class EquipmentException : Exception {
        public EquipmentException(string? message) : base(message) {
        }

        public EquipmentException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
