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

        public TimeSlot(int startTime, int endTime, string partOfDay, int timeSlotId) {
            _startTime = startTime;
            TimeSlotId = timeSlotId;
        }

        public string PartOfDay {
			get {
                if (StartTime < 12 && StartTime > 7) {
					return "morning";
                };
                if (StartTime >= 12 && StartTime < 18) {
                    return "afternoon";
                };
                if (StartTime >= 17 && StartTime <= 21) {
                    return "evening";
                };
				return "unknown";
            }
			
		}

        public int EndTime => StartTime + 1;


        public int StartTime {
			get { return _startTime; }
			set {
				if (value < 8 && value > 21) {
					throw new TimeSlotException("Start time must be between 8 and 21");
				}
				
				_startTime = value; }
		}

		public int TimeSlotId { get; set; }

        public override string? ToString() {
            return $"{PartOfDay} at {StartTime}:00";
        }
    }
}
