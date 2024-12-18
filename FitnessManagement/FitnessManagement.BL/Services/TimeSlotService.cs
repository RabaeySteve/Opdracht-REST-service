using FitnessManagement.BL.Exceptions;
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Services {
    public class TimeSlotService {
        private ITimeSlotRepository repo;

        public TimeSlotService(ITimeSlotRepository repo) {
            this.repo = repo;
        }

        public TimeSlot GetTimeSlot(int startTime) {
            try {
                return repo.GetTimeSlot(startTime);
            } catch (Exception ex) {

                throw new TimeSlotException("GetTimeSlot", ex);
            }
        }
    }

}
