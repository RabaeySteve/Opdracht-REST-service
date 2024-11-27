﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Exceptions {
    internal class RunningsessionException : Exception {
        public RunningsessionException(string? message) : base(message) {
        }

        public RunningsessionException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}