using FitnessManagement.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessManagement.BL.Models {
    public class TimeSlot {
		private int _startTime;
		
		

        public TimeSlot() {

        }
       
        public TimeSlot( int startTime) {
            _startTime = startTime;
            EndTime = EndTimeFunc(startTime);
            PartOfDay = PartOfDayFunc(startTime);
        }
        public TimeSlot(int timeSlotId, int startTime) {
            TimeSlotId = timeSlotId;
            _startTime = startTime;
            EndTime = EndTimeFunc(startTime);
            PartOfDay = PartOfDayFunc(startTime);
        }

       
        public int StartTime {
            get { return _startTime; }
            set {
                if (value < 8 && value > 21) {
                    throw new TimeSlotException("Start time must be between 8 and 21");
                }

                _startTime = value;
            }
        }

        public int EndTime { get; private set; }

        public string PartOfDay { get; private set; }

        public int TimeSlotId { get; set; }

        public override string? ToString() {
            return $"StartTime : {StartTime} EndTime : {EndTime} PartOfTheDay : {PartOfDay}";
        }

        public int EndTimeFunc(int startTime) {
           
            if (startTime < 8 || startTime > 21)
                throw new TimeSlotException("Start time must be between 8 and 21");

            return startTime + 1;
        }

        public string PartOfDayFunc(int startTime) {
            if (startTime < 8 || startTime > 21)
                throw new TimeSlotException("Start time must be between 8 and 21");

            if (startTime < 12) return "morning";
            if (startTime < 18) return "afternoon";
            if (startTime <= 21) return "evening";
            return "unknown";
        }

    }
}
