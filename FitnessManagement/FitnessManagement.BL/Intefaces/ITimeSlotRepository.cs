using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Intefaces {
    public interface ITimeSlotRepository {
        void addTimeSlot(TimeSlot timeSlot);
        IEnumerable<TimeSlot> GetAllTimeSlots();
    }
}
